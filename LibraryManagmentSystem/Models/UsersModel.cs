using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class UsersModel
    {
        [Key]
        [DisplayName("User ID")]
        public int user_id { get; set; }
        [Required(ErrorMessage = "Please enter the name")]
        [DisplayName("Name")]
        public string name { get; set; }
        [Required(ErrorMessage = "Please enter user name")]
        [DisplayName("User Name")]
        public string user_name { get; set; }
        [Required(ErrorMessage ="Please enter the password")]
        [DisplayName("Password")]
        public string password { get; set; }
        public int ?role_id { get; set; }
        
        //public RoleModel role { get; set; }
    }
}
