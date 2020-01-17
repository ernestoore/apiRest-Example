using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ServiciosRestApp.Models
{
    public class EmpresaContext : DbContext
    {
        public EmpresaContext (DbContextOptions<EmpresaContext> options)
            : base(options)
        {
        }

        public DbSet<ServiciosRestApp.Models.Productos> Productos { get; set; }
    }
}
