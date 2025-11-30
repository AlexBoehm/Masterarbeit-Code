using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorDSL;

static class Html {
    #region Tags
    public static Node div(Attribute[] attributes, params Node[] inner)
        => new TagNode("div", attributes, inner);

    public static Node div(params Node[] inner) => new TagNode("div", inner);
    public static Node div(IEnumerable<Node> inner) => new TagNode("div", inner.ToArray());
    public static Node h1(string text) => new TagNode("h1", Html.text(text));
    public static Node text(string text) => new TextNode(text);
    public static Node p(string text) => new TagNode("p", Html.text(text));
    public static Node button(string text) => new TagNode("button", Html.text(text));
    public static Node button(Attribute[] attributes, string text)
        => new TagNode("button", attributes, Html.text(text));
    public static Node DocTypeHtml() => new TagNode("!DOCTYPE html");
    public static Node html(Attribute[] attributes, params Node[] inner) => new TagNode("html", inner);
    public static Node head(params Node[] inner) => new TagNode("div", inner);
    public static Node meta(Attribute[] attributes, params Node[] inner)
        => new TagNode("meta", attributes, inner);
    public static Node @base(Attribute[] attributes, params Node[] inner)
        => new TagNode("base", attributes, inner);
    public static Node link(Attribute[] attributes, params Node[] inner)
        => new TagNode("link", attributes, inner);
    public static Node body(Attribute[] attributes, params Node[] inner)
        => new TagNode("body", attributes, inner);
    public static Node script(Attribute[] attributes, params Node[] inner)
        => new TagNode("script", attributes, inner);


    #endregion

    public static Node Tags(params Node[] nodes)
        => new ArrayNode(nodes.ToArray());

    public static Node Cond(bool condition, Func<Node> nodesTrue, Func<Node> nodeFalse)
        => new CondNode(condition, condition ? nodesTrue() : nodeFalse());

    public static Node empty() => EmptyNode.Instance;

    #region Attributes

    public static Attribute className(string value)
        => new Attribute("class", value);

    public static Attribute lang(string value)
        => new Attribute("lang", value);

    public static Attribute charset(string value)
        => new Attribute("charset", value);

    public static Attribute name(string value)
        => new Attribute("name", value);

    public static Attribute content(string value)
        => new Attribute("content", value);

    public static Attribute href(string value)
        => new Attribute("href", value);

    public static Attribute link(string value)
        => new Attribute("link", value);

    public static Attribute rel(string value)
        => new Attribute("rel", value);

    public static Attribute type(string value)
        => new Attribute("type", value);

    public static Attribute src(string value)
        => new Attribute("src", value);

    public static Attribute onClick(object sender, Action callback)
        => new Attribute(
            "onclick",
            EventCallback.Factory.Create<MouseEventArgs>(sender, callback)
        );

    public static Attribute parameter(string name, object value)
        => new Attribute(name, value);

    #endregion

    #region templateParameter

    public static Attribute templateParameter(string key, params Node[] template)
        => new Attribute(
                key,
            (RenderFragment)(
                (RenderTreeBuilder builder) => {
                    Renderer.Render(builder, new ArrayNode(template));
                }
            )
        );

    public static Attribute templateParameter(string key, Func<Node> template)
        => new Attribute(
                key,                
            (RenderFragment)(
                (RenderTreeBuilder builder) => {
                    Renderer.Render(builder, template());
                }
            )
        );

    public static Attribute templateParameter<TContext>(string key, Func<TContext, Node> template)
        => new Attribute(
            key,                
            (RenderFragment<TContext>)(
                (TContext context) =>
                    (RenderTreeBuilder builder) => {
                        Renderer.Render(builder, template(context));
                    }
                )
            );

    #endregion

    #region Components

    public static Node Component<TComponent>()
        => new ComponentNode(typeof(TComponent));

    public static Node Component<TComponent>(IComponentRenderMode renderMode)
        => new ComponentNode(typeof(TComponent), Array.Empty<Attribute>(), renderMode);

    public static Node Component<TComponent>(IComponentRenderMode renderMode, Attribute[] parameters)
        => new ComponentNode(typeof(TComponent), parameters, renderMode);

    public static Node Component<TComponent>(Attribute[] parameters, params Node[] childContent)
        => new ComponentNode(
            typeof(TComponent),
            childContent.Length == 0
                ? parameters
                : parameters.Concat([templateParameter("ChildContent", childContent)]).ToArray()
            );

    public static Node Component<TComponent>(IComponentRenderMode renderMode, Attribute[] parameters, params Node[] childContent)
        => new ComponentNode(
            typeof(TComponent),
            childContent.Length == 0
                ? parameters
                : parameters.Concat([templateParameter("ChildContent", childContent)]).ToArray(),
            renderMode
        )
    ;

    #endregion
}
