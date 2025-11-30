using Lib.MVU;
using System.Collections.Immutable;

namespace BlazorDSL.Components.Pages.Todo;

static class Commands
{
    public static Command<Message> LoadItemsFromDatabase()
    {
        return async (Dispatch<Message> dispatch) =>
        {
            var items = await Database.GetItems();
            dispatch(new ItemsFromDatabaseLoaded(items.ToImmutableList()));
        };
    }

    public static Command<Message> AddNewItem(string text)
    {
        return async (Dispatch<Message> dispatch) =>
        {
            var item = new TodoItem(-1, text, false);
            var id = await Database.InsertItem(item);
            item = item with { Id = id };
            dispatch(new ItemAdded(item));
        };
    }

    public static Command<Message> ChangeItemText(ChangeItemText cmd)
    {
        return async (Dispatch<Message> Dispatch) =>
        {
            await Database.UpdateItem(cmd.item with { Text = cmd.text }).ConfigureAwait(false);
        };
    }

    public static Command<Message> SetItemStatus(SetItemStatus cmd)
    {
        return async (Dispatch<Message> Dispatch) =>
        {
            await Database.UpdateItem(cmd.item with { Done = cmd.status }).ConfigureAwait(false);
        };
    }

    public static Command<Message> RemoveDoneItems(State state)
    {
        return async (Dispatch<Message> dispatch) =>
        {
            var doneItems = state.TodoItems.Where(x => x.Done);
            var ids = doneItems.Select(x => x.Id);
            await Database.RemoveItems(ids).ConfigureAwait(false);
            dispatch(new DoneItemsRemoved(ids.ToImmutableList()));
        };
    }

    public static Command<Message> RemoveItem(RemoveItem cmd)
    {
        return async (Dispatch<Message> Dispatch) =>
        {
            await Database.RemoveItem(cmd.item.Id).ConfigureAwait(false);
        };
    }
}
