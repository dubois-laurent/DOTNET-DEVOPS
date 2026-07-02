using aspnet.Models;

namespace aspnet.Data
{
    public class CarCustomerSqlliteRepository : ICarCustomerRepository
    {
        private readonly AppDbContext _context;

        public CarCustomerSqlliteRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CarCustomer> Customers => _context.CarCustomers;

        public bool Add(CarCustomer customer)
        {
            _context.CarCustomers.Add(customer);
            _context.SaveChanges();
            return true;
        }

        public bool Update(CarCustomer customer)
        {
            var existing = _context.CarCustomers.FirstOrDefault(c => c.Id == customer.Id);
            if (existing == null) return false;
            existing.Name = customer.Name;
            existing.Email = customer.Email;
            existing.PhoneNumber = customer.PhoneNumber;
            _context.SaveChanges();
            return true;
        }

        public bool Delete(CarCustomer customer)
        {
            var existing = _context.CarCustomers.FirstOrDefault(c => c.Id == customer.Id);
            if (existing == null) return false;
            _context.CarCustomers.Remove(existing);
            _context.SaveChanges();
            return true;
        }

        public List<string> GetAllCustomers()
        {
            return _context.CarCustomers.Select(c => c.Name).ToList();
        }
    }
}
