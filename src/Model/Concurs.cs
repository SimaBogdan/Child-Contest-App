using System;

namespace src.Model
{
    public class Concurs : Entitate
    {
        private int id_proba;
        private int id_participant;
        public int id { get; set; }

        public Concurs()
        {
            this.id = 0;
            this.id_proba = 0;
            this.id_participant = 0;
        }

        public Concurs(int id, int idParticipant, int idProba)
        {
            this.id = id;
            this.id_participant = idParticipant;
            this.id_proba = idProba;
        }

        public int IdProba
        {
            get { return id_proba; }
            set { id_proba = value; }
        }

        public int IdParticipant
        {
            get { return id_participant; }
            set { id_participant = value; }
        }

        public override string ToString()
        {
            return $"Proba: " + this.id_proba + $"\n Participant: " + this.id_participant;
        }
    }
}