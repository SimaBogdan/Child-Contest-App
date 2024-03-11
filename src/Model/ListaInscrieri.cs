using System;
using System.Collections;

namespace src.Model
{
    public class ListaInscrieri
    {
        private List<Inscriere> inscrieri;
        int id_inscriere;

        public ListaInscrieri()
        {
            this.inscrieri = new List<Inscriere>();
            this.id_inscriere = 0;
        }

        public ListaInscrieri(List<Inscriere> inscrieri, int idInscriere)
        {
            this.inscrieri = inscrieri;
            this.id_inscriere = idInscriere;
        }

        public List<Inscriere> Inscrieri
        {
            get { return this.inscrieri; }
            set { this.inscrieri = value; }
        }

        public int IdInscriere
        {
            get { return this.id_inscriere; }
            set { this.id_inscriere = value; }
        }
    }
    
    
}