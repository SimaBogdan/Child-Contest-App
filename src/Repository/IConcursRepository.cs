using System;
using System.Collections.Generic;
using src.Repository;
using src.Model;

namespace src.Repository
{
    public interface IConcursRepository<Concurs> : IRepo<Concurs>
    {
        IEnumerable<Concurs> findByProba(int id_proba);
        IEnumerable<Concurs> findByParticipant(int id_participant);
        void add(Concurs concurs);
        Concurs findOne(int id);
        IEnumerable<Concurs> findAll();
    }
}