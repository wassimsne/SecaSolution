using Seca.WebAPI.Models.Domaine;

namespace Seca.WebAPI.Models.Repositories.BorneRepo
{
    public interface IDAOProprietaire
    {
        List<Proprietaire> GetProprietaires();
        Proprietaire GetProprietaireById(int id);
        int AddProprietaire(Proprietaire proprietaire);
        bool UpdateProprietaire(Proprietaire proprietaire);
        void Deleteproprietaire(int idproprietaire);
    }
}
