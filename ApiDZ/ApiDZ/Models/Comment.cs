using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDZ.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string ProductName { get; set; }
        public int Rating { get; set; }
        public string CommentText { get; set; }
        public string Img { get; set; }
        public string Video { get; set; }
    }
}
