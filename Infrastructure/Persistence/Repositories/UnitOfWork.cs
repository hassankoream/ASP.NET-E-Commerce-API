// Ignore Spelling: Tkey

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositroies = [];
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
           //Get the type name
           var typeName = typeof(TEntity).Name;
            //Dic<String, Object>: string = Name of type -- object= object from GenericRepository
            //if (_repositroies.ContainsKey(typeName))
            //{
            //    return (IGenericRepository<TEntity, Tkey>) _repositroies[typeName];
                
            //}
            if (_repositroies.TryGetValue(typeName, out Object? value))
            {
                return (IGenericRepository<TEntity, Tkey>) value;

            }
            else
            {
                //Create object
                var Repo = new GenericRepository<TEntity, Tkey>(_dbContext);
                //Store Object in Dic
                _repositroies["typeName"] = Repo;
                //_repositroies.Add(typeName, Repo);
                //Return Repo
                return Repo;
            }
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
