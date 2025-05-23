using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Display(Name = "Введите почту")]
        [Required(ErrorMessage = "Вам нужно ввести почту")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            ErrorMessage = "Почта должна содержать символ @ и быть в формате example@domain.com")]
        public string Email { get; set; }

        [Display(Name = "Введите сообщение")]
        [StringLength(1000, ErrorMessage = "Текст должен быть не более 1000 символов")]
        [Required(ErrorMessage = "Вам нужно ввести сообщение")]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
