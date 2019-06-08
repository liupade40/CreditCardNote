using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CreditCardNote.SQLite.Model
{
    /// <summary>
    /// 信用卡表
    /// </summary>
    public class CreditCard : INotifyPropertyChanged
    {
        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _username;
        /// <summary>
        /// 用户名
        /// </summary>

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        private string _cardNumber;
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNumber
        {
            get { return _cardNumber; }
            set
            {
                _cardNumber = value;
                OnPropertyChanged();
            }
        }

        private string _bankName;
        /// <summary>
        /// 银行
        /// </summary>
        public string BankName
        {
            get { return _bankName; }
            set
            {
                _bankName = value;
                OnPropertyChanged();
            }
        }

        private string _cardEndDate;
        /// <summary>
        /// 信用卡有效期
        /// </summary>
        public string CardEndDate
        {
            get { return _cardEndDate; }
            set
            {
                _cardEndDate = value;
                OnPropertyChanged();
            }
        }

        private int _statementDate;
        /// <summary>
        /// 账单日
        /// </summary>
        public int StatementDate
        {
            get { return _statementDate; }
            set
            {
                _statementDate = value;
                OnPropertyChanged();
            }
        }

        private int _repaymentDate;
        /// <summary>
        /// 还款日
        /// </summary>
        public int RepaymentDate
        {
            get { return _repaymentDate; }
            set
            {
                _repaymentDate = value;
                OnPropertyChanged();
            }
        }

        private string _bankPhone;
        /// <summary>
        /// 银行发送消费短信的号码
        /// </summary>
        public string BankPhone
        {
            get { return _bankPhone; }
            set
            {
                _bankPhone = value;
                OnPropertyChanged();
            }
        }

        private DateTime _addTime;
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime
        {
            get { return _addTime; }
            set
            {
                _addTime = value;
                OnPropertyChanged();
            }
        }

        private int _bankCardType;
        /// <summary>
        /// 银行卡类型，1: 借记卡; 2: 信用卡,暂时默认为信用卡
        /// </summary>
        public int BankCardType
        {
            get { return 2; }
            set
            {
                _bankCardType = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
