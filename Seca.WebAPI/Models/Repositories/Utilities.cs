using MySqlConnector;
using Seca.WebAPI.Models.Repositories.DAOException;

namespace Seca.WebAPI.Models.Repositories
{
    public class Utilities
    {
        public static Int32 GetMaxId(String tableName, String idTable, MySqlConnection connexion)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connexion;
                cmd.CommandText = "select ifnull(max(" + idTable + "),0) from " + tableName;
                reader = cmd.ExecuteReader();
                Int32 id = 0;
                if (reader.Read())  
                {
                    id = reader.GetInt32(0);
                }
                    reader.Close();
                return id+1;
            }
            catch (Exception ex)
                        {
                throw new RepoException();
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();
            }
        }
    }
}
