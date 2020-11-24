using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarbershopService.Models
{
    [Display(Name = "Услуги")]
    public class Service
    {
        public enum SortState
        {
            DateServiceAsc, DateServiceDesc,
            DescriptionAsc, DescriptionDesc,
            PriceAsc, PriceDesc,
            ClientAsc, ClientDesc,
            ServiceTypeAsc, ServiceTypeDesc,
            EmployeeAsc, EmployeeDesc
        }

        [Display(Name = "Код услуги")]
        public int Id { get; set; }
        [Display(Name = "Дата заказа")]
        public DateTime DateService { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        public int ClientId { get; set; }
        public int ServiceTypeId { get; set; }
        public int EmployeeId { get; set; }

        [Display(Name = "Вид услуги")]
        public virtual ServiceType ServiceType { get; set; }
        [Display(Name = "Клиент")]
        public virtual Client Client { get; set; }
        [Display(Name = "Сотрудник")]
        public virtual Employee Employee { get; set; }
    }
}
