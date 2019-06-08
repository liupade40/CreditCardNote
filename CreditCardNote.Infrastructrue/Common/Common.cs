using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCardNote.Infrastructrue.Common
{
    public static class Common
    {
        public static bool ModelIsNull<T>(T model)
        {
            Type type = model.GetType(); //获取类型
            foreach (var item in type.GetProperties())
            {
                object value = item.GetValue(model);
                if (value == null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
