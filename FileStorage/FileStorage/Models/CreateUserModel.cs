using System.ComponentModel.DataAnnotations;

namespace FileStorage.Models
{
    public class CreateUserModel
    {
        [Required(ErrorMessage = "Без имени нельзя")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Display(Name = "Имя")]
        public string Firstname { get; set; }

        [Display(Name = "Фамилия")]
        public string Lastname { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 50, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Повторите пароль")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 50, MinimumLength = 8)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}