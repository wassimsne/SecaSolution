using Microsoft.AspNetCore.Mvc;
using Seca.WebAPI.Models.Domaine;
using Seca.WebAPI.Models.Repositories.BorneRepo;

namespace Seca.WebAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProprietaireController : Controller
    {
        IDAOProprietaire _DBProprietaire;
        public ProprietaireController(IDAOProprietaire dbProprietaire)
        {

            _DBProprietaire = dbProprietaire;

        }

        [HttpPost]
        public IActionResult AjouterProprietaire(Proprietaire proprietaire)
        {

            _DBProprietaire.AddProprietaire(proprietaire);
            return Ok();

        }
        [HttpGet]
        public IActionResult RecupererProprietaire()
        {

            return Ok(_DBProprietaire.GetProprietaires());

        }
        [HttpGet("{idproprietaire:int}")]
        public IActionResult Getbyid(int idproprietaire)
        {

            return Ok(_DBProprietaire.GetProprietaireById(idproprietaire));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Proprietaire proprietaire)
        {

            _DBProprietaire.UpdateProprietaire(proprietaire);

            return Ok(proprietaire); /*NoContent();*/
        }

        [HttpDelete("{idproprietaire:int}")]
        public IActionResult Delete(int idproprietaire)
        {

            _DBProprietaire.Deleteproprietaire(idproprietaire);


            return NoContent();
        }

        

    }
}
