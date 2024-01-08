using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class RoleModel
    {
        [Key]
        public int role_id { get; set; }
        public string role_name { get; set;}
    }
}
