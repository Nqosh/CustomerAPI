using Customer_API.Data;
using Customer_API.Helpers;
using Customer_API.Model;
using System.Linq;
using System.Threading.Tasks;

namespace Customer_API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerDbContext _context;
        public CustomerService(CustomerDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Customer>> GetAll(CustomerParams customerParams)
        {
            var customers = _context.Customers.OrderByDescending(x => x.Id).AsQueryable();
            return await PagedList<Customer>.CreateAsync(customers, customerParams.PageNumber, customerParams.PageSize);
        }

        public async Task<bool> Create(Customer customer)
        {
            var customerCounter = _context.Customers.ToList().Count;
            customerCounter++;
            customer.Id = customerCounter;
            _context.Customers.Add(customer);
            return await Save();
        }

        public async Task<bool> Update(Customer customer)
        {
            this._context.Customers.Update(customer);
            return await Save();
        }

        public async Task<Customer> Get(int Id)
        {
            return this._context.Customers.FirstOrDefault(id => id.Id == Id);
        }

        public async Task<bool> Delete(int id)
        {
            var customer = _context.Customers.Where(x => x.Id == id).FirstOrDefault();
            _context.Customers.Remove(customer);
            return await Save();
        }
        public async Task<bool> Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

        public async Task<bool> Exists(int id)
        {
            return _context.Customers.Any(x => x.Id == id);
        }
    }
}
