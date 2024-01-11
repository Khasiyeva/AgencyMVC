using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DAL.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity :  new()
    {
        
    }
}
