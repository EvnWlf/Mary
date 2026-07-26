using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

namespace Mary.Book.Model
{
    internal class Book_class
    {   public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; } = string.Empty;
        public string Song { get; set; }
        public List Playlist { get; set; }


    }
}
