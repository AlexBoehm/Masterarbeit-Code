namespace BlazorDSL.Components.Pages;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

[Route("/counter")]
public partial class Counter : ComponentBase
{        
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder
            .h1("Counter")
            .div([
                new KeyValuePair<string, object>("class", "box")],
                inner =>
                    inner
                    .p("Current count: " + currentCount)
                    .button([
                        new KeyValuePair<string, object>("class", "btn btn-primary"),
                        new KeyValuePair<string, object>(
                            "onclick",
                            EventCallback.Factory.Create<MouseEventArgs>(
                                this,
                                IncrementCount
                            )
                        )],
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
