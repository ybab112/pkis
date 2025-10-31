using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Поле Имя обязательно")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Range(18, 100, ErrorMessage = "Возраст должен быть от 18 до 100")]
        [Display(Name = "Возраст")]
        public int Age { get; set; }
    }
}