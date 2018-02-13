using System.ComponentModel.DataAnnotations;

namespace FileStorage.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Без имени мы вас не узнаём!")]
        [Display(Name = "Логин или e-mail")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        //[StringLength(maximumLength: 50, MinimumLength = 8)]
        public string Password { get; set; }
    }
}