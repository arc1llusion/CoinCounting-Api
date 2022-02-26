using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinCounting.Data
{
    public class CoinDeposit : EntityWithId
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int Pennies { get; set; } = 0;
        public int Nickels { get; set; } = 0;
        public int Dimes { get; set; } = 0;
        public int Quarters { get; set; } = 0;
    }
}
