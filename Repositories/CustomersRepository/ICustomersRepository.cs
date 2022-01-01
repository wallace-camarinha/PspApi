using PspApi.DTO;
using PspApi.Models;

namespace PspApi.Repositories.CustomersRepository
{
    public interface ICustomersRepository
    {
        public Task<Customer?> FindById(Guid id);
        public Task<Customer?> FindByEmail(string email);
        public Task<List<Customer>> ListAll();
        public Task<Customer> Create(CreateOrUpdateCustomerDTO customerData);
        public Task<Customer?> Update(Customer customer, CreateOrUpdateCustomerDTO newData);
        public Task<Customer?> Delete(Customer customer);
    }
}
