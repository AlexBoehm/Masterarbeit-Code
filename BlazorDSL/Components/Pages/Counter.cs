namespace BlazorDSL.Components.Pages;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using static BlazorDSL.HTML;

[Route("/counter")]
public partial class Counter : ComponentBase
{        
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder
            .h1("Counter")
            .div([
                Attribute("class", "box")],
                inner =>
                    inner
                    .p("Current count: " + currentCount)
                    .button([
                        className("btn btn-primary"),
                        OnClick(this, IncrementCount)
                        ],
                        "Click me"
                    )
                );
    }

    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++;
    }
}
