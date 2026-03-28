using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HierarchicalItemProcessingSystem.Models;
using HierarchicalItemProcessingSystem.Services;
using HierarchicalItemProcessingSystem.ViewModels;

namespace HierarchicalItemProcessingSystem.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var items = await _itemService.GetAllItemsAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item = new Item
                {
                    Name = model.Name,
                    Weight = model.Weight,
                    Status = "Unprocessed"
                };
                await _itemService.AddItemAsync(item);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null) return NotFound();

            var model = new ItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Weight = item.Weight,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ItemViewModel model)
        {
            if (id != model.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var item = await _itemService.GetItemByIdAsync(id);
                if (item == null) return NotFound();

                item.Name = model.Name;
                item.Weight = model.Weight;
                await _itemService.UpdateItemAsync(item);
                
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _itemService.DeleteItemAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Dashboard()
        {
            var tree = await _itemService.GetItemTreeAsync();
            var items = await _itemService.GetAllItemsAsync();
            var parentItems = items.Where(i => i.Status == "Unprocessed").ToList();

            var model = new DashboardViewModel
            {
                Tree = tree,
                ProcessModel = new ProcessItemViewModel(),
                ParentItems = parentItems
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessFromDashboard(DashboardViewModel inputModel)
        {
            var processModel = inputModel.ProcessModel;
            var items = await _itemService.GetAllItemsAsync();
            var unprocessedItems = items.Where(i => i.Status == "Unprocessed").ToList();
            var tree = await _itemService.GetItemTreeAsync();

            var dashboardModel = new DashboardViewModel
            {
                Tree = tree,
                ProcessModel = processModel,
                ParentItems = unprocessedItems
            };

            if (!ModelState.IsValid || processModel.Children == null || !processModel.Children.Any())
            {
                ModelState.AddModelError("", "Please add at least one valid child item.");
                return View("Dashboard", dashboardModel);
            }

            var childItems = processModel.Children.Select(c => new Item
            {
                Name = c.Name,
                Weight = c.Weight,
                Status = "Unprocessed"
            }).ToList();

            var success = await _itemService.ProcessItemAsync(processModel.ParentId, childItems);
            if (!success)
            {
                ModelState.AddModelError("", "Total child weight cannot exceed parent weight.");
                return View("Dashboard", dashboardModel);
            }

            return RedirectToAction(nameof(Dashboard));
        }
    }
}
