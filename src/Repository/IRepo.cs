using System;
using System.Collections.Generic;
using src.Model;

namespace src.Repository
{
    public interface IRepo<T>
    {
        void add(T e);
        void update(T e);
        void delete(int id);
        T getById(int id);
        int size();
        List<T> getAll();
    }
}