using System.Collections.Generic;
using src.Model;

namespace src.Repository
{
    public interface IOrganizatorRepository<Organizator> : IRepo<Organizator>
    {
        Organizator findOneByUsernameAndPassword(string username, string parola);
        void add(Organizator organizator);
        Organizator findOne(int id);
        IEnumerable<Organizator> findAll();
    }
}