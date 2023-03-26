using Eventmi.Core.Contracts;
using Eventmi.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eventmi.Controllers;

public class EventController : Controller
{
    private readonly IEventService eventService;

    public EventController(IEventService _eventService)
    {
        this.eventService = _eventService;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var model = await this.eventService.GetEventsAsync();

        return View(model);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(EventFormModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await this.eventService.AddAsync(model);

        return RedirectToAction(nameof(All), model);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        EventFormModel model = await this.eventService.GetEventAsync(id);

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        EventFormModel model = await this.eventService.GetEventAsync(id);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EventFormModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await this.eventService.UpdateAsync(model);

        return View("Details", model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await this.eventService.DeleteAsync(id);

        return RedirectToAction(nameof(All));
    }
}
