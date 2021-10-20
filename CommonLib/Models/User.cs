using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCustomTestBlog.Models
{
    public class User : IModel, IValidatableObject
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid.")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",
            ErrorMessage = "Email is not valid.")]
        public string E_Mail { get; set; }
        public string Password { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(UserName))
            {
                errors.Add(new ValidationResult("Write first name"));
            }
            if(string.IsNullOrWhiteSpace(E_Mail))
            {
                errors.Add(new ValidationResult("Write correct Email"));
            }
            if(string.IsNullOrWhiteSpace(Password) || Password.Length < 8 || Password.Length > 16)
            {
                errors.Add(new ValidationResult("Write correct Password"));
            }

            return errors;
        }
    }
}
