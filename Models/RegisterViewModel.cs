using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Почта обязательна для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный формат электронной почты")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Телефон обязателен для заполнения")]
        [RegularExpression(@"^(\+7|8)[\s\-]?\(?[489][0-9]{2}\)?[\s\-]?[0-9]{3}[\s\-]?[0-9]{2}[\s\-]?[0-9]{2}$", 
            ErrorMessage = "Введите корректный номер РФ (например: +79991234567 или 89991234567)")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пароль обязателен")]
        [MinLength(6, ErrorMessage = "Пароль должен содержать минимум 6 символов")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}