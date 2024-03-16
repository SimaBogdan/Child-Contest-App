using System;

namespace src.Model
{
    public class Participant : Entitate
    {
        private string nume;
        private int varsta;
        private string proba;
        public int id { get; set; }

        public Participant()
        {
            this.id = 0;
            this.nume = null;
            this.varsta = 0;
            this.proba = null;
        }

        public Participant(int id, string nume, int varsta, string proba)
        {
            this.id = id;
            this.nume = nume;
            this.varsta = varsta;
            this.proba = proba;
        }

        public string Nume
        {
            get { return nume; }
            set { nume = value; }
        }

        public int Varsta
        {
            get { return varsta; }
            set { varsta = value; }
        }

        public string Proba
        {
            get { return proba; }
            set { proba = value; }
        }
        // public string getNume()
        // {
        //     return this.nume;
        // }

        // public int getVarsta()
        // {
        //     return this.varsta;
        // }
        //
        // public string getProba()
        // {
        //     return this.proba;
        // }
        //
        // // public void setNume(string nume)
        // // {
        // //     this.nume = nume;
        // // }
        //
        // public void setVarsta(int varsta)
        // {
        //     this.varsta = varsta;
        // }
        //
        // public void setProba(string proba)
        // {
        //     this.proba = proba;
        // }

        public bool login(string nume, int varsta, string proba)
        {
            return this.nume.Equals(nume) && this.varsta.Equals(varsta) && this.proba.Equals(proba);
        }

        public override string ToString()
        {
            return $"Participant: " + $"\n Id: " + this.id + $"\n Nume: " + this.nume + $"\n Varsta: " + this.varsta +
                   $"\n Proba: " + this.proba;
        }
    }
}