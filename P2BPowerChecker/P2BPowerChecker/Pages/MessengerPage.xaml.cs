using P2BPowerChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace P2BPowerChecker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MessengerPage : ContentPage
	{
		public MessengerPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetItemsAsync();
        }

        async void ToolbarAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPage());
        }

        async void SwipeItem_Invoked_Edit(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var pcm = item.CommandParameter as PCMessenger;
            await Navigation.PushAsync(new AddPage(pcm));
        }

        async void SwipeItem_Invoked_Delete(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var pcm = item.CommandParameter as PCMessenger;
            var result = await DisplayAlert("Delete", $"Delete {pcm.PhoneNumber} from the database?", "Yes", "No");
            if (result)
            {
                await App.Database.DeleteItemAsync(pcm);
            }
            collectionView.ItemsSource = await App.Database.GetItemsAsync();
        }
    }
}