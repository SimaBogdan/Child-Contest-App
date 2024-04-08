using System;

namespace src.Model
{
    public class Concurs : Entitate
    {
        private int id_proba;
        private int id_participant;
        private int id_organizator;
        public int id { get; set; }

        public Concurs()
        {
            this.id = 0;
            this.id_proba = 0;
            this.id_participant = 0;
            this.id_organizator = 0;
        }

        public Concurs(int id, int idParticipant, int idProba, int idOrganizator)
        {
            this.id = id;
            this.id_participant = idParticipant;
            this.id_proba = idProba;
            this.id_organizator = idOrganizator;
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

        public int IdOrganizator
        {
            get { return id_participant; }
            set { this.id_organizator = value; }
        }

        public override string ToString()
        {
            return $"Proba: " + this.id_proba + $"\n Participant: " + this.id_participant + $"Org: " + this.id_organizator;
        }
    }
}