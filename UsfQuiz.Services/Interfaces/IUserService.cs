namespace UsfQuiz.Services.Interfaces
{
    using System.Linq;
    using UsfQuiz.Data.DataModels;

    public interface IUsersService
    {
        User ById(string id);

        User ByUserName(string userName);

        IQueryable<User> AllUsers();

        int GetCreatedQuizCount();
    }
}
