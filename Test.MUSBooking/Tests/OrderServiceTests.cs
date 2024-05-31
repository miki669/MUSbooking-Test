using Microsoft.EntityFrameworkCore;
using MUSbooking.Context;
using MUSbooking.Exceptions;
using MUSbooking.Model;
using MUSbooking.Model.Dto;
using MUSbooking.Services;

namespace Test.MUSBooking.Tests;

public class OrderServiceTests
    {
        private readonly OrderServices _service;
        private readonly PrimaryDataBaseContext _context;

        public OrderServiceTests()
        {
            var options = new DbContextOptionsBuilder<PrimaryDataBaseContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new PrimaryDataBaseContext(options);
            _service = new OrderServices(_context);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldAddNewOrder_WhenOrderIsValid()
        {
            var equipment = new Equipment
            {
                Id = Guid.NewGuid(),
                Name = "Test Equipment",
                Amount = 10,
                Price = 100.0m
            };
            _context.Equipments.Add(equipment);
            await _context.SaveChangesAsync();

            var orderDto = new OrderDto
            {
                Description = "Test Order",
                OrderEquipmentsDto = new List<OrderEquipmentDto>
                {
                    new OrderEquipmentDto
                    {
                        EquipmentId = equipment.Id,
                        Quantity = 2
                    }
                }
            };

            var result = await _service.CreateOrderAsync(orderDto);

            Assert.NotNull(result);
            Assert.Equal(orderDto.Description, result.Description);
            Assert.Single(result.OrderEquipments);
        }

        

        [Fact]
        public async Task GetOrderAsync_ShouldReturnOrder_WhenOrderExists()
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                Description = "Test Order",
                CreatedAt = DateTime.UtcNow,
                Price = 200.0m
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var result = await _service.GetOrderAsync(order.Id);

            Assert.NotNull(result);
            Assert.Equal(order.Description, result.Description);
        }

        [Fact]
        public async Task GetOrderAsync_ShouldThrowNotFoundException_WhenOrderDoesNotExist()
        {
            var nonExistentId = Guid.NewGuid();
            await Assert.ThrowsAsync<OrderNotFoundException>(() => _service.GetOrderAsync(nonExistentId));
        }

        [Fact]
        public async Task DeleteOrderAsync_ShouldRemoveOrder_WhenOrderExists()
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                Description = "Test Order",
                CreatedAt = DateTime.UtcNow,
                Price = 200.0m
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            await _service.DeleteOrderAsync(order.Id);

            var result = await _context.Orders.FindAsync(order.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteOrderAsync_ShouldThrowNotFoundException_WhenOrderDoesNotExist()
        {
            var nonExistentId = Guid.NewGuid();
            await Assert.ThrowsAsync<OrderNotFoundException>(() => _service.DeleteOrderAsync(nonExistentId));
        }
    }