using MyCustomTestBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Repository
{
    public class MockRepositoryPost : MockRepositoryBase<Post>
    {
        protected override void FillModelForCreate(Post modelToFill)
        {
            MockRepositoryUser user = new MockRepositoryUser();

            modelToFill.Id = modelToFill.Id;
            modelToFill.Message = modelToFill.Message;
            modelToFill.User = user.GetItem(modelToFill.User.Id);
            modelToFill.Comments = new List<Comment>();

            user.GetItem(modelToFill.User.Id).Posts.Add(modelToFill);
        }
    }
}
