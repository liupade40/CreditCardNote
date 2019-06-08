using CreditCardNote.PlatformInterface;
using CreditCardNote.SQLite.DAL;
using CreditCardNote.SQLite.Model;
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
    public partial class MessageList : ContentPage
    {
        public static ObservableCollection<Bill> BillList = new ObservableCollection<Bill>();
        int pageIndex = 1;
        BillDAL billDAL = new BillDAL();
        public MessageList()
        {
            InitializeComponent();
            list.ItemsSource = BillList;
            list.IsPullToRefreshEnabled = true;
            list.Refreshing += List_Refreshing;
            BillList.CollectionChanged += BillList_CollectionChanged;
            // BillList.CollectionChanged += BillList_CollectionChanged;
            BtnIsSHow();
        }
        
        private void BillList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            BtnIsSHow();
        }


        private void BtnIsSHow()
        {
            if (BillList.Count == 0)
            {
                btn.IsVisible = false;
            }
            else
            {
                btn.IsVisible = true;
            }
        }

        private async void List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var bill = e.SelectedItem as Bill;
            if (bill==null)
            {
                return;
            }
            MessageDetail messageDetail = new MessageDetail();
            messageDetail.Bill = bill;
            await Navigation.PushAsync(messageDetail);
            list.SelectedItem = null;
        }

        private void List_Refreshing(object sender, EventArgs e)
        {
            pageIndex = 1;
            BillList.Clear();
            AddData();
            list.EndRefresh();
        }
        private async void Help_OnClicked(object sender, EventArgs e)
        {
           await  DisplayAlert("提示", "1、添加信用卡。\r\n2、App会自动读取短信。\r\n3、可以查看短信，和信用卡的统计。\r\n4、暂时只支持招商、广发、交行银行短信识别，其他银行未测试。\r\n5、本App处于测试阶段，所有的数据可以删除，点击信用卡界面的X操作。", "确定");
        }
        private void Load(object sender, EventArgs e)
        {
            pageIndex++;
            AddData();
        }

        private void AddData()
        {
            var data = billDAL.FindList(pageIndex, 10);
            if (data.Count()==0)
            {
                btn.IsVisible = false;
            }
            else
            {
                btn.IsVisible = true;
            }
            foreach (var item in data)
            {
                BillList.Add(item);
            }

        }

        private async Task MenuItem_OnClicked(object sender, EventArgs e)
        {
            AddCreditCard addCredit = new AddCreditCard();
            addCredit.IsAdd = true;
            addCredit.AddAction += AddCredit_AddAction;
            await Navigation.PushAsync(addCredit);
        }

        private void AddCredit_AddAction(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 没有短信权限的提醒
        /// </summary>
        public static void NoSMSPermissionTip()
        {
            DependencyService.Get<IToast>().ShortAlert("没有获取短信的权限");
        }
    }

}