using BarbershopService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarbershopService.ViewModels.Sorting
{
    public class ServicesSort
    {
        public Service.SortState DateServiceSort { get; private set; }
        public Service.SortState DescriptionSort { get; private set; }
        public Service.SortState PriceSort { get; private set; }
        public Service.SortState ClientSort { get; private set; }
        public Service.SortState EmployeeSort { get; private set; }
        public Service.SortState ServiceTypeSort { get; private set; }

        public Service.SortState Current { get; private set; }

        public ServicesSort(Service.SortState sortOrder)
        {
            DateServiceSort = sortOrder == Service.SortState.DateServiceAsc ? Service.SortState.DateServiceDesc
                : Service.SortState.DateServiceAsc;

            DescriptionSort = sortOrder == Service.SortState.DescriptionAsc ? Service.SortState.DescriptionDesc
                : Service.SortState.DescriptionAsc;

            PriceSort = sortOrder == Service.SortState.PriceAsc ? Service.SortState.PriceDesc
                : Service.SortState.PriceAsc;

            ClientSort = sortOrder == Service.SortState.ClientAsc ? Service.SortState.ClientDesc
                : Service.SortState.ClientAsc;

            EmployeeSort = sortOrder == Service.SortState.EmployeeAsc ? Service.SortState.EmployeeDesc
                : Service.SortState.EmployeeAsc;

            ServiceTypeSort = sortOrder == Service.SortState.ServiceTypeAsc ? Service.SortState.ServiceTypeDesc
                : Service.SortState.ServiceTypeAsc;

            Current = sortOrder;
        }
    }
}
