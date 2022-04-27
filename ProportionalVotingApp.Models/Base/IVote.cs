
namespace ProportionalVotingApp.Models.Base
{
    public interface IVote
    {
        bool Completed { get; }
        DateTime CreatedAt { get; }
        string Creator { get; }
        IList<string> Options { get; }
    }
}