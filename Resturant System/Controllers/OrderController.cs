using Application.DTO;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Resturant_System.Data;
using Resturant_System.Models;
using Resturant_System.ViewModel;

namespace Resturant_System.Controllers
{
    public class OrderController: Controller
    {


        private readonly IOrderService orderService;
        public OrderController( IOrderService _orderservice)
        {



            orderService = _orderservice;
        }
        public async Task<IActionResult> index() { 

        var allorders = await orderService.GetallitemsAsync();
            ViewBag.OrderTypes = Enum.GetValues(typeof(orderkind))
             .Cast<orderkind>()
            .Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() })
      .ToList();

            ViewBag.OrderStatuses = Enum.GetValues(typeof(orderstaues))
                .Cast<orderstaues>()
                .Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() })
                .ToList();

            return View(allorders); 
        
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var menuDtos = await orderService.GetAvailableMenuItemsAsync();
            var model = new OrderCreateViewModel
            {
                OrderItems = new List<OrderItemViewModel> { new OrderItemViewModel() },
                menuesitems = menuDtos.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                }).ToList()
            };

            return View("CreateForm", model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateViewModel viewModel)
        {
            var dto = new OrderCreateDto
            {
                CreatedAt = viewModel.CreatedAt,
                DeliveryAddress = viewModel.DeliveryAddress,
                Type = viewModel.Type,
                Items = viewModel.OrderItems.Select(i => new OrderItemDto
                {
                    MenuItemId = i.MenuItemId,
                    Quantity = i.Quantity
                }).ToList()
            };

            bool success = await orderService.CreateOrderAsync(dto);

            if (!success)
            {
                TempData["Error"] = "Some items are not available or exceed the 50 orders/day limit.";
                return RedirectToAction("Create");
            }

            TempData["Success"] = "Order created successfully.";
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> EditOrder(Order order)
        {
            var existingOrder = await orderService.GetOrderForEditAsync(order.Id);
            if (existingOrder == null)
            {
                TempData["Error"] = "Order not found.";
                return RedirectToAction("Index");
            }

            existingOrder.Type = order.Type;
            existingOrder.Status = order.Status;
            existingOrder.DeliveryAddress = order.DeliveryAddress;
            existingOrder.Total = order.Total;

           await orderService.UpdateOrder(existingOrder);
            TempData["Success"] = "Order updated successfully.";
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var deletedOrder = await orderService.GetOrderForEditAsync(id);
          

            if (deletedOrder == null)
            {
                TempData["Error"] = "Order not found.";
                return RedirectToAction("Index");
            }

            if (deletedOrder.Type == orderkind.delivery)
            {
                TempData["Error"] = "Cannot delete delivery orders.";
                return RedirectToAction("Index");
            }

           await orderService.DeleteOrderAsync(id);
            TempData["Success"] = "Order deleted successfully.";
            return RedirectToAction("Index");
        }

    }
}
