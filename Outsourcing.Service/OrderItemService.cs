using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Repository;
using Outsourcing.Service.Properties;

namespace Outsourcing.Service
{
    public interface IOrderItemService
    {

        IEnumerable<OrderItem> GetOrderItems();
        OrderItem GetOrderItemById(int orderItemId);
        void CreateOrderItem(OrderItem orderItem);
        void EditOrderItem(OrderItem orderItemToEdit);
        void DeleteOrderItem(int orderItemId);
        void SaveOrderItem();
        IEnumerable<ValidationResult> CanAddOrderItem(OrderItem orderItem);

    }
    public class OrderItemService : IOrderItemService
    {
        #region Field
        private readonly IOrderItemRepository orderItemRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public OrderItemService(IOrderItemRepository orderItemRepository, IUnitOfWork unitOfWork)
        {
            this.orderItemRepository = orderItemRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<OrderItem> GetOrderItems()
        {
            var orderItems = orderItemRepository.GetAll();
            return orderItems;
        }

        public OrderItem GetOrderItemById(int orderItemId)
        {
            var orderItem = orderItemRepository.GetAll().Where(p=>p.Id==orderItemId).FirstOrDefault();
            return orderItem;
        }

        public void CreateOrderItem(OrderItem orderItem)
        {
            orderItemRepository.Add(orderItem);
            SaveOrderItem();
        }

        public void EditOrderItem(OrderItem orderItemToEdit)
        {
            orderItemRepository.Update(orderItemToEdit);
            SaveOrderItem();
        }

        public void DeleteOrderItem(int orderItemId)
        {
            //Get orderItem by id.
            var orderItem = orderItemRepository.GetById(orderItemId);
            if (orderItem != null)
            {
                orderItemRepository.Delete(orderItem);
                SaveOrderItem();
            }
        }

        public void SaveOrderItem()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddOrderItem(OrderItem orderItem)
        {
        
            //    yield return new ValidationResult("OrderItem", "ErrorString");
            return null;
        }

        #endregion
    }
}
