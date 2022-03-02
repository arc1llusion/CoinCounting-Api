using CoinCounting.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoinCounting_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoinsController : ControllerBase
    {
        private readonly ILogger<CoinsController> _logger;
        private readonly CoinContext _context;

        public CoinsController(ILogger<CoinsController> logger, CoinContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet("GetDeposits")]
        public async Task<ActionResult<IEnumerable<CoinDepositDto>>> GetDeposits(int userId)
        {
            var deposits = await (from cd in _context.CoinDeposits
                                  where cd.UserId == userId
                                  select new CoinDepositDto()
                                  {
                                      Id = cd.Id,
                                      UserId = cd.UserId,
                                      UserName = cd.User.UserName,
                                      Dimes = cd.Dimes,
                                      Nickels = cd.Nickels,
                                      Pennies = cd.Pennies,
                                      Quarters = cd.Quarters
                                  }).ToArrayAsync();

            return Ok(deposits);
        }

        [HttpPost("Deposit")]
        public async Task<ActionResult> Deposit(CoinDepositDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId);

            if (user == null)
                return NotFound("User not found");

            await _context.CoinDeposits.AddAsync(new CoinDeposit()
            {
                UserId = dto.UserId,
                Pennies = dto.Pennies,
                Nickels = dto.Nickels,
                Dimes = dto.Dimes,
                Quarters = dto.Quarters
            });

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
