using System.Collections.Generic;
using src.Model;
using src.Repository;

namespace src.Service
{
    public class ConcursService
    {
        private IConcursRepository<Concurs> concursRepository;

        public ConcursService(IConcursRepository<Concurs> concursRepository)
        {
            this.concursRepository = concursRepository;
        }
        
        public IEnumerable<Concurs> findByProba(int id_proba){
            return concursRepository.findByProba(id_proba);
        }
        
        public IEnumerable<Concurs> findByParticipant(int id_participant){
            return concursRepository.findByParticipant(id_participant);
        }
        
        public Concurs findOne(int id) {
            return concursRepository.findOne(id);
        }

        public IEnumerable<Concurs> findAll()
        {
            return concursRepository.findAll();
        }

        public void add(Concurs entity)
        {
            concursRepository.add(entity);
        }
        
        public ICollection<Concurs> getAll()
        {
            return (ICollection<Concurs>)concursRepository.findAll();
        }
    }
}