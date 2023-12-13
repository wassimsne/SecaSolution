

using MySqlConnector;

namespace Seca.WebAPI.Models.Repositories.Connexion
{
    public class ConnectionManager : IDisposable
    {
        private static ConnectionManager connectionInstance;
        public static MySqlConnection _connexion;


        public MySqlConnection Connexion {get { return _connexion; } set { _connexion = value; }        }



        private ConnectionManager()
        {

            _connexion = new MySqlConnection();

        }



        public static ConnectionManager GetConnectionManagerInstance(String connectionString)
        {

            if (connectionInstance == null)
            {
                try
                {
                    connectionInstance = new ConnectionManager();
                    _connexion.ConnectionString = connectionString;
                    _connexion.Open();
                }
                catch (Exception ex)
                {

                }
            }



            return connectionInstance;

        }


        public void Dispose()
        {
            this.Dispose();
        }
    }
}
