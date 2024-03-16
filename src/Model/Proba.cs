using System;

namespace src.Model
{
    public class Proba
    {
        private int id_proba;
        private string tip_proba;
        private int varsta_min;
        private int varsta_max;
        private int nr_loc;
        private string locatie;

        public Proba()
        {
            this.id_proba = 0;
            this.tip_proba = null;
            this.varsta_min = 0;
            this.varsta_max = 0;
            this.nr_loc = 0;
            this.locatie = null;
        }

        public Proba(int id_proba, string tip_proba, int vasta_min, int varsta_max, int nr_loc, string locatie)
        {
            this.id_proba = id_proba;
            this.tip_proba = tip_proba;
            this.varsta_min = vasta_min;
            this.varsta_max = varsta_max;
            this.nr_loc = nr_loc;
            this.locatie = locatie;
        }

        public int IdProba
        {
            get { return id_proba; }
            set { id_proba = value; }
        }

        public string TipProba
        {
            get { return tip_proba; }
            set { tip_proba = value; }
        }

        public int VarstaMin
        {
            get { return varsta_min; }
            set { varsta_min = value; }
        }

        public int VarstaMax
        {
            get { return varsta_max; }
            set { varsta_max = value; }
        }

        public int NrLoc
        {
            get { return nr_loc; }
            set { nr_loc = value; }
        }

        public string Locatie
        {
            get { return locatie; }
            set { locatie = value; }
        }

        public override string ToString()
        {
            return $"Proba: " + $"\n Id: " + this.id_proba + $"\n Tipul Probei: " + this.tip_proba +
                   $"\n Varsta Minima: " + this.varsta_min + $"\n Varsta Maxima:" + this.varsta_max +
                   $"\n Numar Locuri: " + this.nr_loc + $"\n Locatie: " + this.locatie;
        }
    }
}