using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCardNote.SQLite.Model
{
    /// <summary>
    /// 记录上次读取短信的时间，下次读取数据在本次时间以后的短信
    /// </summary>
    public class SMSTime
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string GetDateTime { get; set; }
    }
}
