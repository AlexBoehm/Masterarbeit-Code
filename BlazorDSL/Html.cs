using Microsoft.AspNetCore.Components;
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
    #endregion

    public static Node Tags(params Node[] nodes)
        => new ArrayNode(nodes.ToArray());

    public static Node Cond(bool condition, Func<Node> nodesTrue, Func<Node> nodeFalse)
        => new CondNode(condition, condition ? nodesTrue() : nodeFalse());

    public static Node empty() => EmptyNode.Instance;

    #region Attributes

    public static Attribute className(string className)
        => new Attribute("class", className);

    public static Attribute onClick(object sender, Action callback)
        => new Attribute(
            "onclick",
            EventCallback.Factory.Create<MouseEventArgs>(sender, callback)
        );

    public static Attribute parameter(string name, object value)
        => new Attribute(name, value);

    #endregion

    #region Components

        public static Node Component<TComponent>()
            => new ComponentNode(typeof(TComponent));

        public static Node Component<TComponent>(Attribute[] parameters)
            => new ComponentNode(typeof(TComponent), parameters);

    #endregion
}
