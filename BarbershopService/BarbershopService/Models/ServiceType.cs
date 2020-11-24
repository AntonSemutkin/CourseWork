using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarbershopService.Models
{
    [Display(Name = "Виды услуг")]
    public class ServiceType
    {
        [Display(Name = "Код")]
        public int Id { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public ServiceType()
        {
            Reviews = new List<Review>();
        }
    }
}
