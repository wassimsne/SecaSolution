using MySqlConnector;
using Seca.WebAPI.Models.Domaine;
using Seca.WebAPI.Models.Repositories.Connexion;
using Seca.WebAPI.Models.Repositories.DAOException;
using System.Data;

namespace Seca.WebAPI.Models.Repositories.BorneRepo
{
    public class ProprietaireRepoImp:IDAOProprietaire
    {
        MySqlConnection _connection;
        MySqlCommand _command;
        IConfiguration _configuration;
        MySqlDataReader _cureseur;
        public ProprietaireRepoImp(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = ConnectionManager.GetConnectionManagerInstance(configuration.GetConnectionString("Default")).Connexion;
            _command = new MySqlCommand();
            _command.Connection = _connection;
        }

        public int AddProprietaire(Proprietaire proprietaire)
        {

            try
            {
                _command.Parameters.Clear();

                if (_connection.State == System.Data.ConnectionState.Closed || _connection.State == System.Data.ConnectionState.Broken)
                    _connection.Open();
                int id = Utilities.GetMaxId("proprietaire", "idproprietaire", _connection);
                _command.CommandText = "INSERT INTO proprietaire(idproprietaire,nom,prenom,login,password)"
                                + " VALUES(@IdidProprietaire,@Nom,@Prenom,@Login,@Password)";

                _command.Parameters.AddWithValue("@IdidProprietaire", id);
                _command.Parameters.AddWithValue("@Nom", proprietaire.Nom);
                _command.Parameters.AddWithValue("@Prenom", proprietaire.Prenom);
                _command.Parameters.AddWithValue("@Login", proprietaire.Login);
                _command.Parameters.AddWithValue("@Password", proprietaire.Password);


                _command.ExecuteNonQuery();
                return id;

            }
            catch (MySqlException except)
            {
                throw new RepoException();
            }
            catch (Exception except)
            {
                throw except;
            }
            finally
            {



            }
        }

        public Proprietaire GetProprietaireById(int id)
        {
            try
            {
                _command.Parameters.Clear();
                if (_connection.State == System.Data.ConnectionState.Closed || _connection.State == System.Data.ConnectionState.Broken)
                    _connection.Open();
                Proprietaire proprietaire = new Proprietaire();
                _command.CommandText = "SELECT * FROM proprietaire WHERE idproprietaire = @id"; // Utilisation d'un paramètre pour éviter les injections SQL
                _command.Parameters.AddWithValue("@id", id); // Paramètre pour l'identifiant de la borne
                _command.Connection = _connection;

                using (MySqlDataReader _cureseur = _command.ExecuteReader())
                {
                    if (_cureseur.Read())
                    {
                        proprietaire.IdProprietaire = _cureseur.GetInt16(0);
                        proprietaire.Nom = _cureseur.GetString(1);
                        proprietaire.Prenom = _cureseur.GetString(2);
                        proprietaire.Login = _cureseur.GetString(3);
                        proprietaire.Password = _cureseur.GetString(4);


                    }
                }

                return proprietaire;
            }
            catch (MySqlException)
            {
                throw new RepoException();
            }
            catch (Exception)
            {
                throw new RepoException();
            }
            finally
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close(); // Fermeture de la connexion si elle est ouverte
                }
            }
        }

        public void Deleteproprietaire(int idproprietaire)
        {
            try
            {
                _command.Parameters.Clear();
                if (_connection.State == System.Data.ConnectionState.Closed || _connection.State == System.Data.ConnectionState.Broken)
                    _connection.Open();

                string req = "DELETE FROM proprietaire WHERE idproprietaire = @idProprietaire"; // Utilisation de paramètres pour éviter les injections SQL
                _command.CommandText = req;

                // Ajout du paramètre idBorne
                _command.Parameters.AddWithValue("@idProprietaire", idproprietaire);

                _command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new RepoException();
            }
        }
        public bool UpdateProprietaire(Proprietaire proprietaire)
        {
            try
            {
                _command.Parameters.Clear();
                String req = "update  proprietaire set nom=@Nom,prenom=@Prenom,login=@Login,password=@Password" +
                    "        where idproprietaire =" + proprietaire.IdProprietaire;
                _command.Parameters.AddWithValue("@Nom", proprietaire.Nom);
                _command.Parameters.AddWithValue("@Prenom", proprietaire.Prenom);
                _command.Parameters.AddWithValue("@Login", proprietaire.Login);
                _command.Parameters.AddWithValue("@Password", proprietaire.Password);


                _command.CommandText = req;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new RepoException();
            }
        }

        public List<Proprietaire> GetProprietaires()
        {
            try
            {
                List<Proprietaire> listeProprietaire = new List<Proprietaire>();

                MySqlCommand cmd = new MySqlCommand();
                if (_connection.State == System.Data.ConnectionState.Closed || _connection.State == System.Data.ConnectionState.Broken)
                    _connection.Open();
                cmd.CommandText = "select * from proprietaire ";
                cmd.Connection = _connection;
                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable tabProprietaire = new DataTable();
                tabProprietaire.Load(reader);
                reader.Close();
                foreach (DataRow row in tabProprietaire.Rows)
                {
                    Proprietaire proprietaire = new Proprietaire();
                    proprietaire.IdProprietaire = Convert.ToInt32(row[0]);
                    proprietaire.Nom = Convert.ToString(row[1]);
                    proprietaire.Prenom = Convert.ToString(row[2]);
                    proprietaire.Login= Convert.ToString(row[3]);
                    proprietaire.Password= Convert.ToString(row[4]);

                    listeProprietaire.Add(proprietaire);
                }

                return listeProprietaire;
            }
            catch (MySqlException except)
            {
                throw new RepoException();
            }
            catch (Exception except)
            {
                throw new RepoException();
            }
            finally
            {



            }
        }
    }
}
