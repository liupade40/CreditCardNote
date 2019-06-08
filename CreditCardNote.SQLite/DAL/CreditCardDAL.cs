using CreditCardNote.SQLite.SQLiteHelper;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using CreditCardNote.SQLite.Model;

namespace CreditCardNote.SQLite.DAL
{
    public class CreditCardDAL
    {
        public int Add(CreditCard model)
        {
            return SQLiteAccessHelper.Add<CreditCard>(model);
        }
        public int Update(CreditCard model)
        {
            return SQLiteAccessHelper.Update<CreditCard>(model);
        }
        public CreditCard Find(int id)
        {
            return SQLiteAccessHelper.Find<CreditCard>(x => x.Id == id);
        }
        public CreditCard Find(string cardNumber)
        {
            return SQLiteAccessHelper.Find<CreditCard>(x => x.CardNumber == cardNumber);
        }
        public int Find(string shortNumber,string bankName)
        {
            var model = SQLiteAccessHelper.Find<CreditCard>(x => x.CardNumber.Contains(shortNumber) && x.BankName == bankName);
            if (model!=null)
            {
                return model.Id;
            }
            return 0;
        }
        public TableQuery<CreditCard> FindList(int pageIndex, int pageSize)
        {
            TableQuery<CreditCard> query = SQLiteAccessHelper.FindList<CreditCard>((pageIndex - 1) * pageSize, pageSize).OrderByDescending<DateTime>(x => x.AddTime);
            return query;
        }
        public int Delete(int id)
        {
            return SQLiteAccessHelper.Delete<CreditCard>(id);
        }
        public void DeleteAll()
        {
            SQLiteAccessHelper.DeleteAll<CreditCard>();
        }
        public string FindAllBankPhone()
        {
            TableQuery<CreditCard> list = SQLiteAccessHelper.FindList<CreditCard>(1, 20);
            if (list.Count() == 0)
            {
                return null;
            }
            string str = "";
            int i = 0;
            foreach (var item in list)
            {

                if (i != 0)
                {
                    str += ",";
                }
                else
                {
                    i++;
                }
                str += item.BankPhone;
            }
            return str;
        }
    }
}
