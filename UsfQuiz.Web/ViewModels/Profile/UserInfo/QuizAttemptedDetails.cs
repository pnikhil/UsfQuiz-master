namespace UsfQuiz.Web.ViewModels.Profile.UserInfo
{
    using System;
    using AutoMapper;
    using UsfQuiz.Data.DataModels;
    using UsfQuiz.Services.AutoMappers;

    public class QuizAttemptedDetails : IMapFrom<UserAnswers>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<UserAnswers, QuizAttemptedDetails>()
                .ForMember(
                    self => self.Title,
                    opt => opt.MapFrom(model => model.ForQuiz.Name));
        }
    }
}
