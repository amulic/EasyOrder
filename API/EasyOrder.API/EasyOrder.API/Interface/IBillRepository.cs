using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Interface
{
    public interface IBillRepository
    {
        Bill GetBill(int id);
        ICollection<Bill> GetBills();
        bool CreateBill(Bill bill);
        bool BillExists(int billId);
        bool Save();
    }
}
