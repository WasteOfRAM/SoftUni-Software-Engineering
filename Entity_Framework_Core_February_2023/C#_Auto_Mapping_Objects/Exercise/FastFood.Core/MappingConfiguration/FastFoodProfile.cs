namespace FastFood.Core.MappingConfiguration;

using System.Globalization;
using AutoMapper;

using FastFood.Models;
using ViewModels.Categories;
using ViewModels.Employees;
using ViewModels.Items;
using ViewModels.Orders;
using ViewModels.Positions;

public class FastFoodProfile : Profile
{
    public FastFoodProfile()
    {
        //Positions
        CreateMap<CreatePositionInputModel, Position>()
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.PositionName));

        CreateMap<Position, PositionsAllViewModel>();

        // Categories
        CreateMap<CreateCategoryInputModel, Category>()
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.CategoryName));

        CreateMap<Category, CategoryAllViewModel>();

        // Items
        CreateMap<CreateItemInputModel, Item>();

        CreateMap<Category, CreateItemViewModel>()
            .ForMember(d => d.CategoryId, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Name));

        CreateMap<Item, ItemsAllViewModels>()
            .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category.Name));

        // Emmployees
        CreateMap<Employee, EmployeesAllViewModel>()
            .ForMember(d => d.Position, opt => opt.MapFrom(s => s.Position.Name));

        CreateMap<RegisterEmployeeInputModel, Employee>();

        CreateMap<Position, RegisterEmployeeViewModel>()
            .ForMember(d => d.PositionId, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.PositionName, opt => opt.MapFrom(s => s.Name));

        //Orders
        CreateMap<CreateOrderInputModel, OrderItem>();

        CreateMap<CreateOrderInputModel, Order>()
            .AfterMap((s, d) => d.DateTime = DateTime.Now);

        CreateMap<Order, OrderAllViewModel>()
            .ForMember(d => d.OrderId, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee.Name))
            .ForMember(d => d.DateTime, opt => opt.MapFrom(s => s.DateTime.ToString("dd-MM-yyyy H:mm", CultureInfo.InvariantCulture)));
    }
}
