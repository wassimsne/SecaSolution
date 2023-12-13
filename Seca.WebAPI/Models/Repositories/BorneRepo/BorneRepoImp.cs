using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Seca.WebAPI.Models.Domaine;
using Seca.WebAPI.Models.Repositories.Connexion;
using Seca.WebAPI.Models.Repositories.DAOException;
using System.Data;

namespace Seca.WebAPI.Models.Repositories.BorneRepo
{
    public class BorneRepoImp : IDAOBorne
    {
        MySqlConnection _connection;
        MySqlCommand _command;
        IConfiguration _configuration;
        MySqlDataReader _cureseur;
        public BorneRepoImp(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = ConnectionManager.GetConnectionManagerInstance(configuration.GetConnectionString("Default")).Connexion;
            _command = new MySqlCommand();
            _command.Connection = _connection;
        }

        public int AddBorne(Borne borne)
        {

            try
            {
                _command.Parameters.Clear();

                if (_connection.State == System.Data.ConnectionState.Closed || _connection.State == System.Data.ConnectionState.Broken)
                    _connection.Open();
                int id = Utilities.GetMaxId("borne", "idborne", _connection);
                _command.CommandText = "INSERT INTO borne(idborne,ip,port,etat)"
                                + " VALUES(@Id,@IP,@Port,@Etat)";

                _command.Parameters.AddWithValue("@Id", id);
                _command.Parameters.AddWithValue("@IP", borne.Ip);
                _command.Parameters.AddWithValue("@Port", borne.Port);
                _command.Parameters.AddWithValue("@Etat", borne.Etat.ToString());

               
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

        public Borne GetBorneById(int id)
        {
            try 
            {
                _command.Parameters.Clear();
                if (_connection.State == System.Data.ConnectionState.Closed || _connection.State == System.Data.ConnectionState.Broken)
                    _connection.Open();
                Borne borne = new Borne();
        _command.CommandText = "SELECT * FROM borne WHERE idborne = @id"; // Utilisation d'un paramètre pour éviter les injections SQL
        _command.Parameters.AddWithValue("@id", id); // Paramètre pour l'identifiant de la borne
        _command.Connection = _connection;

        using (MySqlDataReader _cureseur = _command.ExecuteReader())
        {
            if (_cureseur.Read())
            {
                borne.Id = _cureseur.GetInt16(0);
                borne.Ip = _cureseur.GetString(1);
                borne.Port = _cureseur.GetInt16(2);
                string etatTexte = _cureseur.GetString(3); // Supposons que la colonne 3 représente l'état de la borne

                if (Enum.TryParse(etatTexte, out EtatBorne etatParsed))
                {
                    borne.Etat = etatParsed;
                }
            }
        }

        return borne;
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

        public void DeleteBorne(int idborne)
        {
            try
            {
                _command.Parameters.Clear();
                if (_connection.State == System.Data.ConnectionState.Closed || _connection.State == System.Data.ConnectionState.Broken)
                    _connection.Open();

                string req = "DELETE FROM borne WHERE idborne = @idBorne"; // Utilisation de paramètres pour éviter les injections SQL
                _command.CommandText = req;

                // Ajout du paramètre idBorne
                _command.Parameters.AddWithValue("@idBorne", idborne);

                _command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new RepoException();
            }
        }
        public bool UpdateBorne(Borne borne)
        {
            try
            {
                _command.Parameters.Clear();
                String req = "update  borne set ip=@IP,port=@Port,etat=@Etat" +
                    "        where idborne =" + borne.Id;
                _command.Parameters.AddWithValue("@IP", borne.Ip);
                _command.Parameters.AddWithValue("@Port", borne.Port);
                _command.Parameters.AddWithValue("@Etat", borne.Etat.ToString());


                _command.CommandText = req;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new RepoException();
            }
        }

        public List<Borne> GetBornes()
        {
            try
            {
                List<Borne> listeBorne = new List<Borne>();

                MySqlCommand cmd = new MySqlCommand();
                if (_connection.State == System.Data.ConnectionState.Closed || _connection.State == System.Data.ConnectionState.Broken)
                    _connection.Open();
                cmd.CommandText = "select * from borne ";
                cmd.Connection = _connection;
                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable tabBorne = new DataTable();
                tabBorne.Load(reader);
                reader.Close();
                foreach (DataRow row in tabBorne.Rows)
                {
                    Borne newBorne = new Borne();
                    newBorne.Id = Convert.ToInt32(row[0]);
                    newBorne.Ip= Convert.ToString(row[1]);
                    newBorne.Port = Convert.ToInt32(row[2]);
                    EtatBorne.TryParse(Convert.IsDBNull(row[3]) ? null : (String)(row[3]),true, out EtatBorne result);
                    newBorne.Etat = result;
                   
                    listeBorne.Add(newBorne);
                }

                return listeBorne;
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

       public bool AffectationBorne_Prop(List<Borne> Bornes, int idprop)
        {
            try
            {
                if (_connection.State == System.Data.ConnectionState.Closed || _connection.State == System.Data.ConnectionState.Broken)
                _connection.Open();
                _command.Connection = _connection;

                foreach (var borne in Bornes)
                {
                    _command.Parameters.Clear();
                    String req = "update  borne set idproprietaire=@id where idBorne = @idb";
                    _command.Parameters.AddWithValue("@id", idprop);
                    _command.Parameters.AddWithValue("@idb", borne.Id);
                    _command.CommandText = req;
                    _command.ExecuteNonQuery();
                }
                    return true;



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
