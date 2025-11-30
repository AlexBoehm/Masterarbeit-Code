using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using static BlazorDSL.Html.bind;
using static BlazorDSL.Html;

namespace BlazorDSL; 

public abstract class WebComponent : ComponentBase {
    protected WebComponent()
    {
        var input = new Input(this);
        var change = new Change(this);
        bind = new Bind(input, change);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder) {
        var node = Render();
        Renderer.Render(builder, node);
    }

    protected abstract Node Render();

    protected Attribute onSubmit(Action<EventArgs> callback)
        => Html.onSubmit(this, callback);

    protected Attribute onSubmit(Action callback)
        => Html.onSubmit(this, _ => callback());

    protected Bind bind {get; private set; }
    protected Change change {get; private set; }

    public class Bind
    {
        public Input input {get; private set;}
        public Change change {get; private set;}

        public Bind(Input input, Change change)
        {
            this.input = input;
            this.change = change;
        }
    }

    public class Change
    {
        WebComponent _component;

        public Change(WebComponent webComponent)
        {
            _component = webComponent;
        }

        public Attribute @checked(bool value, Action<bool> onChange)            
            => Html.bind.change.@checked(_component, value, onChange);
    }

    public class Input
    {
        WebComponent _component;

        public Input(WebComponent webComponent)
        {
            _component = webComponent;
        }

        public Attribute @string(string value, Action<string> onChange)
            => Html.bind.input.@string(_component, value, onChange);
    }
}
