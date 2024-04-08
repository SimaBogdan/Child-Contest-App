using System.Collections.Generic;
using src.Model;
using src.Repository;

namespace src.Service
{
    public class OrganizatorService
    {
        private IOrganizatorRepository<Organizator> organizatorRepository;

        public OrganizatorService(IOrganizatorRepository<Organizator> organizatorRepository)
        {
            this.organizatorRepository = organizatorRepository;
        }
        
        public Organizator findOneByUsernameAndPassword(string username, string parola) {
            return organizatorRepository.findOneByUsernameAndPassword( username,  parola);
        }
        
        public Organizator findOne(int id) {
            return organizatorRepository.findOne(id);
        }

        public IEnumerable<Organizator> findAll()
        {
            return organizatorRepository.findAll();
        }

        public void add(Organizator entity)
        {
            organizatorRepository.add(entity);
        }
        
        public ICollection<Organizator> getAll()
        {
            return (ICollection<Organizator>)organizatorRepository.findAll();
        }
    }
}