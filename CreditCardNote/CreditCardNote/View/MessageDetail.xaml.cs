using CreditCardNote.SQLite.DAL;
using CreditCardNote.SQLite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CreditCardNote.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessageDetail : ContentPage
    {
        public Bill Bill { get; set; }
        public MessageDetail()
        {
            InitializeComponent();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = Bill;
            if (Bill != null && Bill.Id != 0)
            {
                CreditCardDAL creditCardDAL = new CreditCardDAL();
                btn.Text = creditCardDAL.Find(Bill.CreditCarkId).CardNumber;
            }
        }
        public async Task btn_Click(object sender, EventArgs e)
        {
            if (btn.Text != null)
            {
                CreditCardDAL creditCardDAL = new CreditCardDAL();
                var credit = creditCardDAL.Find(btn.Text);
                if (credit != null)
                {
                    CreditCardDetail creditCardDetail = new CreditCardDetail();
                    creditCardDetail.CreditCard = credit;
                    await Navigation.PushAsync(creditCardDetail);
                }
            }

        }
    }
}