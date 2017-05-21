namespace UsfQuiz.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Services.Interfaces;
    using ViewModels.Profile;
    using ViewModels.Profile.UserInfo;
    using System.Linq;

    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly IUsersService users;

        public ProfileController(IUsersService users)
        {
            this.users = users;
        }

        public ActionResult MyAccount()
        {
            return this.RedirectToAction("User", new { username = this.UserName });
        }

        public new ActionResult User(string username)
        {
            var user = this.users.ByUserName(username);
            if (user == null)
            {
                return this.View("Error");
            }

            var profileInfo = this.Mapper.Map<UserProfile>(user);
            var lastQuizzes = this.Mapper.Map<List<CreatedQuizInfo>>(
                user.QuizzesCreated.OrderByDescending(q => q.CreatedOn).Take(5));
            var lastSolutions = this.Mapper.Map<List<QuizAttemptedDetails>>(
                user.SolutionsSubmited.OrderByDescending(s => s.CreatedOn).Take(5));

            var pageModel = new UserProfileViewModel
            {
                UserProfile = profileInfo,
                QuizzesCreated = lastQuizzes,
                QuizzesTaken = lastSolutions
            };

            var maxQuizzesCreated = this.users.GetCreatedQuizCount();
            UserProfile.MaxQuizzesCreated = maxQuizzesCreated;

            return this.View(pageModel);
        }

    }
}
