using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

namespace Mary.Book.Model
{
    internal class Book_class
    {   public int Id { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Song { get; set; } = string.Empty;
        public List<string> Playlist { get; set; } = new List<string>();


    }
}
