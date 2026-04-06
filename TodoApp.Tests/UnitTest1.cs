using System;
using System.Linq;
using Xunit;
using TodoApp;

namespace TodoApp.Tests
{
    public class ItemServiceTests
    {
        [Fact]
        public void Add_ValidTitle_SavesItemAndReturnsIt()
        {
           
            var service = new ItemService();

           
            var result = service.Add("Зробити лабу");

           
            Assert.NotNull(result);
            Assert.Equal("Зробити лабу", result.Title);
            Assert.False(result.IsCompleted); 
            Assert.Single(service.GetAll()); 
        }

        [Fact]
        public void Add_EmptyTitle_ThrowsError()    
        {
            var service = new ItemService();

            var ex = Assert.Throws<ArgumentException>(() => service.Add(""));

            Assert.Equal("Title cannot be empty", ex.Message);
        }

        [Fact]
        public void GetAll_ReturnsAllAddedItems()
        {
            var service = new ItemService();
            service.Add("Завдання 1");
            service.Add("Завдання 2");

            var items = service.GetAll();

            Assert.Equal(2, items.Count);
        }

        [Fact]
        public void GetById_WhenItemExists_ReturnsCorrectItem()
        {
            var service = new ItemService();
            var item = service.Add("Тест пошуку");

            var found = service.GetById(item.Id);

            Assert.NotNull(found);
            Assert.Equal(item.Id, found.Id);
        }

        [Fact]
        public void Complete_ChangesStatusToTrue()
        {
            var service = new ItemService();
            var item = service.Add("Треба виконати");

            service.Complete(item.Id);
            var checkItem = service.GetById(item.Id);

            Assert.True(checkItem.IsCompleted);
        }

        [Fact]
        public void Complete_WrongId_ThrowsException()
        {
            var service = new ItemService();

            var error = Assert.Throws<Exception>(() => service.Complete(999));
            Assert.Equal("Item not found", error.Message);
        }

        [Fact]
        public void GetCompleted_ReturnsOnlyFinishedTasks()
        {
            var service = new ItemService();
            var task1 = service.Add("Перше (виконано)");
            var task2 = service.Add("Друге (в процесі)");

            service.Complete(task1.Id); 
            var completedList = service.GetCompleted();

            Assert.Single(completedList);
            Assert.Equal("Перше (виконано)", completedList.First().Title);
        }

        [Fact]
        public void Delete_RemovesItemFromList()
        {
            var service = new ItemService();
            var item = service.Add("Це треба видалити");

            service.Delete(item.Id);

            Assert.Empty(service.GetAll()); 
        }

        [Fact]
        public void Delete_WrongId_ThrowsException()
        {
            var service = new ItemService();

            var error = Assert.Throws<Exception>(() => service.Delete(777));
            Assert.Equal("Todo not found", error.Message);
        }
    }
}