using UsfQuiz.Services.AutoMappers;

namespace UsfQuiz.Services.DataMappers
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data.DataModels;
    using Interfaces;

    public class EvaluationResult : IEvaluationResult, IHaveCustomMappings
    {
        public int ForQuizId { get; set; }

        public string Name { get; set; }

        public ICollection<IQuestionResult> QuestionResults { get; set; }

        public int TotalQuestions => this.QuestionResults.Count;

        public int TotalCorrectAnswer => this.QuestionResults.Count(q => q.AnsweredCorrectly);
        public double CorrectScorePercent()
        {
            var correctlyAnsweredCount = (double)this.QuestionResults.Count(q => q.AnsweredCorrectly);
            return (correctlyAnsweredCount / this.TotalQuestions) * 100;
        }
        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Questions, IQuestionResult>().As<QuestionResult>();

            configuration.CreateMap<Quiz, EvaluationResult>()
                .ForMember(
                    self => self.ForQuizId,
                    opt => opt.MapFrom(m => m.Id))
                .ForMember(
                    self => self.QuestionResults,
                    opt => opt.MapFrom(m => m.Questions));
        }
    }
}
