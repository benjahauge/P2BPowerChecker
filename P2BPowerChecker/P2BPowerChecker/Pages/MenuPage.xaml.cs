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
	public partial class MenuPage : FlyoutPage
	{
		public MenuPage ()
		{
			InitializeComponent ();
			flyout.listView.ItemSelected += OnSelectedItem;
		}

		private void OnSelectedItem(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as FlyoutItemPage;
			if (item != null)
			{
				Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetPage));
				flyout.listView.SelectedItem = null;
				IsPresented = false;
			}
		}
	}
}