using P2BPowerChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace P2BPowerChecker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddPage : ContentPage
	{
		public AddPage ()
		{
			InitializeComponent ();
		}

        PCMessenger _pCMessenger;
        public AddPage(PCMessenger pCMessenger)
        {
            InitializeComponent();
            Title = "Edit information";
            _pCMessenger = pCMessenger;
            phoneEntry.Text = pCMessenger.PhoneNumber;
            messageEntry.Text = pCMessenger.SmsMessage;
            phoneEntry.Focus();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(phoneEntry.Text))
            {
                await DisplayAlert("Invalid", "Blank or whitespace value is invalid", "OK");
            }
            else if (_pCMessenger != null)
            {
                EditPCMessenger();
            }
            else
                AddPCMessenger();
        }

        async void AddPCMessenger()
        {
            await App.Database.SaveItemAsync(new PCMessenger
            {
                PhoneNumber = phoneEntry.Text,
                SmsMessage = messageEntry.Text,
            });

            await Navigation.PopAsync();
        }

        async void EditPCMessenger()
        {
            _pCMessenger.PhoneNumber = phoneEntry.Text;
            _pCMessenger.SmsMessage = messageEntry.Text;
            await App.Database.UpdateItemAsync(_pCMessenger);
            await Navigation.PopAsync();
        }
    }
}