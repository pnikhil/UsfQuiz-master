using UsfQuiz.Web.ViewModels.Profile.UserInfo;

namespace UsfQuiz.Web.ViewModels.Profile
{
    using System.Collections.Generic;
    using Data.DataModels;

    public class UserProfileViewModel
    {
        public UserProfileViewModel()
        {
            this.QuizzesCreated = new HashSet<CreatedQuizInfo>();
            this.QuizzesTaken = new HashSet<QuizAttemptedDetails>();
        }

        public UserProfile UserProfile { get; set; }

        public ICollection<CreatedQuizInfo> QuizzesCreated { get; set; }

        public ICollection<Categories> QuizCategory { get; set; }

        public ICollection<QuizAttemptedDetails> QuizzesTaken { get; set; }
    }
}
