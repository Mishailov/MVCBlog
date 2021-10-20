using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCustomTestBlog.Models
{
    public class User : IModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string E_Mail { get; set; }
        public string Password { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
