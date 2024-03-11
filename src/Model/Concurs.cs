using System;

namespace src.Model
{
    public class Concurs
    {
        private Proba proba;
        private Participant participant;

        public Concurs()
        {
            this.proba = new Proba();
            this.participant = new Participant();
        }

        public Concurs(Proba proba, Participant participant)
        {
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