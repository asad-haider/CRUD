using System;
using System.Collections.Generic;
using DAL;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace Logging
{
    public class clsLogging
    {
        public void WriteLog(Exception ex)
        {
            SQLHelper sqlHelper = new SQLHelper();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("_DateTime", DateTime.Now));
            parameters.Add(new SqlParameter("_ErrorMessage", ex.Message));
            parameters.Add(new SqlParameter("_ErrorStack", ex.StackTrace));

            sqlHelper.executeSP<int>(parameters, "InsertLog");
        }
    }
}
