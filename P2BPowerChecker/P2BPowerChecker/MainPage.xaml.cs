using Android;
using Android.Content.PM;
using Java.Lang;
using P2BPowerChecker.Data;
using P2BPowerChecker.Models;
using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Android.Provider.ContactsContract.CommonDataKinds;

namespace P2BPowerChecker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ShowBatteryStatus(Battery.State == BatteryState.Charging);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetItemsAsync();
            Battery.BatteryInfoChanged += Battery_BatteryInfoChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Battery.BatteryInfoChanged -= Battery_BatteryInfoChanged;
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

        private void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
        {
            ShowBatteryStatus(e.State == BatteryState.Charging);
            if (e.State == BatteryState.Discharging)
            {
                List<PCMessenger> listOfMessengers = App.Database.GetItemsAsync().Result;
                var smsMessenger = CrossMessaging.Current.SmsMessenger;
                if (smsMessenger.CanSendSmsInBackground)
                {
                    foreach (var sms in listOfMessengers)
                    {
                        smsMessenger.SendSmsInBackground(sms.PhoneNumber, sms.SmsMessage);
                    }
                }
            }
        }

        private void ShowBatteryStatus(bool charging)
        {
            var status = charging ? "Charging" : "Not charging";
            LabelBatteryLevel.Text = status;
            BatteryMessage.Text = "Current status is:";
        }
    }
}
