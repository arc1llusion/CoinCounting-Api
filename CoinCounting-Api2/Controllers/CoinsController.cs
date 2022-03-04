using CoinCounting.Data;
using CoinCounting_Api2.Hubs;
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
        private readonly DepositNotificationManager _depositNotificationManager;

        public CoinsController(ILogger<CoinsController> logger, CoinContext context, DepositNotificationManager depositNotificationManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _depositNotificationManager = depositNotificationManager ?? throw new ArgumentNullException(nameof(depositNotificationManager));
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
                                      Quarters = cd.Quarters,
                                      DateDeposited = cd.DateDesposited
                                  }).ToArrayAsync();

            return Ok(deposits);
        }

        [HttpPost("Deposit")]
        public async Task<ActionResult> Deposit(CoinDepositDto dto, CancellationToken token = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId, token);

            if (user == null)
                return NotFound("User not found");

            await _context.CoinDeposits.AddAsync(new CoinDeposit()
            {
                UserId = dto.UserId,
                Pennies = dto.Pennies,
                Nickels = dto.Nickels,
                Dimes = dto.Dimes,
                Quarters = dto.Quarters,
                DateDesposited = DateTimeOffset.UtcNow
            }, token);

            _logger.LogInformation("Deposit Made");

            await _depositNotificationManager.BroadCast(dto, token);

            await _context.SaveChangesAsync(token);

            return Ok();
        }
    }
}
