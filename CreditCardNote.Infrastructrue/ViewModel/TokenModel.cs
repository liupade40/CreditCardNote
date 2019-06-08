using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCardNote.Infrastructrue.ViewModel
{
    public class TokenModel
    {
        public string access_token { get; set; }
        public string session_key { get; set; }
        public string scope { get; set; }
        public string refresh_token { get; set; }
        public string session_secret { get; set; }
        public int expires_in { get; set; }
    }
}
