using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesList.Models
{
    internal class Note
    {
        public string? FileName { get; set; }
        public string? Text { get; set; }
        public DateTime Date { get; set; }
    }
}
