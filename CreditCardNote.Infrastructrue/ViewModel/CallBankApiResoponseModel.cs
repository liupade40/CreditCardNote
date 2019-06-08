using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCardNote.Infrastructrue.ViewModel
{

    public class CallBankApiResoponseModel
    {
        public UInt64 log_id { get; set; }
        public Result result { get; set; }
        public string error_code { get; set; }
        public string error_msg { get; set; }
    }

    public class Result
    {
        public string bank_card_number { get; set; }
        public string bank_name { get; set; }
        public int bank_card_type { get; set; }
        
    }
}
