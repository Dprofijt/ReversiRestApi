using ReversieISpelImplementatie.Model;

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
    }
}
