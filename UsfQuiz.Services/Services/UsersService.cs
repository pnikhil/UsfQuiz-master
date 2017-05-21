namespace UsfQuiz.Services.Services
{
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Data.DataModels;
    using Interfaces;

    public class UsersService : IUsersService
    {
        private readonly UserManager<User> manager;

        public UsersService(UserManager<User> manager)
        {
            this.manager = manager;
        }

        public User ById(string id)
        {
            return this.manager.FindById(id);
        }

        public User ByUserName(string userName)
        {
            return this.manager.FindByName(userName);
        }

        public IQueryable<User> AllUsers()
        {
            return this.manager.Users;
        }

        public int GetCreatedQuizCount()
        {
            var result = this.AllUsers().Max(u => u.QuizzesCreated.Count);
            return result;
        }
    }
}
