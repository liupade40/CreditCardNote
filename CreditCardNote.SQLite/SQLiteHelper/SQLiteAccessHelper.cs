using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CreditCardNote.SQLite.Model;
using Xamarin.Forms;
namespace CreditCardNote.SQLite.SQLiteHelper
{

    public static class SQLiteAccessHelper
    {
        private static SQLiteConnection database = SQLiteConnection();

        public static SQLiteConnection SQLiteConnection()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            //database.DropTable<Bill>();
            //database.DropTable<CreditCard>();
            //database.DropTable<SMSTime>();
            database.CreateTable<Bill>();
            database.CreateTable<CreditCard>();
            database.CreateTable<SMSTime>();
            return database;
        }
        public static void DeleteTable()
        {

            database.DropTable<Bill>();
            database.DropTable<CreditCard>();
            database.DropTable<SMSTime>();

        }
        public static int Add<T>(T model)
        {
            return database.Insert(model, typeof(T));
        }
        public static int Update<T>(T model)
        {
            return database.Update(model, typeof(T));
        }
        public static T Find<T>(Expression<Func<T, bool>> where) where T : new()
        {
            return database.Table<T>().FirstOrDefault(where);
        }
        public static TableQuery<T> FindList<T>() where T : new()
        {
            return database.Table<T>();
        }
        public static TableQuery<T> FindList<T>(int pageIndex, int pageSize) where T : new()
        {
            return database.Table<T>().Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        public static int Delete<T>(int id)
        {
            return database.Delete<T>(id);
        }
        public static void DeleteAll<T>()
        {
            database.DropTable<T>();
            database.CreateTable<T>();
        }
    }
}
