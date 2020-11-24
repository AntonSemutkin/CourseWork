using System.Collections.Generic;
using BarbershopService.Models;
using BarbershopService.ViewModels.Sorting;

namespace BarbershopService.ViewModels
{
    public class ServiceViewModel
    {
        public ServicesSort ServicesSort { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public IEnumerable<Service> Services { get; set; }
    }
}
