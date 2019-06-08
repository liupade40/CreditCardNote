using CreditCardNote.Infrastructrue.Common;
using CreditCardNote.SQLite.Model;
using CreditCardNote.SQLite.SQLiteHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCardNote.SQLite.DAL
{
    public class SMSTimeDAL
    {
        public int Add(SMSTime model)
        {
            return SQLiteAccessHelper.Add<SMSTime>(model);
        }
        public string FindTimeStamp()
        {
            var data = SQLiteAccessHelper.Find<SMSTime>(x => x.Id == 1);
            if (data == null)
            {
                Add(new SMSTime() { GetDateTime = TimeHelper.GetTimeStamp(DateTime.Now) });
                return TimeHelper.GetTimeStamp(DateTime.Now.Date.AddDays(-60));
            }
            else
            {
                Update();
                return data.GetDateTime;
            }
        }
        public void Update()
        {
            SQLiteAccessHelper.Update<SMSTime>(new SMSTime() { Id = 1, GetDateTime = TimeHelper.GetTimeStamp(DateTime.Now) });
        }
    }
}
