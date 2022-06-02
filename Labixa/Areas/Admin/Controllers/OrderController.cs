using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Outsourcing.Data.Models;
using Outsourcing.Data;
using Outsourcing.Service;
using Labixa.Areas.Admin.ViewModel;
using AutoMapper;
using Outsourcing.Core.Framework.Controllers;

namespace Labixa.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Manager, Employee")]
    public class OrderController : Controller
    {
        private OutsourcingEntities db = new OutsourcingEntities();

        #region Field
        readonly IOrderService _orderService;

        readonly IOrderItemService _orderItemService;

        #endregion

        #region Ctor
        public OrderController(IOrderService orderService, IOrderItemService orderItemService)
        {
            this._orderService = orderService;
            this._orderItemService = orderItemService;
        }
        #endregion
        [Authorize(Roles = "Admin,Manager")]

        public ActionResult Index()
        {
            var orders = _orderService.GetOrders();

            return View(model: orders);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var order = new OrderFormModel();
      
            return View(order);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Create(OrderFormModel newOrder, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                Order order = Mapper.Map<OrderFormModel, Order>(newOrder);
                order.Deleted = false;
                _orderService.CreateOrder(order);
                return continueEditing ? RedirectToAction("Edit", "Order", new { id = order.Id })
                                : RedirectToAction("Index", "Order");
            }
            else
            {
                return View("Create", newOrder);
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int orderId)
        {
            var order = _orderService.GetOrderById(orderId);
            OrderFormModel orderFormModel = Mapper.Map<Order, OrderFormModel>(order);
            return View(model: orderFormModel);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateInput(false)]
        public ActionResult Edit(OrderFormModel newOrder, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var order = Mapper.Map<OrderFormModel, Order>(newOrder);
                _orderService.EditOrder(order);
                return continueEditing ? RedirectToAction("Edit", "Order", new { id = order.Id })
                 : RedirectToAction("Index", "Order");
            }
            else
            {
                return View("Edit", newOrder);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int orderId)
        {
            _orderService.DeleteOrder(orderId);
            return RedirectToAction("Index");
        }
        public ActionResult OrderDetail(int orderitemID)
        {
            var orders = _orderService.GetOrderById(orderitemID);
            return View(orders);
        }
    }
}
