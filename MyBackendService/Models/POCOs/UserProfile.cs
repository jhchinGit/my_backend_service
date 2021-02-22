using System.ComponentModel.DataAnnotations.Schema;

namespace MyBackendService.Models.POCOs
{
    [Table("user_profile")]
    public class UserProfile
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("salt")]
        public string Salt { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }
    }
}