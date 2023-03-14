namespace FastFood.Core.Controllers;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;

using Data;
using FastFood.Models;
using ViewModels.Employees;

public class EmployeesController : Controller
{
    private readonly FastFoodContext _context;
    private readonly IMapper _mapper;

    public EmployeesController(FastFoodContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IActionResult Register()
    {
        var positions = _context.Positions
            .ProjectTo<RegisterEmployeeViewModel>(_mapper.ConfigurationProvider)
            .ToList();

        return View(positions);
    }

    [HttpPost]
    public IActionResult Register(RegisterEmployeeInputModel model)
    {
        ModelState.Remove("PositionName");

        if (!ModelState.IsValid)
        {
            return RedirectToAction("Error", "Home");
        }

        var employee = _mapper.Map<Employee>(model);

        _context.Employees.Add(employee);

        _context.SaveChanges();

        return RedirectToAction("All", "Employees");
    }

    public IActionResult All()
    {
        var employees = _context.Employees
            .ProjectTo<EmployeesAllViewModel>(_mapper.ConfigurationProvider)
            .ToList();

        return View(employees);
    }
}
