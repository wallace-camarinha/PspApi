using Microsoft.EntityFrameworkCore;
using PspApi.Data;
using PspApi.DTO;
using PspApi.Models;

namespace PspApi.Repositories.CustomersRepository
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly DatabaseContext _context;
        public CustomersRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Customer> Create(CreateOrUpdateCustomerDTO customerData)
        {
            var customer = new Customer
                (
                    firstName: customerData.FirstName,
                    lastName: customerData.LastName,
                    email: customerData.Email,
                    documentType: customerData.DocumentType,
                    documentNumber: customerData.DocumentNumber
                );

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer?> Delete(Customer customer)
        {
            customer!.Active = false;
            customer.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer?> FindByEmail(string email)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Email == email);
            return customer;
        }

        public async Task<Customer?> FindById(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            return customer;
        }

        public async Task<List<Customer>> ListAll()
        {
            var result = await _context.Customers.Where(x => x.Active).ToListAsync();

            return result;
        }

        public async Task<Customer?> Update(Customer customer, CreateOrUpdateCustomerDTO newData)
        {
            customer.FirstName = newData.FirstName ?? customer.FirstName;
            customer.LastName = newData.LastName ?? customer.LastName;
            customer.Email = newData.Email ?? customer.Email;
            customer.DocumentType = newData.DocumentType ?? customer.DocumentType;
            customer.DocumentNumber = newData.DocumentNumber ?? customer.DocumentNumber;

            customer.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return customer;
        }
    }
}
