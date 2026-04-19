using Moq;
using MockQueryable.Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Entities;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace Repositories.Tests
{
    public class OrderRepositoryUnitTests
    {
        private readonly Mock<ShopContext> _mockContext;
        private readonly OrderRepository _repository;

        public OrderRepositoryUnitTests()
        {
            _mockContext = new Mock<ShopContext>();
            _repository = new OrderRepository(_mockContext.Object);
        }

        [Fact]
        public async Task GetOrderById_ReturnsOrder_WithOrderItems_WhenOrderExists()
        {
            // Arrange
            var orderId = 1;
            var order = new Order
            {
                OrderId = orderId,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { OrderItemId = 1, ProductId = 1, Quantity = 2 },
                    new OrderItem { OrderItemId = 2, ProductId = 2, Quantity = 1 }
                }
            };

            var mockOrderSet = new List<Order> { order }.AsQueryable().BuildMockDbSet();
            _mockContext.Setup(m => m.Orders).Returns(mockOrderSet.Object);

            // Act
            var result = await _repository.GetOrderById(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderId, result.OrderId);
            Assert.Equal(2, result.OrderItems.Count);
        }

        // Test for GetOrderById when the order does not exist
        [Fact]
        public async Task GetOrderById_ReturnsNull_WhenOrderDoesNotExist()
        {
            // Arrange
            var mockOrderSet = new List<Order>().AsQueryable().BuildMockDbSet();
            _mockContext.Setup(m => m.Orders).Returns(mockOrderSet.Object);

            // Act
            var result = await _repository.GetOrderById(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddOrder_ReturnsOrder_WhenOrderIsAdded()
        {
            // Arrange
            var order = new Order { OrderId = 1, OrderItems = new List<OrderItem>() };
            var mockOrderSet = new List<Order>().AsQueryable().BuildMockDbSet();
            _mockContext.Setup(m => m.Orders).Returns(mockOrderSet.Object);
            _mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var result = await _repository.AddOrder(order);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order.OrderId, result.OrderId);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AddOrder_ShouldCallAddAsync_WhenOrderIsAdded()
        {
            // Arrange
            var order = new Order { OrderId = 2, OrderItems = new List<OrderItem>() };
            var mockOrderSet = new List<Order>().AsQueryable().BuildMockDbSet();
            _mockContext.Setup(m => m.Orders).Returns(mockOrderSet.Object);
            _mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            await _repository.AddOrder(order);

            // Assert
            mockOrderSet.Verify(m => m.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
