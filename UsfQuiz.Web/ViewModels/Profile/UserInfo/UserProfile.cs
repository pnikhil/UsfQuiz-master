namespace UsfQuiz.Web.ViewModels.Profile.UserInfo
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Commons;
    using Data.DataModels;
    using Services.AutoMappers;

    public class UserProfile : IHaveCustomMappings, IMapFrom<User>, IMapTo<User>
    {
        public string Id { get; set; }

        [Display(Name = "Username")]
        [Required]
        [MinLength(GlobalConstants.UsernameMinLength, ErrorMessage = GlobalConstants.MinimumLength)]
        [MaxLength(GlobalConstants.UsernameMaxLength, ErrorMessage = GlobalConstants.MaximumLength)]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }

        [Display(Name = "First Name")]
        [Required]

        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }
        public static int MaxQuizzesCreated { get; set; }

        public int QuizzesCreated { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<User, UserProfile>()
                .ForMember(
                    self => self.QuizzesCreated,
                    opt => opt.MapFrom(model => model.QuizzesCreated.Count));
        }
    }
}
