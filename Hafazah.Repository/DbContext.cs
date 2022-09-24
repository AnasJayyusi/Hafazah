using Hafazah.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hafazah.Repository
{
    public class DbContext
    {
        public HafazahDbContext _dbContext;
        public DbContext()
        {
            _dbContext = new HafazahDbContext();
        }
    }
}
