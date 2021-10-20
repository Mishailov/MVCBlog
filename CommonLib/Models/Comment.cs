using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCustomTestBlog.Models
{
    public class Comment : IModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Message { get; set; }

        public User User { get; set; }
    }
}
