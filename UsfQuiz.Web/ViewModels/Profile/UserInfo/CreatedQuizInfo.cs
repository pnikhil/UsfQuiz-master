namespace UsfQuiz.Web.ViewModels.Profile.UserInfo
{
    using System;
    using Data.DataModels;
    using Services.AutoMappers;

    public class CreatedQuizInfo : IMapFrom<Quiz>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
