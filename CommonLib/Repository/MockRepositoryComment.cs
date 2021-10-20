using MyCustomTestBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Repository
{
    public class MockRepositoryComment : MockRepositoryBase<Comment>
    {
        protected override void FillModelForCreate(Comment modelToFill)
        {
            MockRepositoryUser user = new MockRepositoryUser();
            MockRepositoryPost post = new MockRepositoryPost();

            modelToFill.Id = modelToFill.Id;
            modelToFill.Message = modelToFill.Message;
            modelToFill.PostId = modelToFill.PostId;
            modelToFill.User = user.GetItem(modelToFill.User.Id);

            user.GetItem(modelToFill.User.Id).Comments.Add(modelToFill);
            post.GetItem(modelToFill.PostId).Comments.Add(modelToFill);

        }
    }
}
