using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCustomTestBlog.Models
{
    public class Post : IModel
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
