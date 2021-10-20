using MyCustomTestBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Repository
{
    public class MockRepositoryUser : MockRepositoryBase<User>
    {
        protected override void FillModelForCreate(User modelToFill)
        {
            modelToFill.Id = modelToFill.Id;
            modelToFill.E_Mail = modelToFill.E_Mail;
            modelToFill.UserName = modelToFill.UserName;
            modelToFill.Password = modelToFill.Password;
            modelToFill.Posts = new List<Post>();
            modelToFill.Comments = new List<Comment>();
        }
    }
}
