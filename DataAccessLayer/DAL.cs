using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IDAL<T> where T : class, new()
    {
        IEnumerable<T> GetData(string code, ICollection<KeyValuePair<string, Object>> parameters);
    }
    public class DAL<T> : IDAL<T> where T : class, new()
    {/// <summary>
     /// 
     /// </summary>
     /// <param name="code"> mapped name(see file) </param>
     /// <param name="parameters"> parameters for query or SP </param>
     /// <returns> IEnumerable of type T</returns>
        public IEnumerable<T> GetData(string code, ICollection<KeyValuePair<string, object>> parameters)
        {
            bool isSP = false; // whether we have query or stored procedure
            List<T> result = new List<T>(); // Data to return
            int IndexOfquery = -1; // the line of the name of query or SP
            String[] AllLines = new string[] { };
            String[] queryParameters = new String[] { }; // parameters
            String query = "";
            string path = @"D:\Queries.txt"; // file
            if (File.Exists(path))
            {
                AllLines = File.ReadAllLines(path);
            }
            for (int i = 0; i < AllLines.Length; i++)
            {
                if (AllLines[i].Contains("name:"))
                {
                    int index = AllLines[i].IndexOf(':');
                    if (AllLines[i].Substring(index + 1) == code)
                    { // finding the line of specified query
                        IndexOfquery = i;
                    }
                }
            }
            if (IndexOfquery >= 0)
            {
                // finding the type (SP or query)
                if (AllLines[IndexOfquery - 1].Substring(AllLines[IndexOfquery - 1].IndexOf(':') + 1) == "SP")
                    isSP = true;
                // if such a query exists

                String str = AllLines[IndexOfquery + 1].Substring(AllLines[IndexOfquery + 1].IndexOf(':') + 1);
                queryParameters = str.Split(',');
                query = AllLines[IndexOfquery + 2].Substring(AllLines[IndexOfquery + 2].IndexOf(':') + 1);
                if (!isSP)
                {
                    if (parameters != null)
                    {
                        for (int i = 0; i < queryParameters.Length; i++)
                        {
                            foreach (var param in parameters)
                            {
                                if (param.Key.ToUpper() == queryParameters[i].Replace("@", "").ToUpper())
                                {
                                    query = query.Replace(queryParameters[i], param.Value.ToString());
                                }
                            }
                        }
                    }
                }
            }
            Type type = typeof(T);
            // let's get all properties
            List<PropertyInfo> properties = type.GetProperties().ToList();
            // get mapping values
            List<String> mappedNames = new List<string>();
            for (int i = 0; i < properties.Count; i++)
            {
                DataNameAttribute attribute = (DataNameAttribute)Attribute.GetCustomAttribute(properties[i], typeof(DataNameAttribute));
                if (attribute != null)
                    mappedNames.Add(attribute.Value);
            }
            // writing column's values
            List<object> values;

            using (SqlConnection connection = new SqlConnection("Data Source = (local); Initial Catalog = Airport; Integrated Security = true; "))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                if (!isSP)
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = query;
                }
                else
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = query;
                    SqlCommandBuilder.DeriveParameters(command);
                    if (parameters != null)
                    {
                        foreach (SqlParameter param in command.Parameters)
                        {
                            foreach (var p in parameters)
                            {
                                if (p.Key.ToUpper() == param.ParameterName.Replace("@", "").ToUpper())
                                {
                                    param.Value = p.Value;
                                }
                            }
                        }
                    }
                }
                SqlDataReader rdr = null;
                // it is possible that we'll select only some of the columns
                List<PropertyInfo> matchedProperties = new List<PropertyInfo>();
                using (rdr = command.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        T instance = new T();
                        matchedProperties = new List<PropertyInfo>();
                        values = new List<object>();
                        for (int i = 0; i < mappedNames.Count; i++)
                        {
                            if (rdr.HasRows)
                            {
                                if (HasColumn(rdr, mappedNames[i])) //check to see what column is selected
                                {
                                    values.Add(rdr[mappedNames[i]]);
                                    matchedProperties.Add(properties[i]);
                                }
                            }
                        }

                        for (int i = 0; i < matchedProperties.Count; i++)
                        { // setting values to instance properties
                            matchedProperties[i].SetValue(instance, values[i], null);

                        }
                        result.Add(instance);
                    }
                }

            }
            return result;
        }
        /// <summary>
        ///  check whether we have columnName column returned in reader
        /// </summary>
        /// <param name="dr"> DataReader</param>
        /// <param name="columnName">Column name to compare</param>
        /// <returns></returns>
        public static bool HasColumn(IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}

