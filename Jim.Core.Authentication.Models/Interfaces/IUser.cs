namespace Jim.Core.Authentication.Models.Interfaces
{
    public interface IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
