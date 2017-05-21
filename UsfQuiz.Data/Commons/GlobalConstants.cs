namespace UsfQuiz.Data.Commons
{
    public class GlobalConstants
    {
        public const string Admin = "Administrator";

        public const int TitleMinLength = 2;
        public const int TitleMaxLength = 150;

        public const int QuestionMinLength = 2;
        public const int QuestionMaxLength = 850;

        public const int AnswerMinLength = 1;
        public const int AnswerMaxLength = 350;

        public const int NameMinLength = 2;
        public const int NameMaxLength = 50;

        public const int DescriptionMinLength = 5;
        public const int DescriptionMaxLength = 1000;

        public const int TermsConditionsMinLength = 5;
        public const int TermsConditionsMaxLength = 1500;

        public const int UrlMinLength = 10;
        public const int UrlMaxLength = 500;

        public const int UsernameMinLength = 4;
        public const int UsernameMaxLength = 80;

        public const int MinRating = 1;
        public const int MaxRating = 10;

        public const int MinQuestionsCount = 3;

        public const string MinimumLength = "The {0} must be at least {1} characters";
        public const string MaximumLength = "The {0} must be less than {1} characters";

        public const string DefaultCategoryImageUrl = "/Content/images/no-category.jpg";
    }
}
