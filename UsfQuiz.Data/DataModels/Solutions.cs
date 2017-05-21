namespace UsfQuiz.Data.DataModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using CommonModels;
    using Commons;


    public class Solutions : IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public int ForQuestionId { get; set; }

        public virtual Questions ForQuestion { get; set; }

        [Required]
        [MinLength(GlobalConstants.AnswerMinLength)]
        [MaxLength(GlobalConstants.AnswerMaxLength)]
        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
