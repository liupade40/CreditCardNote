using System;
using System.IO;
using Xamarin.Forms;
using Android.Widget;
using CreditCardNote.Droid.Implement;
using CreditCardNote.PlatformInterface;
using MonoDroid= Android.App;

[assembly: Dependency(typeof(Toast_Android))]
namespace CreditCardNote.Droid.Implement
{
    public class Toast_Android : IToast
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(MonoDroid.Application.Context, message, ToastLength.Long).Show();
        }
        public void ShortAlert(string message)
        {
            Toast.MakeText(MonoDroid.Application.Context, message, ToastLength.Short).Show();
        }
    }
}