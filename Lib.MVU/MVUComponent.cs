using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using BlazorDSL;
using System.Diagnostics;

namespace Lib.MVU;

public abstract class MVUComponent : ComponentBase {
    ExecuteView _executeView;
    Action _initialize;

    protected void Setup<TState, TMessage>(
        Init<TState, TMessage> init,
        Update<TState, TMessage> update,
        View<TState, TMessage> view
    ) {
        (_executeView, _initialize) = MVUViewBuilder.BuildExecuteView(
            init,
            update,
            view,
            this.RenderComponent
        );
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder) {
        // View aufrufen, um den aktuellen Inhalt des Views zu berechnen
        Node content = _executeView(this);

        // Inhalt in die Blazor-Komponente rendern
        Renderer.Render(builder, content);
    }

    protected override void OnInitialized() {
        _initialize();
    }

    void RenderComponent() {
        InvokeAsync(() => {
            try {
                // Rendern der Komponente auslösen
                StateHasChanged();
            } catch (Exception e) {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
            }
        });
    }
}