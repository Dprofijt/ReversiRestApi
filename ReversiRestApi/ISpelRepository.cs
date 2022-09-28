using ReversieISpelImplementatie.Model;

namespace ReversiRestApi
{
    public interface ISpelRepository
    {
        void AddSpel(Spel spel);

        public List<Spel> GetSpellen();

        Spel GetSpel(string spelToken);

        Spel GetSpelMetSpelerToken(string spelerToken);

        public List<Spel> GetBeschikbareSpellen();
        public bool DeleteSpel(string spelToken);
        public bool UpdateSpel(Spel spel);
        public bool LeaveSpel(string spelerToken);

    }
}
