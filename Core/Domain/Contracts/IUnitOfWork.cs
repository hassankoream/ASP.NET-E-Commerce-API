using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        //public IGenericRepository<Product, int> ProductRepository { get; }
        //public IGenericRepository<Employee, int> EmployeeRepository { get; }

        //we will use a method to handle all Entities

        IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity: BaseEntity<Tkey>;

        Task<int> SaveChangesAsync();
    }
}
