using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Seca.WebAPI.Models.Domaine;
using Seca.WebAPI.Models.Repositories.BorneRepo;
using Seca.WebAPI.Models.Repositories.Connexion;

namespace Seca.WebAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class BorneController : Controller
    {

        IDAOBorne _DBBorne;
        public BorneController(IDAOBorne dbBorne)
        {

            _DBBorne = dbBorne;
            
        }

        [HttpPost]
        public IActionResult AjouterBorne(Borne borne)
        {

            
            return Ok(_DBBorne.AddBorne(borne));

        }
        [HttpGet]
        public IActionResult RecupererBorne()
        {

            return Ok(_DBBorne.GetBornes());

        }
        [HttpGet("{idborne:int}")]
        public IActionResult Getbyid(int idborne)
        {
            
            return Ok(_DBBorne.GetBorneById(idborne));
        }

        [HttpPut]
        [Route("/Update")]
        public async Task<IActionResult> Update( Borne borne)
        {

            _DBBorne.UpdateBorne(borne);

            return Ok(borne); /*NoContent();*/
        }

        [HttpDelete("{idborne:int}")]
        public IActionResult Delete(int idborne)
        {

            _DBBorne.DeleteBorne(idborne);

           
            return NoContent();
        }

        public class AffectationModel
        {
            public int IdProprietaire { get; set; }
            public List<Borne> Bornes { get; set; }
        }


       
        [HttpPut]
        [Route("/Affectation")]
        public IActionResult Affectation([FromBody] AffectationModel model)
        {
            if (model == null || model.Bornes == null)
            {
                return BadRequest("Les données de la requête sont invalides.");
            }

            _DBBorne.AffectationBorne_Prop(model.Bornes, model.IdProprietaire);

            return NoContent();
        }

    }
}
