using System.Collections.Generic;
using src.Model;

namespace src.Service
{
    public interface IService
    {
        Organizator findOneByUsernameAndPassword(string username, string parola);
        ICollection<Proba> getAllProba();

        ICollection<Participant> getAllParticipant();

        ICollection<Concurs> findByProba(int id_proba);

        ICollection<Concurs> findByParticipant(int id_participant);

        Participant findOne(int id);
        Proba findOnProba(int id);
        Organizator findOneOrganizator(int id);
        Concurs findOneConcurs(int id);

        Participant findOneByNumeSiVarsta(string nume, string prenume, int varsta);

        Proba findByVarstaSiTip(int varsta_min, int varsta_max, string tip_proba);

        void addConcurs(Concurs entity);

        void addParticipant(Participant entity);
    }
}