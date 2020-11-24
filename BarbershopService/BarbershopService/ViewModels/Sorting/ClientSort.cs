using BarbershopService.Models;

namespace BarbershopService.ViewModels.Sorting
{
    public class ClientSort
    {
        public Client.SortState FullNameSort { get; set; }
        public Client.SortState AddressSort { get; set; }
        public Client.SortState PhoneNumberSort { get; set; }
        public Client.SortState DiscountSort { get; set; }
        public Client.SortState Current { get; set; }

        public ClientSort(Client.SortState sortOrder)
        {
            FullNameSort = sortOrder == Client.SortState.FullNameAsc
                ? Client.SortState.FullNameDesc
                : Client.SortState.FullNameAsc;
            
            AddressSort = sortOrder == Client.SortState.AddressAsc
                ? Client.SortState.AddressDesc
                : Client.SortState.AddressAsc;
            
            PhoneNumberSort = sortOrder == Client.SortState.PhoneNumberAsc
                ? Client.SortState.PhoneNumberDesc
                : Client.SortState.PhoneNumberAsc;

            DiscountSort = sortOrder == Client.SortState.DiscountAsc
                ? Client.SortState.DiscountDesc
                : Client.SortState.DiscountAsc;

            Current = sortOrder;
        }
    }
}