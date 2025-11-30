using Microsoft.AspNetCore.Components;
using static BlazorDSL.Html;

namespace BlazorDSL.Components.Pages;

[Route("/counter")]
public partial class Counter : WebComponent
{        
    protected override Node Render() =>
        div(
            h1("counter"),
            div(
                [className("box")],
                p("Current Count: " + currentCount),
                button(
                    [className("btn btn-primary")],
                    "Click me"
                )
            )
        );

    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++;
    }
}
