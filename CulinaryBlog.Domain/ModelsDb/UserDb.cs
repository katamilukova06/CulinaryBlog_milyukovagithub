using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CulinaryBlog.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CulinaryBlog.Domain.ModelsDb
{
    [Table("user")]
    public class UserDb
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("login")] 
        public string Login { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("email")] 
        public string Email { get; set; }

        [Column("role")] 
        public Role Role { get; set; }


    }


}
