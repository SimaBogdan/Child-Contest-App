using System.Collections.Generic;
using src.Model;

namespace src.Service
{
    public class Service<T> where T : IService<T>
    {
        ParticipantService participantService;
        OrganizatorService organizatorService;
        ProbaService probaService;
        ConcursService concursService;

        public Service(ParticipantService participantService, OrganizatorService organizatorService, ProbaService probaService, ConcursService concursService)
        {
            this.participantService = participantService;
            this.organizatorService = organizatorService;
            this.probaService = probaService;
            this.concursService = concursService;
        }

        public Organizator findOneByUsernameAndPassword(string username, string parola)
        {
            return organizatorService.findOneByUsernameAndPassword(username, parola);
        }

        public ICollection<Proba> getAllProba()
        {
            return probaService.getAll();
        }

        public ICollection<Participant> getAllParticipant()
        {
            return participantService.getAll();
        }

        public ICollection<Concurs> findByProba(int id_proba)
        {
            return (ICollection<Concurs>)concursService.findByProba(id_proba);
        }

        public ICollection<Concurs> findByParticipant(int id_participant)
        {
            return (ICollection<Concurs>)concursService.findByParticipant(id_participant);
        }

        public Participant findOne(int id)
        {
            return participantService.findOne(id);
        }

        public Participant findOneByNumeSiVarsta(string nume, string prenume, int varsta)
        {
            return participantService.findOneByNumeSiVarsta(nume, prenume, varsta);
        }

        public Proba findByVarstaSiTip(int varsta_min, int varsta_max, string tip_proba)
        {
            return probaService.findByVarstaSiTip(varsta_min, varsta_max, tip_proba);
        }

        public void addConcurs(Concurs entity)
        {
            concursService.add(entity);
        }

        public void addParticipant(Participant entity)
        {
            participantService.add(entity);
        }

        public void addProba(Proba entity)
        {
            probaService.add(entity);
        }

        public void addOrganizator(Organizator entity)
        {
            organizatorService.add(entity);
        }
    }
}