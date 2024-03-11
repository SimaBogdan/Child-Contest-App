using System;

namespace src.Model
{
    public class Inscriere
    {
        private int id_participant;
        private int id_proba;
        private Concurs concurs;

        public Inscriere()
        {
            this.id_participant = 0;
            this.id_proba = 0;
            this.concurs = new Concurs();
        }

        public Inscriere(int id_participant, int id_proba, Concurs concurs)
        {
            this.id_participant = id_participant;
            this.id_proba = id_proba;
            this.concurs = concurs;
        }

        public int IdParticipantInscriere
        {
            get { return id_participant; }
            set { id_participant = value; }
        }

        public int IdProbaInscriere
        {
            get { return id_proba; }
            set { this.id_proba = value; }
        }

        public Concurs Concurs
        {
            get { return concurs; }
            set { concurs = value; }
        }

        public override string ToString()
        {
            return $"ID Participant: " + this.id_participant + $"\n ID Proba: " + this.id_proba + $"\n Concurs" +
                   this.concurs.ToString();
        }
    }
}