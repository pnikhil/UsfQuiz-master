using System.Linq;

namespace UsfQuiz.Web.ViewModels.Quiz.Solve
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Services.AutoMappers;

    public class QuizForSolvingModel : IMapFrom<Data.DataModels.Quiz>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<QuestionModel> Questions { get; set; }

        public string Description { get; set; }

        public string TermsConditions { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Data.DataModels.Quiz, QuizForSolvingModel>()
                .ForMember(
                    self => self.Questions,
                    opt => opt.MapFrom(model => model.NumberOfQuestions > 0
                        ? model.Questions.OrderBy(q => Guid.NewGuid()).Take(model.NumberOfQuestions)
                        : model.Questions.OrderBy(q => q.CreatedOn)))
                .ForMember(
                    self => self.ImageUrl,
                    opt => opt.MapFrom(model => model.Category.ImageUrl));
        }
    }
}
