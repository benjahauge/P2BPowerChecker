using Android;
using Android.Content.PM;
using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace P2BPowerChecker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ShowBatteryStatus(Battery.State == BatteryState.Charging);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Battery.BatteryInfoChanged += Battery_BatteryInfoChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Battery.BatteryInfoChanged -= Battery_BatteryInfoChanged;
        }

        private void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
        {
            ShowBatteryStatus(e.State == BatteryState.Charging);
            if (e.State == BatteryState.Discharging)
            {
                //string phoneNumber = Preferences.Get(nameof(PhoneNumber.Text), PhoneNumber.Text);
                //string smsMessage = Preferences.Get(nameof(SMSMessage.Text), SMSMessage.Text);

                //bool hasPhoneNumberKey = Preferences.ContainsKey(nameof(SavedPhoneNumber));
                //bool hasSmsMessageKey = Preferences.ContainsKey(nameof(SavedSMSMessage));

                var smsMessenger = CrossMessaging.Current.SmsMessenger;
                if (smsMessenger.CanSendSmsInBackground)
                {
                    smsMessenger.SendSmsInBackground(SavedPhoneNumber, SavedSMSMessage);
                }
            }
        }

        //string phoneNumber = Preferences.Get(nameof(SavedPhoneNumber), SavedPhoneNumber);
        public string SavedPhoneNumber
        {
            get => Preferences.Get(nameof(SavedPhoneNumber), PhoneNumber.Text);
            set
            {
                //phoneNumber = value;
                Preferences.Set(nameof(SavedPhoneNumber), value);
            }
        }

        public string SavedSMSMessage
        {
            get => Preferences.Get(nameof(SavedSMSMessage), SMSMessage.Text);
            set
            {
                Preferences.Set(nameof(SavedSMSMessage), value);
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("key_phone_number", SavedPhoneNumber);
            Preferences.Set("key_sms_message", SavedSMSMessage);

            //string number = Preferences.Get("key_phone_number", PhoneNumber.Text);
            //string message = Preferences.Get("key_sms_message", SMSMessage.Text);

            //ShowPhoneNumber.Text = number;
            //ShowSMSMessage.Text = message;
        }

        private void ShowBatteryStatus(bool charging)
        {
            var status = charging ? "Charging" : "Not charging";
            LabelBatteryLevel.Text = status;
            BatteryMessage.Text = "Current status is:";
        }
    }
}
