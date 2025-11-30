using Microsoft.AspNetCore.Components;
using static BlazorDSL.Html;

namespace BlazorDSL.Components.Pages;

[Route("/counter")]
public partial class Counter : WebComponent
{
    [Inject]
    public NameService NameService {get; set;}

    protected override Node Render() =>
        div(
            h1("counter"),
            Component<Greeting>(
                [parameter("Name", NameService.Name)],
                text("Das ist eine Nachricht")
            ),
            div(
                [className("box")],
                p("Current Count: " + currentCount),
                div(
                    from name in names
                    select Tags(
                        p(name),
                        Cond(
                            name.StartsWith("J"),
                            () => button(
                                [onClick(this, _ => names.Remove(name))],
                                "delete"
                            ),
                            () => empty()
                        )
                    )
                ),
                button([
                    className("btn btn-primary"),
                    onClick(this, _ => IncrementCount())],
                    "Click me"
                )
            )
        );

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
