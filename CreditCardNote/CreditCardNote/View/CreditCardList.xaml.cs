using CreditCardNote.Infrastructrue.Common;
using CreditCardNote.PlatformInterface;
using CreditCardNote.SQLite.DAL;
using CreditCardNote.SQLite.Model;
using CreditCardNote.SQLite.SQLiteHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CreditCardNote.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreditCardList : ContentPage
	{
        public static ObservableCollection<CreditCard> CreditCard = new ObservableCollection<CreditCard>();
        public static Action<string,string> AddCardGetSMS;//添加完信用卡需要去获取短信
        CreditCardDAL dAL = new CreditCardDAL();
        public CreditCardList ()
		{
			InitializeComponent ();
            var data = dAL.FindList(1, 20);
            CreditCard.Clear();
            foreach (var item in data)
            {
                CreditCard.Add(item);
            }
            list.ItemsSource = CreditCard;
            
        }

        private async void List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var creditCard = e.SelectedItem as CreditCard;
            if (creditCard==null)
            {
                return;
            }
            CreditCardDetail creditCardDetail = new CreditCardDetail();
            creditCardDetail.CreditCard = creditCard;
            await Navigation.PushAsync(creditCardDetail);
            list.SelectedItem = null;
        }

        private async Task MenuItem_OnClicked(object sender, EventArgs e)
        {
            AddCreditCard addCredit = new AddCreditCard();
            addCredit.IsAdd = true;
            addCredit.AddAction += AddCredit_AddAction;
            await Navigation.PushAsync(addCredit);
        }
        private async Task Clear_OnClicked(object sender, EventArgs e)
        {
           var b= await DisplayAlert("提示", "确定删除所有数据?", "确定", "取消");
            if (b)
            {
                MessageList.BillList.Clear();
                CreditCardList.CreditCard.Clear();
                SQLiteAccessHelper.DeleteTable();
            }
        }
        private void AddCredit_AddAction(object sender, EventArgs e)
        {
            var model = (CreditCard)sender;
            model.AddTime = DateTime.Now;
            
            dAL.Add(model);
            CreditCard.Add(model);
            var timeStamp =  TimeHelper.GetTimeStamp(DateTime.Now.Date.AddDays(-60));
            AddCardGetSMS?.Invoke(model.BankPhone, timeStamp);
        }
    }
}