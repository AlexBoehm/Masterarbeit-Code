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
                div(
                    from name in names
                    where name.StartsWith("J")
                    select p(name)
                ),
                button([
                    className("btn btn-primary"),
                    onClick(this, IncrementCount)],
                    "Click me"
                )
            )
        );

    string[] names = [
        "George Washington",
        "John Adams",
        "Thomas Jefferson",
        "James Madison",
        "James Monroe",
        "John Quincy Adams",
        "Andrew Jackson"
    ];

    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++;
    }
}
