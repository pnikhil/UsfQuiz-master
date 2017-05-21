namespace UsfQuiz.Services.DataMappers {
    using AutoMappers;
    using Data.DataModels;
    using Interfaces;

    public class AvailableAnswer : IAvailableAnswer, IMapFrom<Solutions>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }
    }
}
