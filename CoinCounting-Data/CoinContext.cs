using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinCounting.Data
{
    public class CoinContext : DbContext
    {
#nullable disable
        public CoinContext(DbContextOptions<CoinContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
#nullable restore
    }
}
