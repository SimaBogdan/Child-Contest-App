using System;

namespace src.Model
{
    public abstract class Entitate
    {
        private int id;

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