using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReversieISpelImplementatie.Model;
using ReversiRestApi.Models;

namespace ReversiRestApi.Controllers
{
    
    [Route("api/Spel")]
    [ApiController]
    public class SpelController : ControllerBase
    {
        private readonly ISpelRepository _iRepository;

        public SpelController(ISpelRepository repository)
        {
            _iRepository = repository;
        }


        // GET api/spel
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetSpelOmschrijvingenVanSpellenMetWachtendeSpeler()
        {
            List<Spel> spellen = _iRepository.GetSpellen();
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
        public ActionResult<Spel_Info> GetSpelMetSpelToken(string spelToken)
        {
            Spel_Info spel_info = new Spel_Info();

            Spel spel = _iRepository.GetSpel(spelToken);
            if (spel != null)
            {
                return spel_info.ConvertToJSON(spel);
            }

            return null;
        }

        //getSpel with spelerToken
        [HttpGet("speler/{spelerToken}")]
        public ActionResult<Spel_Info> GetSpelMetSpelerToken(string spelerToken)
        {
            Spel spel = _iRepository.GetSpelMetSpelerToken(spelerToken);
            if (spel == null)
            {
                return NotFound();
            }
            Spel_Info spel_info = new Spel_Info();
            return spel_info.ConvertToJSON(spel);
        }

        
        [HttpGet("{token}/beurt")]
        public ActionResult<string> GetBeurt(string token)
        {
            Spel spel = _iRepository.GetSpel(token);
            if (spel == null)
            {
                return NotFound();
            }

            return Spel_Info.GetNumberFromColor(spel.AandeBeurt);
        }

        // Putt
        [HttpPut("Zet")]
        public async Task<ActionResult> PutSpelerZet([FromBody] SpelTokens tokens, int rij, int kolom)
        {
            string spelerToken = tokens.SpelerToken;
            string spelToken = tokens.SpelToken;

            Spel spel = _iRepository.GetSpel(spelToken);
            if (spel == null || spel.Speler1Token != spelerToken && spel.Speler2Token != spelerToken)
            {
                return NotFound();
            }

            spel.DoeZet(rij, kolom);
            _iRepository.UpdateSpel(spel);
            return Ok();

        }



        [HttpPut("Opgeven")]
        public async Task<ActionResult> PutSpelerOpgeven([FromBody] SpelTokens tokens)
        {
            string spelerToken = tokens.SpelerToken;
            string spelToken = tokens.SpelToken;

            Spel spel = _iRepository.GetSpel(spelToken);
            if (spel == null || spel.Speler1Token != spelerToken && spel.Speler2Token != spelerToken)
            {
                return NotFound();
            }

            spel.Opgeven(spelerToken);

            spel.Afgelopen();
            _iRepository.UpdateSpel(spel);
            return Ok();

        }

        // join spel
        [HttpPut("Join")]
        public async Task<ActionResult> PutSpelerJoin([FromBody] SpelTokens tokens)
        {
            string spelerToken = tokens.SpelerToken;
            string spelToken = tokens.SpelToken;

            Spel spel = _iRepository.GetSpel(spelToken);
            if (spel == null || spel.Speler2Token != null)
            {
                return NotFound();
            }

            if (spel.Speler1Token == spelerToken)
            {
                return BadRequest();
            }
            if (spel.Speler2Token == null)
            {
                spel.Speler2Token = spelerToken;
                _iRepository.UpdateSpel(spel);
                return Ok();
            }


            return BadRequest();

        }


        // POST create spel
        [HttpPost("CreateGame")]
        public void PostNieuwSpel([FromBody] Spel_Info spelInfo)
        {
            // check if speler 1 token is empty and omschrijving empty, then make new spel
            if (spelInfo.Speler1Token != null && spelInfo.Omschrijving != null)
            {
                Spel spel = new Spel();
                spel.Speler1Token = spelInfo.Speler1Token;
                spel.Omschrijving = spelInfo.Omschrijving;
                spel.Token = Guid.NewGuid().ToString();
                _iRepository.AddSpel(spel);
            }
        }

        // DELETE api/spel
        [HttpDelete("{spelerToken}")]
        public async Task<ActionResult> DeleteSpel(string spelerToken)
        {
            throw new NotImplementedException();

            Spel spel = _iRepository.GetSpelMetSpelerToken(spelerToken);
            if (spel == null)
            {
                return NotFound();
            }

     //       _iRepository.DeleteSpel(spel);
            return Ok();
        }

    }
}
