using System.Diagnostics;
using ReversieISpelImplementatie.Model;

namespace ReversiRestApi
{
    public class SpelRepository : ISpelRepository
    {
        // Lijst met tijdelijke spellen
        public List<Spel> Spellen { get; set; }

        public SpelRepository()
        {
            Spel spel1 = new Spel();
            Spel spel2 = new Spel();
            Spel spel3 = new Spel();

            spel1.Speler1Token = "abcdef";
            spel1.Omschrijving = "Potje snel reveri, dus niet lang nadenken";
            spel2.Speler1Token = "ghijkl";
            spel2.Speler2Token = "mnopqr";
            spel2.Omschrijving = "Ik zoek een gevorderde tegenspeler!";
            spel3.Speler1Token = "stuvwx";
            spel3.Omschrijving = "Na dit spel wil ik er nog een paar spelen tegen zelfde tegenstander";
            spel1.Token = "todo";


            Spellen = new List<Spel> { spel1, spel2, spel3 };
        }

        public void AddSpel(Spel spel)
        {
            Spellen.Add(spel);
        }

        public List<Spel> GetSpellen()
        {
            return Spellen;
        }

        public Spel GetSpel(string spelToken)
        {
            return Spellen.FirstOrDefault(s => s.Token == spelToken);
        }

        public List<Spel> GetBeschikbareSpellen()
        {
            return Spellen.Where(s => s.Speler2Token == null).ToList();
        }

        public bool DeleteSpel(string spelToken)
        {
            Spel spel = Spellen.FirstOrDefault(s => s.Token == spelToken);
            if (spel != null)
            {
                Spellen.Remove(spel);
                return true;
            }

            return false;
        }

        public bool UpdateSpel(Spel spel)
        {
            Spel spelToUpdate = Spellen.FirstOrDefault(s => s.Token == spel.Token);
            if (spelToUpdate != null)
            {
                spelToUpdate.Speler2Token = spel.Speler2Token;
                return true;
            }

            return false;
        }

        public bool LeaveSpel(string spelerToken)
        {
            Spel spel = Spellen.FirstOrDefault(s => s.Speler1Token == spelerToken || s.Speler2Token == spelerToken);
            if (spel != null)
            {
                if (spel.Speler1Token == spelerToken)
                {
                    spel.Speler1Token = null;
                }
                else
                {
                    spel.Speler2Token = null;
                }

                return true;
            }

            return false;


        }

        public Spel GetSpelMetSpelerToken(string spelerToken)
        {

            var spel = Spellen.FirstOrDefault(s => s.Speler1Token == spelerToken || s.Speler2Token == spelerToken);

            //if Spellen.First... is null return error
            if (spel == null)
            {
                throw new Exception("Speler is niet in een spel");
            }

            return spel;
        }
    }
}
