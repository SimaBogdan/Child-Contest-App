using System;

namespace src.Model
{
    public class TabelUsers
    {
        private List<Participant> participanti;

        public TabelUsers()
        {
            this.participanti = new List<Participant>();
        }

        public TabelUsers(List<Participant> participanti)
        {
            this.participanti = participanti;
        }

        public List<Participant> Participanti
        {
            get { return this.participanti; }
            set { this.participanti = value; }
        }
    }
}