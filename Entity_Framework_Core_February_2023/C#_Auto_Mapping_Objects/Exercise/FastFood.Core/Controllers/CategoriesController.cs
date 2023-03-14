namespace FastFood.Core.Controllers;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;

using Data;
using FastFood.Models;
using ViewModels.Categories;

public class CategoriesController : Controller
{
    private readonly FastFoodContext _context;
    private readonly IMapper _mapper;

    public CategoriesController(FastFoodContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(CreateCategoryInputModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Error", "Home");
        }

        var category = _mapper.Map<Category>(model);

        _context.Categories.Add(category);

        _context.SaveChanges();

        return RedirectToAction("All", "Categories");
    }

    public IActionResult All()
    {
        var categories = _context.Categories
            .ProjectTo<CategoryAllViewModel>(_mapper.ConfigurationProvider)
            .ToList();

        return View(categories);
    }
}
