namespace src.Model
{
    public class Organizator : Entitate
    {
        private string nume;
        private string prenume;
        private string username;
        private string parola;

        public int id { get; set; }

        public Organizator()
        {
            this.id = 0;
            this.nume = "";
            this.prenume = "";
            this.username = "";
            this.parola = "";
        }

        public Organizator(int id, string nume, string prenume, string parola, string username)
        {
            this.id = id;
            this.nume = nume;
            this.prenume = prenume;
            this.parola = parola;
            this.username = username;
        }

        public string Nume
        {
            get { return nume; }
            set { this.nume = value; }
        }

        public string Prenume
        {
            get { return prenume; }
            set { this.prenume = value; }
        }

        public string Username
        {
            get { return username; }
            set { this.username = value; }
        }

        public string Parola
        {
            get { return parola; }
            set { this.parola = value; }
        }

        public override string ToString()
        {
            return $"id_org: " + this.id + $"nume: " + this.nume + $"prenume: " + this.prenume + $"username: " +
                   this.username + $"parola: " + this.parola;
        }
    }
}