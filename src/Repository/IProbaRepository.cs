using System;
using System.Collections.Generic;
using src.Repository;
using src.Model;

namespace src.Repository
{
    public interface IProbaRepository<Proba> : IRepo<Proba>
    {
        Proba findByVarstaSiTip(int vartsa_min, int varsta_max, string tip_proba);
        void add(Proba proba);
        Proba findOne(int id);
        IEnumerable<Model.Proba> findAll();
    }
}