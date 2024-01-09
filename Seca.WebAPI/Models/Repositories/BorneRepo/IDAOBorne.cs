using Seca.WebAPI.Models.Domaine;

namespace Seca.WebAPI.Models.Repositories.BorneRepo
{
    public interface IDAOBorne
    {
        List<Borne> GetBornes();
        Borne GetBorneById(int id);
        int AddBorne(Borne borne);
        bool UpdateBorne(int id,Borne borne);
        void DeleteBorne(int idborne);
        bool AffectationBorne_Prop(List<Borne> Bornes,int idprop);


    }
}
