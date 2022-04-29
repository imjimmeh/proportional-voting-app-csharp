﻿using Jim.Core.Database.Models;

namespace Jim.Core.Authentication.Models.Interfaces
{
    public interface IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public IEnumerable<IClaim> Claims { get; }
    }

    public interface IDatabaseUser : IUser, IDatabaseEntity
    {
    }
}
