namespace Seca.WebAPI.Models.Domaine
{
    public enum EtatBorne { Connected,Disconnected}
    public class Borne
    {
       
        public int Id { get; set; }
        public string? Ip { get; set; }
        public int Port { get; set; }
        public EtatBorne Etat { get; set; }
        public int? IdProp { get; set; }
    }
}
