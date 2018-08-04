
using Kingdee.K3.TestConRunJob.PlugIn.service;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Kingdee.K3.WANYUE.PlugIn.service
{
    public class RemoteExcuteDataBase
    {
        public static string overChar = ";";

        private string remoteServerIP;
        private string databaseName;
        private string userID;
        private string password;
        private bool result;

        public bool Result
        {
            get
            {
                return result;
            }

            set
            {
                result = value;
            }
        }

        public RemoteExcuteDataBase(string remoteServerIP,string databaseName,string userID,string password) {
            this.remoteServerIP = remoteServerIP;
            this.databaseName = databaseName;
            this.userID = userID;
            this.password = password;
        }

        public string getConntionInfo(string remoteServerIP, string databaseName, string userID, string password) {
            
            StringBuilder sb = new StringBuilder();
            sb.Append("Server=").Append(remoteServerIP).Append(overChar);
            sb.Append("Database=").Append(databaseName).Append(overChar);
            sb.Append("uid=").Append(userID).Append(overChar);
            sb.Append("pwd=").Append(password).Append(overChar);
            return sb.ToString();
        }

        public ConnectionResult connectionToRemoteDatabase()
        {
            ConnectionResult connResult = new ConnectionResult();
            try
            {
                SqlConnection sqlconn = new SqlConnection(getConntionInfo(remoteServerIP, databaseName, userID, password));
                sqlconn.Open();
                connResult.Sqlconn = sqlconn;
                connResult.Sucessd = true;
            }
            catch(Exception e) {
                SystemLog.WriteSystemLog(e.ToString());
                connResult.Sucessd = false;
                return connResult;
            }
            return connResult;
        }

        public ExcuteDataBaseResult excuteStatement(string sqlStatement, SqlConnection sqlConn) {
            ExcuteDataBaseResult excuteResult = new ExcuteDataBaseResult();
            try
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, sqlConn);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                excuteResult.Sucessd = true;
                excuteResult.Ds = ds;
            }
            catch (Exception e) {
                SystemLog.WriteSystemLog(e.ToString());
                excuteResult.Sucessd = false;
            }
            
            return excuteResult;
        }

        public bool closeConnection(SqlConnection sqlConn) {
            try {
                sqlConn.Close();
            } catch (Exception e) {
                SystemLog.WriteSystemLog(e.ToString());
                return false;
            }
            return true;
        }


    }
}
