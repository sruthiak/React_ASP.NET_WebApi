using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace ToDoAppAPI.Db
{
    public class DbManager
    {
        public static List<Note> GetNotes(string connectionString)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    //initially throwed error.
                    //{"Only the invariant culture is supported in globalization-invariant mode.
                    //See https://aka.ms/GlobalizationInvariantMode for more information.
                    //(Parameter 'name')\r\nen-us is an invalid culture identifier."}
                    //

                    //This is not a bug. It is by design. SqlClient is dependent on the Globalization libraries and can throw unexpected errors when it is not present.
                    //Thus the exception, "Globalization Invariant Mode is not supported."

                    //Fix - Go to .csproj file- go to <InvariantGlobalization> and make it false

                    connection.Open();
                    using (var command = new SqlCommand("usp_GetNotes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (var dataAdapter = new SqlDataAdapter(command))
                        {
                            using (DataTable dataTable = new DataTable())
                            {
                               
                                dataAdapter.Fill(dataTable);
                                List<Note> notes = new List<Note>();
                                notes = dataTable.AsEnumerable().Select(row => new Note()
                                {
                                    Id = row.Field<long>("id"),
                                    Description = String.IsNullOrEmpty(row.Field<string>("description"))
                                    ? "not found" : row.Field<string>("description")
                                }).ToList();
                                return notes;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static void AddNotes(string connectionString,string desc)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    //initially throwed error.
                    //{"Only the invariant culture is supported in globalization-invariant mode.
                    //See https://aka.ms/GlobalizationInvariantMode for more information.
                    //(Parameter 'name')\r\nen-us is an invalid culture identifier."}
                    //

                    //This is not a bug. It is by design. SqlClient is dependent on the Globalization libraries and can throw unexpected errors when it is not present.
                    //Thus the exception, "Globalization Invariant Mode is not supported."

                    //Fix - Go to .csproj file- go to <InvariantGlobalization> and make it false

                    connection.Open();
                    using (var command = new SqlCommand("usp_AddNotes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@note", SqlDbType.VarChar).Value = desc;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
