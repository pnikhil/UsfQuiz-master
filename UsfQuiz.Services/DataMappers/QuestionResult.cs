namespace UsfQuiz.Services.DataMappers
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data.DataModels;
    using AutoMappers;
    using Interfaces;

    public class QuestionResult : IQuestionResult, IMapFrom<Questions>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public int Id { get; set; }

        public int CorrectAnswerId => this.Answers.First(a => a.IsCorrect).Id;

        public int SelectedAnswerId { get; set; }

        public bool AnsweredCorrectly => this.CorrectAnswerId == this.SelectedAnswerId;

        public IList<IAvailableAnswer> Answers { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Solutions, IAvailableAnswer>().As<AvailableAnswer>();
        }
    }
}
