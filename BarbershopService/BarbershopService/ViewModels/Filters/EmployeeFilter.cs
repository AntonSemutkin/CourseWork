using System;

namespace BarbershopService.ViewModels.Filters
{
    public class EmployeeFilter
    {
        public DateTime? SelectedServiceDate { get; set; }

        public EmployeeFilter(DateTime? selectedServiceDate)
        {
            SelectedServiceDate = selectedServiceDate;
        }
    }
}