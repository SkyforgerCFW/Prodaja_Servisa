using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using prkym.Models;
using prkym.Views;
using System.Linq;

namespace prkym.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Servisi";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                Item newItem = item as Item;
                await DataStore.AddItemAsync(newItem);
                Items.Add(newItem);
            });

            MessagingCenter.Subscribe<ItemsPage, Item>(this, "DeleteItem", async (obj, item) =>
            {
                Item oldItem = item as Item;
                await DataStore.DeleteItemAsync(oldItem.Id);
                Items.Remove(oldItem);
            });

            MessagingCenter.Subscribe<EditPage, Item>(this, "EditItem", async (obj, item) =>
            {
                await DataStore.UpdateItemAsync(item);
                ExecuteLoadItemsCommand();
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}