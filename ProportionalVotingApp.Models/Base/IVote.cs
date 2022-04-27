
namespace ProportionalVotingApp.Models.Base
{
    public interface IVote<TOption> : IVote
    {
        IList<TOption> Options { get; }
    }

    public interface IVote
    {
        bool Completed { get; }
        DateTime CreatedAt { get; }
        string Creator { get; set; }
        string Name { get; set; }
    }
}