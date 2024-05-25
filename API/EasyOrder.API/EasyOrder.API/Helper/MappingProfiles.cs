using EasyOrder.API.Models.Domain;
using AutoMapper;
using EasyOrder.API.Models.DTO;

namespace EasyOrder.API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Administrator, AdministratorDto>();
            CreateMap<AdministratorDto, Administrator>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Bill, BillDto>();
            CreateMap<BillDto, Bill>();
            CreateMap<Brand, BrandDto>();
            CreateMap<BrandDto, Brand>();
            CreateMap<City, CityDto>();
            CreateMap<CityDto, City>();
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<JobPosition, JobPositonDto>();
            CreateMap<JobPositonDto, JobPosition>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<Table, TableDto>();
            CreateMap<TableDto, Table>();
            CreateMap<Food, FoodDto>();
            CreateMap<FoodDto, Food>();
            CreateMap<Supplier, SupplierDto>();
            CreateMap<SupplierDto, Supplier>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();


        }
    }
}
