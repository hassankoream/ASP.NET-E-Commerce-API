﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        //Signature for each Dynamic part in a Query

        public Expression<Func<TEntity, bool>> Criteria { get; }
       public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
    }
}
