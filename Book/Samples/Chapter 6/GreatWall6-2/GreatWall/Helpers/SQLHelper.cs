using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;

namespace GreatWall.Helpers
{
    public static class SQLHelper
    {
        private const string ConnectString = "Server=tcp:greatwalldbserver.database.windows.net,1433;Initial Catalog = greatwalldb; Persist Security Info=False;User ID = winkey; Password=!greatwall1004;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";

        public static DataSet RunSQL(string query)
        {
            DataSet ds = new DataSet();

            SqlConnection con = new SqlConnection(ConnectString);
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(ds);

            return ds;
        }
    }
}