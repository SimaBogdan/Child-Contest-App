using System.Collections.Generic;
using src.Model;

namespace src.Service
{
    public interface IService<T>
    {
        Organizator findOneByUsernameAndPassword(string username, string parola);
        ICollection<Proba> getAllProba();

        ICollection<Participant> getAllParticipant();

        ICollection<Concurs> findByProba(int id_proba);

        ICollection<Concurs> findByParticipant(int id_participant);

        T findOne(int id);

        Participant findOneByNumeSiVarsta(string nume, string prenume, int varsta);

        Proba findByVarstaSiTip(int varsta_min, int varsta_max, string tip_proba);

        void addConcurs(Concurs entity);

        void addParticipant(Participant entity);
    }
}