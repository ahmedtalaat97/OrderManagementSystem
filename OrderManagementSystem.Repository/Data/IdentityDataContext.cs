using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Repository.Data
{
    public class IdentityDataContext:IdentityDbContext<ApplicationUser>
    {

        public IdentityDataContext(DbContextOptions<IdentityDataContext> options)
        : base(options)
        {
        }
    }
}
