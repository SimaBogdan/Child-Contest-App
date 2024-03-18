using System;

namespace src.Model
{
    public class Concurs : Entitate
    {
        private Proba proba;
        private Participant participant;
        public int id { get; set; }

        public Concurs()
        {
            this.id = 0;
            this.proba = new Proba();
            this.participant = new Participant();
        }

        public Concurs(int id,Proba proba, Participant participant)
        {
            this.id = id;
            this.proba = proba;
            this.participant = participant;
        }

        public Proba Proba
        {
            get { return proba; }
            set { proba = value; }
        }

        public Participant Participant
        {
            get { return participant; }
            set { participant = value; }
        }

        public override string ToString()
        {
            return $"Proba: " + this.proba.ToString() + $"\n Participant: " + this.participant.ToString();
        }
    }
}