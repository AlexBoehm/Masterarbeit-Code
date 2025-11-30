using static BlazorDSL.Html;
using Lib.MVU;
using Microsoft.AspNetCore.Components;

namespace BlazorDSL.Components.Pages.Todo;

partial class TodoPage
{
    public static Node View(State state, Dispatch<Message> dispatch, IComponent component) =>
        div(
            [className("TodoList")],
            h2("Todo"),

            div([className("Filter")],
                span(text("Filter")),
                input([
                    type("text"),
                    bind.input.@string(
                        component,
                        state.FilterText,
                        value => dispatch(new SetFilter(value))
                    )]
                )
            ),

            ul(
                from item in state.TodoItems
                where state.FilterText == "" || item.Text.Contains(state.FilterText, System.StringComparison.OrdinalIgnoreCase)
                select li(
                    input([
                        type("checkbox"),
                        bind.change.@checked(
                            component,
                            item.Done,
                            status => dispatch(new SetItemStatus(item, status))
                        )
                    ]),
                    input([
                        type("text"),
                        item.Done ? style("text-decoration: line-through") : style(""),
                        bind.input.@string(component, item.Text, nv => dispatch(new ChangeItemText(item, nv)))
                    ]),
                    button([
                        className("Delete"),
                        onClick(component, _ => dispatch(new RemoveItem(item)))
                        ],
                        text("x")
                    )
                )
            ),

            div(
                form([onSubmit(component, _ => dispatch(new AddItem(state.InputText)))],
                    input([
                        type("text"),
                        bind.input.@string(component, state.InputText, nv => dispatch(new SetInputText(nv)))
                    ]),
                    button(
                        span([
                            className("oi oi-plus"),
                            attribute("aria-hidden", "true")]
                        ),
                        text("add") // <span class="oi oi-plus" aria-hidden="true" b-4vrz9tvk6a=""></span>
                    )
                )
            ),

            div([className("ButtonBox")],
                button([
                    onClick(component, x => dispatch(new RemoveDoneItems())),
                    className("RemoveDone")
                    ],
                    "remove all done"
                )
            )
        );
}
