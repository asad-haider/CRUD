using System.Collections.Generic;
using MySql.Data.MySqlClient;
using PROP;
using System.Data;
using System;
using System.Data.SqlClient;

namespace DAL
{
    public class DALCountry
    {
        public int CreateCountry(string countryName)
        {            
            SQLHelper sqlHelper = new SQLHelper();
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("_CountryName", countryName));

            return sqlHelper.executeSP<int>(parameters, "InsertCountry");
        }

        public List<PROPCountry> getAllCountry()
        {
            List<PROPCountry> countryList = new List<PROPCountry>();
            SQLHelper sqlHelper = new SQLHelper();
            List<SqlParameter> parameters = new List<SqlParameter>();
            var resultSet = sqlHelper.executeSP<DataSet>(parameters, "getAllCountry");


            PROPCountry country;
            foreach (DataRow drow in resultSet.Tables[0].Rows)
            {
                country = new PROPCountry(Convert.ToInt32(drow[0].ToString()) , drow[1].ToString());
                countryList.Add(country);
            }

            return countryList;
        }

        public List<PROPCountry> getCountry(string searchCountry)
        {
            List<PROPCountry> countryList = new List<PROPCountry>();
            SQLHelper sqlHelper = new SQLHelper();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("_CountryName", searchCountry));
            var resultSet = sqlHelper.executeSP<DataSet>(parameters, "SelectCountryByName");

            PROPCountry country;
            foreach (DataRow drow in resultSet.Tables[0].Rows)
            {
                country = new PROPCountry(Convert.ToInt32(drow[0].ToString()), drow[1].ToString());
                countryList.Add(country);
            }

            return countryList;
        }

        public int DeleteCountry(int countryID)
        {
            SQLHelper sqlHelper = new SQLHelper();
            List<SqlParameter> lstParameter = new List<SqlParameter>();
            lstParameter.Add(new SqlParameter("_CountryID", countryID));
            return sqlHelper.executeSP<int>(lstParameter, "DeleteCountry");                
        }

        public string GetCountryById(int countryID)
        {
            SQLHelper sqlHelper = new SQLHelper();
            List<SqlParameter> lstParameter = new List<SqlParameter>();
            lstParameter.Add(new SqlParameter("_CountryID", countryID));
            return sqlHelper.executeScaler(lstParameter, "SelectCountryByID");                
        }

        public void UpdateCountry(PROPCountry country)
        {
            SQLHelper sqlHelper = new SQLHelper();
            List<SqlParameter> lstParameter = new List<SqlParameter>();
            lstParameter.Add(new SqlParameter("_CountryID", country.CountryID));
            lstParameter.Add(new SqlParameter("_CountryName", country.CountryName));
            sqlHelper.executenonquery(lstParameter, "UpdateCountryName");
        }
    }
}
