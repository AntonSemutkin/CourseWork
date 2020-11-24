using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarbershopService.Models
{
    /// <summary>
    /// TODO:
    /// Фильрация по ServiceTypes, по оценки в Review, по скидке
    /// </summary>
    [Display(Name = "Клиенты")]
    public class Client
    {
        public enum SortState
        {
            FullNameAsc, FullNameDesc,
            AddressAsc, AddressDesc,
            PhoneNumberAsc, PhoneNumberDesc,
            DiscountAsc, DiscountDesc
        }
        [Display(Name = "Код")]
        public int Id { get; set; }
        [Display(Name = "ФИО")]
        public string FullName { get; set; }
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Скидка клиента")]
        public double? Discount { get; set; }

        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public Client()
        {
            Services = new List<Service>();
            Reviews = new List<Review>();
        }
    }
}
