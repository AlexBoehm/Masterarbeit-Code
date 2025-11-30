using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.MVU;
class MVUViewBuilderSimple
{
    static Dispatch<TMessage> BuildDispatchSimple<TState, TMessage>(TState state, UpdateSimple<TState, TMessage> update)
    {
        var dispatchSemaphore = new SemaphoreSlim(1, 1);

        void dispatch(TMessage msg)
        {
            dispatchSemaphore.Wait();

            try
            {
                Debug.WriteLine(msg);
                state = update(state, msg);
            }
            finally
            {
                dispatchSemaphore.Release();
            }
        };

        return dispatch;
    }

    public static ExecuteView BuildViewMethodSimple<TState, TMessage>(
        InitSimple<TState> initState,
        UpdateSimple<TState, TMessage> update,
        View<TState, TMessage> view
    ) {
        var state = initState();
        var dispatch = BuildDispatchSimple(state, update);
        return sender => view(state, dispatch, sender);
    }
}
