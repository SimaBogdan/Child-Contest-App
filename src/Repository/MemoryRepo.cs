using System;
using System.Collections.Generic;
using src.Model;


namespace src.Model
{
    namespace src.Repository
    {
        public abstract class MemoryRepo<T> : IRepo<T>
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
                return null;

            }

            public void add(T e)
            {
                throw new NotImplementedException();
            }

            public void update(T e)
            {
                throw new NotImplementedException();
            }

            public void delete(int id)
            {
                throw new NotImplementedException();
            }

            public int size()
            {
                throw new NotImplementedException();
            }

            public List<T> getAll()
            {
                throw new NotImplementedException();
            }
        }
    }
}