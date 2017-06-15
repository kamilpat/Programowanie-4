using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Porownywarka.Controler
{
  public  static class DatabaseConnection
  {
      private static readonly string  path=Path.GetFullPath(@"..\..\..\..\..\SearchedEngineDataBase.mdf");
      private static string Connection= @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename="+path+";Integrated Security = True; Connect Timeout = 30";
      public static LINQToSQLClassDataContext dc = new LINQToSQLClassDataContext(Connection);
    }
}
