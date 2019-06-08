using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCardNote.SQLite
{
    public interface IDatabaseConnection
    {
        SQLiteConnection DbConnection();
    }
}
