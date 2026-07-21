using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CollegeWebPortal.DAL
{
    /// <summary>
    /// Central ADO.NET data-access helper (Data Access Layer).
    /// Demonstrates SqlConnection, SqlCommand, SqlDataAdapter, DataReader,
    /// DataSet, stored procedures and fully parameterized queries.
    /// The connection string is read from Web.config.
    /// </summary>
    public static class DBHelper
    {
        /// <summary>Connection string stored in Web.config &lt;connectionStrings&gt;.</summary>
        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["CollegePortalDB"].ConnectionString; }
        }

        /// <summary>Creates a new SqlConnection.</summary>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        private static SqlCommand BuildCommand(SqlConnection cn, string commandText,
            bool isStoredProcedure, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(commandText, cn);
            cmd.CommandType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            return cmd;
        }

        /// <summary>Runs a query/proc and returns the results as a DataTable (via SqlDataAdapter).</summary>
        public static DataTable ExecuteDataTable(string commandText, bool isStoredProcedure = false,
            params SqlParameter[] parameters)
        {
            DataTable table = new DataTable();
            using (SqlConnection cn = GetConnection())
            using (SqlCommand cmd = BuildCommand(cn, commandText, isStoredProcedure, parameters))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                adapter.Fill(table);
            }
            return table;
        }

        /// <summary>Runs a query/proc and returns a full DataSet (demonstrates DataSet usage).</summary>
        public static DataSet ExecuteDataSet(string commandText, bool isStoredProcedure = false,
            params SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cn = GetConnection())
            using (SqlCommand cmd = BuildCommand(cn, commandText, isStoredProcedure, parameters))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                adapter.Fill(ds);
            }
            return ds;
        }

        /// <summary>
        /// Returns an open DataReader. The connection is closed automatically
        /// when the reader is disposed (CommandBehavior.CloseConnection).
        /// </summary>
        public static SqlDataReader ExecuteReader(string commandText, bool isStoredProcedure = false,
            params SqlParameter[] parameters)
        {
            SqlConnection cn = GetConnection();
            SqlCommand cmd = BuildCommand(cn, commandText, isStoredProcedure, parameters);
            cn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>Executes INSERT/UPDATE/DELETE and returns affected row count.</summary>
        public static int ExecuteNonQuery(string commandText, bool isStoredProcedure = false,
            params SqlParameter[] parameters)
        {
            using (SqlConnection cn = GetConnection())
            using (SqlCommand cmd = BuildCommand(cn, commandText, isStoredProcedure, parameters))
            {
                cn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>Executes a query and returns the first column of the first row.</summary>
        public static object ExecuteScalar(string commandText, bool isStoredProcedure = false,
            params SqlParameter[] parameters)
        {
            using (SqlConnection cn = GetConnection())
            using (SqlCommand cmd = BuildCommand(cn, commandText, isStoredProcedure, parameters))
            {
                cn.Open();
                return cmd.ExecuteScalar();
            }
        }
    }
}
