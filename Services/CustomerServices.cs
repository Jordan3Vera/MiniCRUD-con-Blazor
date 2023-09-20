using PequeñoCRUD.Data;
using PequeñoCRUD.Repository;
using System.Threading.Tasks;
using System.Collections.Generic;
using PequeñoCRUD.Interfaces;


namespace PequeñoCRUD.Interfaces
{
    public class CustomerServices : ICustomerServices
    {
        private ICustomerRepository customersRepository;
        private SqlConfiguration configuration;

        public CustomerServices(SqlConfiguration c)
        {
            configuration = c;
            customersRepository = new CustomerRepository(configuration.StringConection);
        }

        public Task<bool> SaveCustomer(Customer customer)
        {
            if (customer.Id == 0)
                return customersRepository.SaveCustomer(customer);
            else
                return customersRepository.EditCustomer(customer);
        }

        public Task<IEnumerable<Customer>> CustomerAlls() 
        {
            return customersRepository.CustomerAlls();
        }

        public Task<Customer> GetDataCustomer(int id)
        {
            return customersRepository.GetDataCustomer(id);
        }

        public Task<bool> DeleteCustomer(int id) 
        {
            return customersRepository.DeleteCustomer(id);
        }

        public Task<IEnumerable<Customer>> CustomerAlls(string searched)
        {
            return customersRepository.CustomerAlls(searched);
        }
    }
}
