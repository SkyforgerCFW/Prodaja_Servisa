using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using prkym.Models;

namespace prkym.Views
{
    public partial class EditPage : ContentPage
    {
        public Item Item { get; set; }

        public EditPage(Item item)
        {
            InitializeComponent();

            Item = item;

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "EditItem", Item);
            await Navigation.PopAsync();
        }
    }
}
