using Lib.MVU;
using Microsoft.AspNetCore.Components;

namespace BlazorDSL.Components.Pages.Todo;

[Route("/todo")]
public class TodoPageComponent : MVUComponent {
    public TodoPageComponent() {
        Setup(TodoPage.Init, TodoPage.Update, TodoPage.View);
    }
}
