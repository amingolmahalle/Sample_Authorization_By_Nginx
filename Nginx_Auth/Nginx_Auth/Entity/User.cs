using System.ComponentModel.DataAnnotations;

namespace Nginx_Auth.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
    }
}