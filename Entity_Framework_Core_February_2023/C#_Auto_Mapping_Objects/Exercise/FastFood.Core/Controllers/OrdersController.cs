namespace FastFood.Core.Controllers;

using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;

using Data;
using FastFood.Models;
using ViewModels.Orders;

public class OrdersController : Controller
{
    private readonly FastFoodContext _context;
    private readonly IMapper _mapper;

    public OrdersController(FastFoodContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IActionResult Create()
    {
        var viewOrder = new CreateOrderViewModel
        {
            Items = _context.Items.Select(x => new KeyValuePair<int, string>(x.Id, x.Name!)).ToList(),
            Employees = _context.Employees.Select(x => new KeyValuePair<int, string>(x.Id, x.Name!)).ToList(),
        };

        return View(viewOrder);
    }

    [HttpPost]
    public IActionResult Create(CreateOrderInputModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Error", "Home");
        }

        var order = _mapper.Map<Order>(model);
        OrderItem orderItem = new OrderItem() { ItemId = model.ItemId, OrderId = order.Id, Quantity = model.Quantity };

        order.OrderItems!.Add(orderItem);

        _context.Orders.Add(order);

        _context.SaveChanges();

        return RedirectToAction("All", "Orders");
    }

    public IActionResult All()
    {
        var orders = _context.Orders
            .ProjectTo<OrderAllViewModel>(_mapper.ConfigurationProvider)
            .ToList();

        return View(orders);
    }
}
