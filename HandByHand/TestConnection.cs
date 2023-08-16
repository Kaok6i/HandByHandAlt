using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandByHand
{
    internal class TestConnection
    {
        public bool ConnectionChecker()
        {
            using (var db = new Entities())
            {
                DbConnection conn = db.Database.Connection;
                try
                {
                    conn.Open();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
