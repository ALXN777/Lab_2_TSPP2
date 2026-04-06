using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }

        public Item(int id, string title, bool isCompleted)
        {
            Id = id;
            Title = title;
            IsCompleted = isCompleted;
        }

    }
}
