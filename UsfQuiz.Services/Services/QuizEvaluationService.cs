namespace UsfQuiz.Services.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Core;
    using System.Linq;
    using AutoMapper;
    using Data.Commons;
    using Data.DataModels;
    using Dto;
    using Interfaces;
    using DataMappers;

    public class QuizEvaluationService : IQuizEvaluationService
    {
        private readonly IDbRepository<Quiz> quizzes;
        private readonly IDbRepository<UserAnswers> solutions;
        private readonly IMapper mapper;

        public QuizEvaluationService(
            IDbRepository<Quiz> quizzes, IDbRepository<UserAnswers> solutions, IMapper mapper)
        {
            this.quizzes = quizzes;
            this.solutions = solutions;
            this.mapper = mapper;
        }

        public UserAnswers SaveSolution(QuizEvaluationDto quizSolution, string userId)
        {
            var quiz = this.quizzes.GetById(quizSolution.ForQuizId);

            if (quizSolution.SelectedAnswerIds.Count != quiz.NumberOfQuestions &&
                quizSolution.SelectedAnswerIds.Count != quiz.Questions.Count)
            {
                throw new Exception("Invalid Solution: Questions count mismatch");
            }

            List<Solutions> selectedAnswers = this.ExtractSelectedAnswers(quiz, quizSolution);

            var newSolution = new UserAnswers
            {
                ByUserId = userId,
                ForQuiz = quiz,
                SelectedAnswers = selectedAnswers
            };

            try
            {
                this.solutions.Add(newSolution);
                this.solutions.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while saving your solution.", ex);
            }

            return newSolution;
        }

        public void Save()
        {
            this.quizzes.Save();
        }

        public IEvaluationResult Evaluate(int solutionId)
        {
            var solution = this.solutions.GetById(solutionId);
            if (solution == null)
            {
                throw new ObjectNotFoundException("Falied to find solution by Id");
            }

            return this.Evaluate(solution);
        }

        public IEvaluationResult Evaluate(UserAnswers userAnswer)
        {
            var result = this.CreateEvaluation(userAnswer);
            var answersByQuestionId = this.GetAnswersByQuestionId(userAnswer);

            return this.AddSelectedAnswers(answersByQuestionId, result);
        }

        private List<Solutions> ExtractSelectedAnswers(Quiz quiz, QuizEvaluationDto quizSolution)
        {
            var result = quiz.Questions
                .SelectMany(q => q.Answers)
                .Where(a => quizSolution.SelectedAnswerIds.Any(id => id == a.Id))
                .ToList();

            return result;
        }

        private IEvaluationResult CreateEvaluation(UserAnswers userAnswer)
        {
            var result = this.mapper.Map<EvaluationResult>(userAnswer.ForQuiz);

            return result;
        }

        private IDictionary<int, Solutions> GetAnswersByQuestionId(UserAnswers userAnswer)
        {
            var result = userAnswer.SelectedAnswers.ToDictionary(key => key.ForQuestionId);
            return result;
        }

        private IEvaluationResult AddSelectedAnswers(
            IDictionary<int, Solutions> answersByQuestionId, IEvaluationResult evaluation)
        {
            var allQuestions = evaluation.QuestionResults.ToArray();
            evaluation.QuestionResults.Clear();

            foreach (var question in allQuestions)
            {
                if (answersByQuestionId.ContainsKey(question.Id))
                {
                    question.SelectedAnswerId = answersByQuestionId[question.Id].Id;
                    evaluation.QuestionResults.Add(question);
                }
            }

            return evaluation;
        }
    }
}
