using CreditCardNote.SQLite.SQLiteHelper;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using CreditCardNote.SQLite.Model;

namespace CreditCardNote.SQLite.DAL
{
    public class BillDAL
    {
        public bool Add(Bill model)
        {
            CreditCardDAL billDAL = new CreditCardDAL();
            int id= billDAL.Find(model.ShortNumber, model.BankType);
            if (id!=0)
            {
                model.CreditCarkId = id;
                SQLiteAccessHelper.Add<Bill>(model);
                return true;
            }
            return false;
        }
        public int Update(Bill model)
        {
            return SQLiteAccessHelper.Update<Bill>(model);
        }
        public Bill Find(int id)
        {
            return SQLiteAccessHelper.Find<Bill>(x => x.Id == id);
        }
        public decimal Statistics(int cardId,DateTime start,DateTime end)
        {
            var data= SQLiteAccessHelper.FindList<Bill>().Where(x => x.CreditCarkId == cardId && x.AddTime >= start && x.AddTime <= end);
            decimal money = 0;
            foreach (var item in data)
            {
                money += item.Money;
            }
            return money;
        }
        public TableQuery<Bill> FindList(int pageIndex, int pageSize)
        {
            TableQuery<Bill> query = SQLiteAccessHelper.FindList<Bill>(pageIndex, pageSize).OrderByDescending<DateTime>(x=>x.AddTime);
            return query;
        }
        public int Delete(int id)
        {
            return SQLiteAccessHelper.Delete<Bill>(id);
        }
        public void DeleteAll()
        {
            SQLiteAccessHelper.DeleteAll<Bill>();
        }
    }
}
