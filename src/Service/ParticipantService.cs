using System.Collections.Generic;
using src.Model;
using src.Repository;

namespace src.Service
{
    public class ParticipantService
    {
        private IParticipantRepository<Participant> participantRepository;

        public ParticipantService(IParticipantRepository<Participant> participantRepository)
        {
            this.participantRepository = participantRepository;
        }
        
        public Participant findOneByNumeSiVarsta(string nume, string prenume, int varsta) {
            return participantRepository.findOneByNumeSiVarsta(nume, prenume, varsta);
        }
        
        public Participant findOne(int id) {
            return participantRepository.findOne(id);
        }

        public IEnumerable<Participant> findAll()
        {
            return participantRepository.findAll();
        }

        public void add(Participant entity)
        {
            participantRepository.add(entity);
        }
        
        public ICollection<Participant> getAll()
        {
            return (ICollection<Participant>)participantRepository.findAll();
        }
    }
}