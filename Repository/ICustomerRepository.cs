using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PequeñoCRUD.Data;

namespace PequeñoCRUD.Repository
{
    interface ICustomerRepository
    {
        Task<bool> SaveCustomer(Customer customer);

        Task<IEnumerable<Customer>> CustomerAlls(); 

        Task<Customer> GetDataCustomer(int id);

        Task<bool> EditCustomer(Customer customer);

        Task<bool> DeleteCustomer(int id);

        Task<IEnumerable<Customer>> CustomerAlls(String searched);
    }
}
