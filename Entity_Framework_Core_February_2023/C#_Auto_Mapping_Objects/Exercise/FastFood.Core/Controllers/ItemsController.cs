namespace FastFood.Core.Controllers;


using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;

using Data;
using FastFood.Models;
using ViewModels.Items;

public class ItemsController : Controller
{
    private readonly FastFoodContext _context;
    private readonly IMapper _mapper;

    public ItemsController(FastFoodContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Create()
    {
        var itemCategories = _context.Categories
            .ProjectTo<CreateItemViewModel>(_mapper.ConfigurationProvider)
            .ToList();

        return View(itemCategories);
    }

    [HttpPost]
    public IActionResult Create(CreateItemInputModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Error", "Home");
        }

        var items = _mapper.Map<Item>(model);

        _context.Items.Add(items);

        _context.SaveChanges();

        return RedirectToAction("All", "Items");
    }

    public IActionResult All()
    {
        var items = _context.Items
            .ProjectTo<ItemsAllViewModels>(_mapper.ConfigurationProvider)
            .ToList();

        return View(items);
    }
}
