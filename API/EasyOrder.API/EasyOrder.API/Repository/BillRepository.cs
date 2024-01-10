using EasyOrder.API.Data;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Repository
{
    public class BillRepository : IBillRepository
    {
        private ApplicationDbContext _context;

        public BillRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public bool BillExists(int billId)
        {
            return _context.Bills.Any(x => x.Id == billId);
        }

        public bool CreateBill(Bill bill)
        {
            _context.Add(bill);
            return Save();
        }

        public Bill GetBill(int billId)
        {
            return _context.Bills.Where(x => x.Id == billId).FirstOrDefault();
        }

        public ICollection<Bill> GetBills()
        {
            return _context.Bills.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true: false;
        }
    }
}
