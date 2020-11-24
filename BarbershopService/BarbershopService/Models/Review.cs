using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarbershopService.Models
{
    [Display(Name = "Отзывы")]
    public class Review
    {
        [Key]
        [ForeignKey("Service")]
        [Display(Name = "Код")]
        public int Id { get; set; }
        [Display(Name = "Оценка пользователя")]
        public int ClientMark { get; set; }

        [Display(Name = "Клиент")]
        public int ClientId { get; set; }
        [Display(Name = "Вид услуги")]
        public int ServiceTypeId { get; set; }

        public virtual ServiceType ServiceType { get; set; }
        public virtual Client Client { get; set; }
    }
}
