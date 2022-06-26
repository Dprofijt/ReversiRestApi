﻿namespace ReversiRestApi.Models
{
    public class Spel : ISpel
    {
        private const int bordOmvang = 8;
        private readonly int[,] richting = new int[8, 2] {
                                {  0,  1 },         // naar rechts
                                {  0, -1 },         // naar links
                                {  1,  0 },         // naar onder
                                { -1,  0 },         // naar boven
                                {  1,  1 },         // naar rechtsonder
                                {  1, -1 },         // naar linksonder
                                { -1,  1 },         // naar rechtsboven
                                { -1, -1 } };       // naar linksboven
        public int ID { get; set; }
        public string Omschrijving { get; set; }
        public string Token { get; set; }
        public string Speler1Token { get; set; }
        public string Speler2Token { get; set; }
        public Kleur[,] Bord { get; set; }
        public Kleur AandeBeurt { get; set; }

        public Spel()
        {
            Bord = new Kleur[bordOmvang, bordOmvang];
            Bord[3, 3] = Kleur.Wit;
            Bord[4, 4] = Kleur.Wit;
            Bord[3, 4] = Kleur.Zwart;
            Bord[4, 3] = Kleur.Zwart;
            AandeBeurt = Kleur.Wit;

        }

        public bool Afgelopen()
        {
            //check bord is full
            bool bordVol = true;

            for (int i = 0; i < bordOmvang; i++)
            {
                for (int j = 0; j < bordOmvang; j++)
                {
                    if (Bord[i, j] == Kleur.Geen)
                    {
                        bordVol = false;
                    }
                }
            }
            if (!bordVol)
            {
                for (int i = 0; i < bordOmvang; i++)
                {
                    for (int j = 0; j < bordOmvang; j++)
                    {
                        if (Bord[i, j] == Kleur.Geen)
                        {
                            if (ZetMogelijk(i, j))
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool DoeZet(int rijZet, int kolomZet)
        {
            if (ZetMogelijk(rijZet, kolomZet))
            {

                //doe zet
                Bord[rijZet, kolomZet] = AandeBeurt;
                //change Kleur from every Kleur between AandeBeurt
                for (int i = 0; i < bordOmvang; i++)
                {
                    for (int j = 0; j < bordOmvang; j++)
                    {
                        if (Bord[i, j] == AandeBeurt)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                int rij = i + richting[k, 0];
                                int kolom = j + richting[k, 1];
                                if (rij >= 0 && rij < bordOmvang && kolom >= 0 && kolom < bordOmvang)
                                {
                                    if (Bord[rij, kolom] == (AandeBeurt == Kleur.Wit ? Kleur.Zwart : Kleur.Wit))
                                    {
                                        Bord[rij, kolom] = AandeBeurt;
                                    }
                                }
                            }
                        }
                    }
                }
                //change AandeBeurt
                AandeBeurt = AandeBeurt == Kleur.Wit ? Kleur.Zwart : Kleur.Wit;

            }
            return false;
        }

        public Kleur OverwegendeKleur()
        {
            //decide which Kleur is more on the bord
            int wit = 0;
            int zwart = 0;
            for (int i = 0; i < bordOmvang; i++)
            {
                for (int j = 0; j < bordOmvang; j++)
                {
                    if (Bord[i, j] == Kleur.Wit)
                    {
                        wit++;
                    }
                    else if (Bord[i, j] == Kleur.Zwart)
                    {
                        zwart++;
                    }
                }
            }
            //return winner
            if (wit > zwart)
            {
                return Kleur.Wit;
            }
            else if (zwart > wit)
            {
                return Kleur.Zwart;
            }
            else
            {
                return Kleur.Geen;
            }

        }

        public bool Pas()
        {
            //check if there is a possible set
            for (int i = 0; i < bordOmvang; i++)
            {
                for (int j = 0; j < bordOmvang; j++)
                {
                    if (Bord[i, j] == Kleur.Geen)
                    {
                        if (ZetMogelijk(i, j))
                        {
                            return false;
                        }
                    }
                }
            }
            //give the turn to the other
            AandeBeurt = AandeBeurt == Kleur.Wit ? Kleur.Zwart : Kleur.Wit;

            return true;

        }
        //check if set is outside bord
        
        public bool ZetMogelijk(int rijZet, int kolomZet)
        {
            //check if set is outside bord
            if (rijZet < 0 || rijZet > bordOmvang - 1 || kolomZet < 0 || kolomZet > bordOmvang - 1)
            {
                return false;
            }
            //check if set is possible
            for (int i = 0; i < 8; i++)
            {
                int rij = rijZet + richting[i, 0];
                int kolom = kolomZet + richting[i, 1];
                if (rij < 0 || rij > bordOmvang - 1 || kolom < 0 || kolom > bordOmvang - 1)
                {
                    continue;
                }
                if (Bord[rij, kolom] == AandeBeurt)
                {
                    return true;
                }
            }

            return true;

        }
    }
}