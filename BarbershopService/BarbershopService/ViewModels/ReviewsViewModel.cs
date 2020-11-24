using BarbershopService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarbershopService.ViewModels
{
    public class ReviewsViewModel
    {
        public PageViewModel PageViewModel { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}
