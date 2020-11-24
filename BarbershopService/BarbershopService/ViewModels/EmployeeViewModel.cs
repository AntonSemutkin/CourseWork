using System.Collections.Generic;
using BarbershopService.Models;
using BarbershopService.ViewModels.Filters;

namespace BarbershopService.ViewModels
{
    public class EmployeeViewModel
    {
        public EmployeeFilter EmployeeFilter { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}