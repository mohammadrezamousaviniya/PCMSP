using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DataBaseConnector;

namespace PCMSP_MVC.DBConnect
{
    public class PDBC
    {
        private string _ConnectionString { set; get; }
        SqlConnection connection;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        private List<TransActionReport> _TransActionReports;
        public List<TransActionReport> transActionReports()
        {
            return _TransActionReports;
        }
        private bool _IsConnectionOpen = false;
        public void Connect()
        {
            try
            {
                connection.Open();
                _IsConnectionOpen = true;
            }
            catch { }
        }
        public void DC()
        {
            try
            {
                connection.Close();
                _IsConnectionOpen = false;
            }
            catch { }
        }
        /// <summary>
        /// Exception Object To show What Happened !
        /// </summary>
        private Exception _EXCReporter;
        public Exception GetException()
        {
            return _EXCReporter;
        }
        /// <summary>
        /// a Library for Mssql connections and actions
        /// version 0.01
        /// can connect to database and Do smt...
        /// </summary>
        public PDBC(string ASPArg, bool Using_Wconfig)
        {
            //For ASP.net
            try
            {
                if (Using_Wconfig)
                {

                    _ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[ASPArg].ConnectionString;
                    connection = new SqlConnection(_ConnectionString);
                }
                else
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(ASPArg);
                    string DBConnection = reader.ReadLine();
                    reader.Close();
                    connection = new SqlConnection(DBConnection);
                }
                _EXCReporter = null;
            }
            catch (Exception ex)
            {

                _EXCReporter = ex;
            }
        }
        /// <summary>
        /// a Library for Mssql connections and actions
        /// version 0.01
        /// can connect to database and Do smt...
        /// if using Parameter write your query like this
        /// "insert into Tablename (Parameter1,Parameter2) Values( @Parameter1 , @Parameter2)
        /// then create a list of ExcParameters that _KEY == @Parameter1 And _VALUE == value of Parameter1
        /// Use List<ExcParameters> to send SafeParameterInsert <3
        /// </summary>
        /// <param name="Query">QueryWithParameters</param>
        /// <param name="SafeParameterInsert">"@Parameter"</param>
        /// <returns></returns>
        public string Script(string Query, List<ExcParameters> SafeParameterInsert = null)
        {
            if (_IsConnectionOpen)
            {
                try
                {
                    cmd = new SqlCommand(Query, connection);
                    if (SafeParameterInsert != null)
                    {
                        cmd.CommandType = CommandType.Text;
                        for (int i = 0; i < SafeParameterInsert.Count; i++)
                        {
                            cmd.Parameters.AddWithValue(SafeParameterInsert[i]._KEY, SafeParameterInsert[i]._VALUE);
                        }
                        object result = cmd.ExecuteScalar();
                        if (result == null)
                            return "1";
                        else
                            return result.ToString();
                    }
                    else
                    {
                        object result = cmd.ExecuteScalar();
                        if (result == null)
                            return "1";
                        else
                            return result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    _EXCReporter = ex;
                    return ex.Message;
                }
            }
            return "0";
        }

        public DataTable Select(string Query)
        {
            if (_IsConnectionOpen)
            {
                DataTable dt = new DataTable();
                try
                {
                    adapter = new SqlDataAdapter(Query, connection);
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    _EXCReporter = ex;
                }
                return dt;
            }

            return new DataTable();
        }

        public bool InsertSQLFile(List<string> SqlListArry, string BeginTransactionName)
        {
            if (_IsConnectionOpen)
            {
                cmd = connection.CreateCommand();
                SqlTransaction sqltr;
                sqltr = connection.BeginTransaction(BeginTransactionName);
                cmd.Connection = connection;
                cmd.Transaction = sqltr;
                try
                {
                    _TransActionReports = new List<TransActionReport>();
                    for (int i = 0; i < SqlListArry.Count; i++)
                    {
                        TransActionReport obj = new TransActionReport();
                        cmd.CommandText = SqlListArry[i];
                        obj.RowsAffected = cmd.ExecuteNonQuery();
                        obj.SqlQuery = SqlListArry[i];
                        _TransActionReports.Add(obj);
                    }
                    try
                    {

                        sqltr.Commit();
                        return true;
                    }
                    catch (Exception ex2)
                    {
                        try
                        {
                            _TransActionReports.Add(new TransActionReport() { SqlQuery = "EX2", TransactionExeption = ex2 });
                            sqltr.Rollback();
                        }
                        catch (Exception ex3)
                        {
                            _TransActionReports.Add(new TransActionReport() { SqlQuery = "EX3", TransactionExeption = ex3 });
                        }
                    }

                }
                catch (Exception ex)
                {
                    _EXCReporter = ex;
                }
            }
            else
            {
                _EXCReporter = new Exception("Connection Is Close");
                return false;
            }
            return false;
        }



    }
}
