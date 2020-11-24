using System.Collections.Generic;
using BarbershopService.Models;
using BarbershopService.ViewModels.Filters;
using BarbershopService.ViewModels.Sorting;

namespace BarbershopService.ViewModels
{
    public class ClientViewModel
    {
        public ClientFilter ClientFilter { get; set; }
        public ClientSort ClientSort { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public IEnumerable<Client> Clients { get; set; }
    }
}
