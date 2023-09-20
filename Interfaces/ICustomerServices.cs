using PequeñoCRUD.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PequeñoCRUD.Interfaces
{
    interface ICustomerServices
    {
        Task<bool> SaveCustomer(Customer customer);

        Task<IEnumerable<Customer>> CustomerAlls();

        Task<Customer> GetDataCustomer(int id);

        Task<bool> DeleteCustomer(int id);

        Task<IEnumerable<Customer>> CustomerAlls(string searched);
    }
}
