using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReversieISpelImplementatie.Model;

namespace ReversiRestApi.Controllers
{
    [Route("api/Spel")]
    [ApiController]
    public class SpelController : ControllerBase
    {
        private readonly ISpelRepository iRepository;

        public SpelController(ISpelRepository repository)
        {
            iRepository = repository;
        }


        // GET api/spel
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetSpelOmschrijvingenVanSpellenMetWachtendeSpeler()
        {
            List<Spel> spellen = iRepository.GetSpellen();
            List<string> omschrijvingen = new List<string>();

            foreach (Spel spel in spellen)
            {
                if (spel.Speler2Token == null)
                {
                    omschrijvingen.Add(spel.Omschrijving);
                }
            }

            return omschrijvingen;
        }

        [HttpGet("{spelToken}")]
        public ActionResult<Spel> GetSpelMetSpelToken(string spelToken)
        {
            Spel spel = iRepository.GetSpel(spelToken);
            if (spel == null)
            {
                return NotFound();
            }
            return spel;
        }

        //getSpel with spelerToken
        [HttpGet("speler/{spelerToken}")]
        public ActionResult<Spel> GetSpelMetSpelerToken(string spelerToken)
        {
            Spel spel = iRepository.GetSpelMetSpelerToken(spelerToken);
            if (spel == null)
            {
                return NotFound();
            }
            return spel;
        }

        [HttpGet("{token}/beurt")]
        public ActionResult<string> GetBeurt(string token)
        {
            Spel spel = iRepository.GetSpel(token);
            if (spel == null)
            {
                return NotFound();
            }


            return "test"; 
            // return spel.Beurt;
        }

        // POST create spel
        [HttpPost]
        public void PostNieuwSpel([FromBody] Spel_Info spelInfo)
        {
            // check if speler 1 token is empty and omschrijving empty, then make new spel
            if (spelInfo.Speler1Token != null && spelInfo.Omschrijving != null)
            {
                Spel spel = new Spel();
                spel.Speler1Token = spelInfo.Speler1Token;
                spel.Omschrijving = spelInfo.Omschrijving;
                spel.Token = Guid.NewGuid().ToString();
                iRepository.AddSpel(spel);
            }
        }
    }
}
