using Customer_API.Helpers;
using Customer_API.Model;
using System.Threading.Tasks;

namespace Customer_API.Services
{
    public interface ICustomerService
    {
        Task<PagedList<Customer>> GetAll(CustomerParams customerParams);
        Task<bool> Update(Customer customer);
        Task<bool> Create(Customer customer);
        Task<Customer> Get(int Id);
        Task<bool> Exists(int id);
        Task<bool> Delete(int id);
    }
}
