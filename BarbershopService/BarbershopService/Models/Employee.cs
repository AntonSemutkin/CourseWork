using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BarbershopService.Models
{
    /// <summary>
    /// TODO: Фильтрация по услугам выполняющий в данный день
    /// </summary>
    [Display(Name = "Сотрудники")]
    public class Employee
    {
        [Display(Name = "Код")]
        public int Id { get; set; }
        [Display(Name = "ФИО")]
        public string FullName { get; set; }
        [Display(Name = "Стаж")]
        public int Experience { get; set; }
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        public virtual ICollection<Service> Services { get; set; }

        public Employee()
        {
            Services = new List<Service>();
        }
    }
}
