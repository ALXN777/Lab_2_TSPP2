using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TodoApp
{
    public class ItemService
    {
        private List<Item> _todos = new List<Item>();
        private int _nextId = 1;

        public Item Add(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty");

            var item = new Item(_nextId++, title, false);

            _todos.Add(item);
            return item;
        }

        public List<Item> GetAll()
        {
            return _todos;
        }

        public Item GetById(int id)
        {
            return _todos.FirstOrDefault(t => t.Id == id);
        }

        public void Complete(int id)
        {
            var todo = GetById(id);
            if (todo == null)
                throw new Exception("Item not found");

            todo.IsCompleted = true;
        }

        public List<Item> GetCompleted()
        {
            return _todos.Where(t => t.IsCompleted).ToList();
        }

        public void Delete(int id)
        {
            var todo = GetById(id);
            if (todo == null)
                throw new Exception("Todo not found");

            _todos.Remove(todo);
        }
    }
}
