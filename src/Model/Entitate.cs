using System;

namespace src.Model
{
    public class Entitate
    {
        private protected int id;

        public Entitate()
        {
            this.id = 0;
        }
        
        public Entitate(int id)
        {
            this.id = id;
        }
        
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}