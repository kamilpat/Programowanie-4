using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porownywarka.Controler
{
  public  static class DatabaseConnection
  {
      public static string Connection= @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=E:\Programownie\SearchedEngineDataBase.mdf;Integrated Security = True; Connect Timeout = 30";
      public static LINQToSQLClassDataContext dc = new LINQToSQLClassDataContext(Connection);
    }
}
