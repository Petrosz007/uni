using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using TodoList.Persistence;
using TodoList.Persistence.DTO;
using TodoList.Persistence.Services;
using TodoList.WebApi.Controllers;
using Xunit;

namespace TodoList.WebApi.Tests
{
    public class ListsControllerTest : IDisposable
    {
        private readonly TodoListContext _context;
        private readonly TodoListService _service;
        private readonly ListsController _controller;

        public ListsControllerTest()
        {
            var options = new DbContextOptionsBuilder<TodoListContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new TodoListContext(options);
            TestDbInitializer.Initialize(_context);
            _service = new TodoListService(_context);
            _controller = new ListsController(_service);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public void GetListsTest()
        {
            // Act
            var result = _controller.GetLists();

            // Assert
            var content = Assert.IsAssignableFrom<IEnumerable<ListDto>>(result.Value);
            Assert.Equal(3, content.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetListByIdTest(Int32 id)
        {
            // Act
            var result = _controller.GetList(id);

            // Assert
            var content = Assert.IsAssignableFrom<ListDto>(result.Value);
            Assert.Equal(id, content.Id);
        }

        [Fact]
        public void GetInvalidListTest()
        {
            // Arrange
            var id = 4;

            // Act
            var result = _controller.GetList(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

        [Fact]
        public void PostListTest()
        {
            // Arrange
            var newList = new ListDto { Name = "New test list" };
            var count = _context.Lists.Count();

            // Act
            var result = _controller.PostList(newList);

            // Assert
            var objectResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            var content = Assert.IsAssignableFrom<ListDto>(objectResult.Value);
            Assert.Equal(count + 1, _context.Lists.Count());
        }
    }
}
