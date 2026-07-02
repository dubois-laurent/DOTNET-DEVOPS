using aspnet.Models;

namespace aspnet.Data
{
    public interface ICarCustomerRepository
    {
        public IEnumerable<CarCustomer> Customers { get; }
        public bool Add(CarCustomer customer);
        public bool Update(CarCustomer customer);
        public bool Delete(CarCustomer customer);
        public List<string> GetAllCustomers();
    }
}