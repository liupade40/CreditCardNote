using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using CreditCardNote.SQLite;
using CreditCardNote.SQLite.DAL;
using CreditCardNote.SQLite.Model;
using SQLite;

namespace CreditCardNote
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}
        public void add(object sender,EventArgs e)
        {
            MessagingCenter.Send(this, "Hi");
        }
        public void query(object sender, EventArgs e)
        {
            CreditCardDAL dal = new CreditCardDAL();
            TableQuery<CreditCard> list = dal.FindList(1, 10);
        }
        public void delete(object sender, EventArgs e)
        {
            CreditCardDAL dal = new CreditCardDAL();
            dal.DeleteAll();
        }
    }
}
