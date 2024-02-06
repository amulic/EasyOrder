using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Interface
{
    public interface ITableRepository
    {
        Table GetTable(int id);
        ICollection<Table> GetTables();
        bool CreateTable(Table table);
        bool TableExists(int tableId);
        bool Save();
    }
}
