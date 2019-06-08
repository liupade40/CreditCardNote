using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CreditCardNote.View;
using Naxam.Controls.Forms;
using Xamarin.Forms;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace CreditCardNote
{
    /// <summary>
    /// BennyLiu 123456
    /// </summary>
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
		    var tabs = new BottomTabbedPage();
		    tabs.Title = "信用卡笔记";
		    tabs.BackgroundColor = Color.White;
		    tabs.BarBackgroundColor = Color.White;
		    tabs.Children.Add(new MessageList());
		    tabs.Children.Add(new CreditCardList());
            tabs.Children.Add(new OtherList());
            MainPage =new NavigationPage(tabs);
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
