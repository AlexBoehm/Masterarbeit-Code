using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorDSL;

static class Renderer {
    public static void Render(RenderTreeBuilder builder, Node node) {
        Render(builder, node, 0);
    }

    private static int Render(RenderTreeBuilder builder, Node node, int sequenceNumber) {
        return node switch {
            TagNode n => RenderTagNode(builder, sequenceNumber, n),
            TextNode n => RenderTextNode(builder, sequenceNumber, n),
            CondNode n => RenderCondNode(builder, sequenceNumber, n),
            ArrayNode n => RenderArrayNode(builder, sequenceNumber, n),
            EmptyNode _ => RenderEmptyNode(builder, sequenceNumber),
            ComponentNode n => RenderComponentNode(builder, sequenceNumber, n),
            _ => throw new Exception("Unexpected node of Type " + node.GetType().FullName)
        };
    }

    private static int RenderTagNode(RenderTreeBuilder builder, int sequenceNumber, TagNode n) {
        builder.OpenRegion(sequenceNumber);
        builder.OpenElement(0, n.Tag);

        AddAttributes(1, builder, n.Attributes);

        var nextSequenceInRegion = 2;

        for (int i = 0; i < n.Inner.Length; i++) {
            nextSequenceInRegion = Render(builder, n.Inner[i], nextSequenceInRegion);
        }

        builder.CloseElement();
        builder.CloseRegion();
        return sequenceNumber + 1;
    }

    private static int RenderTextNode(RenderTreeBuilder builder, int sequenceNumber, TextNode n) {
        builder.AddContent(sequenceNumber, n.Text);
        return sequenceNumber + 1;
    }

    private static int RenderCondNode(RenderTreeBuilder builder, int sequenceNumber, CondNode condNode)
    {
        var sequenceNumberToUse = condNode.Condition ? sequenceNumber : sequenceNumber + 1;

        builder.OpenRegion(sequenceNumberToUse);
        Render(builder, condNode.Content);
        builder.CloseRegion();

        // Nichts zu rendern, die Sequenznummer muss um 2 erhöht werden, da jeder Zweig eine
        // eindeutige Sequenznummer bekommt.
        return sequenceNumber + 2;
    }

    private static int RenderArrayNode(RenderTreeBuilder builder, int sequenceNumber, ArrayNode n) {
        builder.OpenRegion(sequenceNumber);

        var nextSequenceInRegion = 0;

        for (int i = 0; i < n.Inner.Length; i++) {
            nextSequenceInRegion = Render(builder, n.Inner[i], nextSequenceInRegion);
        }

        builder.CloseRegion();

        return sequenceNumber + 1;
    }

    private static int RenderEmptyNode(RenderTreeBuilder builder, int sequenceNumber) {
        // Nichts zu rendern, die Sequenznummer muss jedoch erhöht werden
        return sequenceNumber + 1;
    }

    private static int RenderComponentNode(RenderTreeBuilder builder, int sequenceNumber, ComponentNode n) {
        builder.OpenRegion(sequenceNumber);
        builder.OpenComponent(0, n.Type);

        if(n.RenderMode != null)
            builder.AddComponentRenderMode(n.RenderMode);

        AddComponentAttributes(1, builder, n.Attributes, out var nextSequenceNumber);
        builder.CloseComponent();
        builder.CloseRegion();
        return nextSequenceNumber;
    }

    private static void AddComponentAttributes(int sequenceNumber, RenderTreeBuilder builder, Attribute[] attributes, out int nextSequenceNumber) {
        nextSequenceNumber = sequenceNumber;

        foreach (var item in attributes)
        {
            builder.AddComponentParameter(nextSequenceNumber, item.Name, item.Value);
            nextSequenceNumber++;
        }
    }

    private static void AddAttributes(int sequenceNumber, RenderTreeBuilder builder, Attribute[] attributes)
    {
        builder.AddMultipleAttributes(
            sequenceNumber,
            from attribute in attributes
            select new KeyValuePair<string, object>(attribute.Name, attribute.Value)
        );
    }
}
