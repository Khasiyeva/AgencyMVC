
using Agency.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DAL.Repostories.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : new()
    {
        
    }
}
