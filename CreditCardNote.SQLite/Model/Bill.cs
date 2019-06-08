using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CreditCardNote.SQLite.Model
{
    /// <summary>
    /// 短信记录
    /// </summary>
    public class Bill
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        /// <summary>
        /// 信用卡id
        /// </summary>
        public int CreditCarkId { get; set; }
        /// <summary>
        /// 消费日期
        /// </summary>
        public string CostDate { get; set; }
        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 短信内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 尾号
        /// </summary>
        public string ShortNumber { get; set; }
        /// <summary>
        /// 银行
        /// </summary>
        public string BankType { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
}
