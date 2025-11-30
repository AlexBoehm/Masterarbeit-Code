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
                    .div(
                        [], 
                        inner => inner.ForEach(
                            names,
                            (inner, name) => {
                                inner.p(name);

                                if (name.StartsWith("J")) {
                                    inner.button(
                                        [OnClick(this, () => names.Remove(name))],
                                        "delete"
                                    );
                                }
                            }
                        )
                    )
                    .p("Current count: " + currentCount)
                    .button([
                        className("btn btn-primary"),
                        OnClick(this, IncrementCount)
                        ],
                        "Click me"
                    )
                );
    }

    List<string> names = [
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
