using System;
using System.Collections.Generic;
using System.Text;

namespace Mary.item_Song.Model
{
    class Song_class
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string Artist { get; set; } = string.Empty;  
        public string Album { get; set; } = string.Empty;
      
    }
}
