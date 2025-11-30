using System.Diagnostics;

namespace Lib.MVU;

public static class MVUViewBuilder {
    static void ExecuteCommand<TMessage>(
        Command<TMessage> command, 
        Dispatch<TMessage> dispatch
    ) {
        var hasToBeExecuted = !Equals(Cmd<TMessage>.None, command);

        if(hasToBeExecuted) {
            Task.Factory.StartNew(
                async () => {
                    Debug.WriteLine($"[{DateTime.Now}] Starting command {command}");
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    await command(dispatch);
                    stopwatch.Stop();
                    Debug.WriteLine($"[{DateTime.Now}] Command executed; Took {stopwatch.Elapsed.TotalMilliseconds} ms");
                }
            );
        }
    }

    /// <summary>
    /// Erstellt die Methode dispatch
    /// </summary>
    static Dispatch<TMessage> BuildDispatch<TState, TMessage>(
        Update<TState, TMessage> update, 
        TState initState,
        RenderComponent renderComponent,
        Action<TState> setState 
    )
    {
        // Es muss sichergestellt werden, dass immer eine Nachricht nach der anderen behandelt wird.
        // Es dürfen niemals mehrere Nachrichten gleichzeitig behandelt werden. Sonst kann es passieren,
        // dass Zustandsänderungen verloren gehen.
        var dispatchSemaphore = new SemaphoreSlim(1, 1);
        var state = initState;

        void dispatch(TMessage message)
        {
            dispatchSemaphore.Wait();

            try{
                Debug.WriteLine($"[{DateTime.Now}] {message}");

                // Berechnung neuer State und Command
                (state, var command) = update(state, message);

                Debug.WriteLine($"New state: {state}");
                setState(state);
                renderComponent();

                // Ausführen des Commands
                ExecuteCommand(command, dispatch);
            }
            finally
            {
                dispatchSemaphore.Release();
            }
        }

        return dispatch;
    }

    public static (ExecuteView execute, Action initialize) BuildExecuteView<TState, TMessage>(
        Init<TState, TMessage> init,
        Update<TState, TMessage> update,
        View<TState, TMessage> view,
        RenderComponent renderComponent
    ) {
        (TState state, Command<TMessage> initCommand) = init();
        Dispatch<TMessage> dispatch = BuildDispatch(update, state, renderComponent, newState => state = newState);

        return (
            execute: sender => view(state, dispatch, sender),
            initialize: () => ExecuteCommand(initCommand, dispatch)
        );
    }
}
