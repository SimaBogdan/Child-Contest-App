using System.Collections.Generic;
using src.Model;
using src.Repository;

namespace src.Service
{
    public class ProbaService
    {
        private IProbaRepository<Proba> probaRepository;

        public ProbaService(IProbaRepository<Proba> probaRepository)
        {
            this.probaRepository = probaRepository;
        }
        
        public Proba findByVarstaSiTip(int vartsa_min, int varsta_max, string tip_proba) {
            return probaRepository.findByVarstaSiTip( vartsa_min,  varsta_max, tip_proba);
        }
        
        public Proba findOne(int id) {
            return probaRepository.findOne(id);
        }

        public IEnumerable<Proba> findAll()
        {
            return probaRepository.findAll();
        }

        public void add(Proba entity)
        {
            probaRepository.add(entity);
        }
        
        public ICollection<Proba> getAll()
        {
            return (ICollection<Proba>)probaRepository.findAll();
        }
    }
}