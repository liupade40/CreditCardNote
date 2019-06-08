using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCardNote.Infrastructrue.Common
{
    public class BankType
    {
        /// <summary>
        /// 银行列表
        /// </summary>
       public static List<string> BankTypeList = new List<string>()
            {
                "广发银行",
                "招商银行",
                "交通银行",
                "中信银行",
                "华夏银行",
                "民生银行"
            };
        /// <summary>
        /// 银行短信
        /// </summary>
        public static List<string> BankNameList = new List<string>()
            {
                "广发银行",
                "招商银行",
                "交通银行",
                "中信银行",
                "华夏信用卡",
                "民生银行"
            };
    }
}
