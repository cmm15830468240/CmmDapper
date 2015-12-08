using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace Dapper
{
    public class DbCon
    {

        private static string connStr = "Data Source=172.16.31.10;Database=TCInterVacationOVDResource;User ID=TCInterVacation;Password=TC#In(t*e#r2Va)cat^ion;MultipleActiveResultSets=True";
            //ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

        //获取Sql Server的连接数据库对象。SqlConnection
        public static SqlConnection GetSqlCon()
        {
            SqlConnection connection = new SqlConnection(connStr);
            return connection;
        }

        public string ConnectionString
        {
            get { return connStr; }
            set { connStr = value; }
        }
    }
}
