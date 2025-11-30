using Microsoft.AspNetCore.Components;

namespace BlazorDSL;

public abstract class Node {
    protected static Attribute[] _emptyAttributes = [];
}

public class TextNode : Node {
    public string Text { get; private set; }

    public TextNode(string text) {
        Text = text;
    }
}

public class TagNode : Node {
    public string Tag { get; private set; }
    public Node[] Inner { get; private set; }
    public Attribute[] Attributes { get; private set; }

    public TagNode(string tag) {
        Tag = tag;
        Inner = _emptyNodes;
        Attributes = _emptyAttributes;
    }

    public TagNode(string tag, Attribute[] attributes, params Node[] inner) {
        Tag = tag;
        Inner = inner;
        Attributes = attributes;
    }

    public TagNode(string tag, params Node[] inner) {
        Tag = tag;
        Inner = inner;
        Attributes = _emptyAttributes;
    }

    protected static Node[] _emptyNodes = new Node[0];
    protected static Attribute[] _emptyAttributes = new Attribute[0];
}

public class CondNode : Node {
    public bool Condition {get; private set; }
    public Node Content {get; private set; }
        
    public CondNode(bool condition, Node content)
    {
        Condition = condition;
        Content = content;
    }
}

public class ArrayNode : Node {
    public Node[] Inner { get; private set; }

    public ArrayNode(Node[] inner) {
        this.Inner = inner;
    }
}

public class EmptyNode : Node {
    public static EmptyNode Instance { get; private set; } = new EmptyNode();
    private EmptyNode() {}
}

public class ComponentNode : Node {
    public Type Type { get; private set; }
    public Attribute[] Attributes { get; private set; }
    public IComponentRenderMode RenderMode {get; private set; }

    public ComponentNode(Type type) {
        Type = type;
        Attributes = _emptyAttributes;
        RenderMode = null;
    }

    public ComponentNode(Type type, Attribute[] attributes) {
        Type = type;
        Attributes = attributes;
        RenderMode = null;
    }

    public ComponentNode(Type type, Attribute[] attributes, IComponentRenderMode renderMode) {
        Type = type;
        Attributes = attributes;
        RenderMode = renderMode;
    }
}

public class Attribute {
    public string Name { get; set; }
    public object Value { get; set; }

    public Attribute(string key, object value) {
        this.Name = key;
        this.Value = value;
    }
}
