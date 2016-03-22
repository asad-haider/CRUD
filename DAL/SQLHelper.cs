namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using MySql.Data.MySqlClient;
    using System.Data;
    using System.Configuration;
    using System.Data.SqlClient;
    
    public class SQLHelper
    {
        string connString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

public void executenonquery(List<SqlParameter> parameters, string SPName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(SPName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (SqlParameter param in parameters)
                        {
                            cmd.Parameters.Add(param);
                        }

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public string executeScaler(List<SqlParameter> parameters, string SPName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(SPName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (SqlParameter param in parameters)
                        {
                            cmd.Parameters.Add(param);
                        }

                        con.Open();
                        return Convert.ToString(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public T executeSP<T>(List<SqlParameter> parameters, string SPName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(SPName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (SqlParameter param in parameters)
                        {
                            cmd.Parameters.Add(param);
                        }

                        if (typeof(T) == typeof(DataSet))
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataSet dset = new DataSet();
                            con.Open();
                            adapter.Fill(dset);
                            return (T)(object)dset;
                        }
                        else if (typeof(T) == typeof(int) || typeof(T) == typeof(string))
                        {
                            SqlParameter _ReturnValue = new SqlParameter("_ReturnValue", SqlDbType.Int);
                            _ReturnValue.Direction = System.Data.ParameterDirection.Output;
                            cmd.Parameters.Add(_ReturnValue);

                            con.Open();
                            cmd.ExecuteNonQuery();
                            return (T)_ReturnValue.Value;
                        }
                        else
                        {
                            return default(T);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }
}
