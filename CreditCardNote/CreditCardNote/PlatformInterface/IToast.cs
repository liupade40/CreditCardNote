﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCardNote.PlatformInterface
{
    public interface IToast
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
