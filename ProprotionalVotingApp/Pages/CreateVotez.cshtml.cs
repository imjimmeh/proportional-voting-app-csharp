using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProportionalVotingApp.DatabaseService;
using ProportionalVotingApp.Models.DTOs;
using ProportionalVotingApp.Models.Services;

namespace ProprotionalVotingApp.Pages
{
    public class CreateVoteModel : PageModel
    {
        private readonly IVotingRepository repository;

        public CreateVoteModel(VotingRepository repository)
        {
            this.repository = repository;
            Vote = new VoteDTO();
        }

        public VoteDTO Vote { get; private init; } = null!;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var createdId = await repository.AddVoteAsync(Vote);

                return Redirect("votes");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
