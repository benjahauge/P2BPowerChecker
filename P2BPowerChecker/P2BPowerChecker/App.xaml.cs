using P2BPowerChecker.Data;
using System;
using System.IO;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace P2BPowerChecker
{
    public partial class App : Application
    {
        static PCMessengerDatabase database;

        public static PCMessengerDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new PCMessengerDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PCMessengers.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new MenuPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
