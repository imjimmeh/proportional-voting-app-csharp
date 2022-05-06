using Jim.Core.Shared.APIs;
using Jim.Core.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProportionalVotingApp.Models.DTOs;
using ProportionalVotingApp.Models.Requests;
using ProportionalVotingApp.Models.Services;

namespace ProportionalVoting.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VotesController : BaseController<VotesController>
    {
        private readonly IVotingRepository _votes;

        public VotesController(IVotingRepository votes, ILogger<VotesController> logger) : base(logger)
        {
            _votes = votes ?? throw new ArgumentNullException(nameof(votes));
        }

        [HttpGet]
        public async Task<IActionResult> GetVotes(int take, int skip, long minVoteId)
        {
            try
            {
                var request = new GetVotesRequest(take, skip, minVoteId);

                var result = await request.Visit(_votes).ToListAsync();

                return Ok(new GetVotesResponse(result));
            }
            catch(Exception ex)
            {
                return BadRequest(new GetVotesResponse(new[] { new ResponseError(ex.Message) }));
            }
        }
    }
}
