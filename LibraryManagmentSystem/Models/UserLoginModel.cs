using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LibraryManagementSystem.Models
{
    public class UserLoginModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string user_name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
    }
}
