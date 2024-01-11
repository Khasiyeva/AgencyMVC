using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [MaxLength(64)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MaxLength(80)]
        [MinLength(5)]
        public string Surname { get; set; }
        [Required]
        [MaxLength(64)]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
