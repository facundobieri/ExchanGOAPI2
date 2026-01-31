using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ExchanGODbContext : DbContext
    {
        public DnSet<User> Users { get; set; }




        public ExchanGODbContext(DbContextOptions<ApplicationContext> options) :base(options)
        {
        }
    }
}
