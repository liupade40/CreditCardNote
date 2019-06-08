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
    public partial class CreditCardDetail : ContentPage
    {
        public CreditCard CreditCard { get; set; }
        public CreditCardDetail()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = CreditCard;
            BillDAL billDAL = new BillDAL();

            DateTime now = DateTime.Now;

            DateTime start = new DateTime(now.AddMonths(-1).Year, now.AddMonths(-1).Month, CreditCard.StatementDate + 1).Date;//账单开始日
            DateTime end = new DateTime(now.Year, now.Month, CreditCard.StatementDate).AddDays(1).AddSeconds(-1); //账单结束日
            if (now <= end)//还没出账单,计算本月和上个月的消费
            {
                decimal cost = billDAL.Statistics(CreditCard.Id, start, end);
                Statement.Text = $"{start.ToString("MM月dd日")}至{end.ToString("MM月dd日")}消费:{cost}元";
                if (CreditCard.StatementDate < CreditCard.RepaymentDate)
                {
                    Repayment.Text = $"还款日{now.Month}月{CreditCard.RepaymentDate}日";
                }
                else
                {
                    Repayment.Text = $"还款日{now.AddMonths(1).Month}月{CreditCard.RepaymentDate}日";
                }

            }
            else//已出账单,计算本月和下个月的消费.
            {
                decimal cost = billDAL.Statistics(CreditCard.Id, start, end);
                if (CreditCard.StatementDate < CreditCard.RepaymentDate)
                {
                    Repayment.Text = $"{now.Month}月{CreditCard.RepaymentDate}日需还款:{cost}元";
                }
                else
                {
                    Repayment.Text = $"{now.AddMonths(1).Month}月{CreditCard.RepaymentDate}日需还款:{cost}元";
                }
                //Repayment.Text = $"{now.Month}月{CreditCard.RepaymentDate}日需还款:{cost}";
                start = new DateTime(now.Year, now.Month, CreditCard.StatementDate + 1).Date;//账单开始日
                end = new DateTime(now.AddMonths(+1).Year, now.AddMonths(+1).Month, CreditCard.StatementDate).AddDays(1).AddSeconds(-1); //账单结束日
                decimal cost2 = billDAL.Statistics(CreditCard.Id, start, end);
                Statement.Text = $"{start.ToString("MM月dd日")}至{end.ToString("MM月dd日")}消费:{cost2}元";

            }


        }

    }
}