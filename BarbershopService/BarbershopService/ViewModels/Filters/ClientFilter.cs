using System.Collections.Generic;
using BarbershopService.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarbershopService.ViewModels.Filters
{
    public class ClientFilter
    {
        public int? SelectedServiceTypeId { get; set; }
        public SelectList ServiceTypes { get; set; }
        public int? ReviewMark { get; set; }
        public double? Discount { get; set; }

        public ClientFilter(int? selectedServiceTypeId, IList<ServiceType> serviceTypes, int? reviewMark, double? discount)
        {
            serviceTypes.Insert(0, new ServiceType()
            {
                Id = 0,
                Name = "Все"
            });

            SelectedServiceTypeId = selectedServiceTypeId;
            ServiceTypes = new SelectList(serviceTypes, "Id", "Name", selectedServiceTypeId);
            ReviewMark = reviewMark;
            Discount = discount;
        }
    }
}