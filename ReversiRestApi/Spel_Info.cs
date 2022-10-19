using ReversieISpelImplementatie.Model;
using System.Text;

namespace ReversiRestApi
{
    public class Spel_Info
    {
        public int Id { get; set; }
        public string SpelToken { get; set; }
        public string Speler1Token { get; set; }
        public string Speler2Token { get; set; }
        public string Omschrijving { get; set; }
        public Kleur Beurt { get; set; }
        public string Bord { get; set; }

        public Spel_Info ConvertToJSON(Spel spel)
        {
            this.Id = spel.ID;
            this.SpelToken = spel.Token;
            this.Speler1Token = spel.Speler1Token;
            this.Speler2Token = spel.Speler2Token;
            this.Omschrijving = spel.Omschrijving;
            this.Beurt = spel.AandeBeurt;
            this.Bord = BordToString(spel);

            return this;

        }

        public string BordToString(Spel spel)
        {
            StringBuilder stringBord = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    stringBord.Append(GetNumberFromColor(spel.Bord[i, j]).ToString());
                }
            }
            return stringBord.ToString();
        }
        public static string GetNumberFromColor(Kleur kleur)
        {
            switch (kleur)
            {
                case Kleur.Geen:
                    return "0";
                case Kleur.Wit:
                    return "1";
                case Kleur.Zwart:
                    return "2";
                default:
                    return "0";
            }
        }

        public Kleur[,] StringToBord(string bord)
        {
            Kleur[,] bordKleur = new Kleur[8, 8];
            int index = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    bordKleur[i, j] = GetColorFromNumber(Convert.ToInt32(bord[index]));
                    index++;
                }
            }
            return bordKleur;

        }

        public Kleur GetColorFromNumber(int number)
        {
            switch (number)
            {
                case 0:
                    return Kleur.Geen;
                case 1:
                    return Kleur.Wit;
                case 2:
                    return Kleur.Zwart;
                default:
                    return Kleur.Geen;
            }
        }

    }
    
}
