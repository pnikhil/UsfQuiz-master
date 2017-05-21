namespace UsfQuiz.Web.ViewModels.Quiz
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Services.AutoMappers;
    using Profile.UserInfo;

    public class QuizBasicViewModel : IMapFrom<Data.DataModels.Quiz>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CategoryViewModel Category { get; set; }

        public string Description { get; set; }

        [Display(Name = "Terms and Conditions")]
        public string TermsConditions { get; set; }

        [Display(Name = "Added By")]
        public UserProfile CreatedBy { get; set; }

        [Display(Name = "Added On")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Questions")]
        public virtual int QuestionsCount { get; set; }

        public bool IsPrivate { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Data.DataModels.Quiz, QuizBasicViewModel>()
                .ForMember(
                    self => self.QuestionsCount,
                    opt => opt.MapFrom(
                        dest => dest.NumberOfQuestions > 0 ? dest.NumberOfQuestions : dest.Questions.Count));
        }
    }
}
