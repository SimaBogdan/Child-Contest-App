using System;
using System.Collections.Generic;
using src.Model;
using src.Repository;


namespace src.Model
{
    namespace src.Repository
    {
        public abstract class MemoryRepo<T> where T : IRepo<T>
        {

            protected List<T> entitati;

            public MemoryRepo()
            {
                entitati = new List<T>();
            }

            public T getById(int id)
            {
                foreach (T entity in entitati)
                {
                    if (entity.id == id)
                        return entity;
                }

                return default;

            }

            public void add(T e)
            {
                entitati.Add(e);
            }
            
            public int getPos(T e)
            {
                for (int i = 0; i < size(); i++)
                {
                    if (entitati[i].id == e.id)
                    {
                        return i;
                    }
                }
                return -1;
            }

            public void update(T e)
            {
                int poz = getPos(e);
                entitati[poz] = e;
            }

            public void delete(int id)
            {
                T e = getById(id);
                entitati.Remove(e);
            }

            public int size()
            {
                return entitati.Count;
            }

            public List<T> getAll()
            {
                return entitati;
            }
        }
    }
}