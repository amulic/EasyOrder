using EasyOrder.API.Data;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Repository
{
    public class TableRepository : ITableRepository
    {
        private ApplicationDbContext _context;

        public TableRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CreateTable(Table table)
        {
            _context.Tables.Add(table);
            return Save();
        }

        public Table GetTable(int id)
        {
            return _context.Tables.Where(x => x.Id == id).FirstOrDefault();
        }

        public ICollection<Table> GetTables()
        {
            return _context.Tables.ToList();
        }


        public bool TableExists(int tableId)
        {
            return _context.Tables.Any(x => x.Id == tableId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
