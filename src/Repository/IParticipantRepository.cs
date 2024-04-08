using System;
using System.Collections.Generic;
using src.Repository;
using src.Model;

namespace src.Repository
{
    public interface IParticipantRepository<Participant> : IRepo<Participant>
    {
        Participant findOneByNumeSiVarsta(string nume, string prenume, int varsta);
        void add(Participant participant);
        Participant findOne(int id);
        IEnumerable<Participant> findAll();
    }
}