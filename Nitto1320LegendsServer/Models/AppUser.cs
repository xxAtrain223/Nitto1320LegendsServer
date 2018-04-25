using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Nitto1320LegendsServer.Models
{
    public enum Gender
    {
        Male,
        Female
    }

    public class AppUser : IdentityUser
    {
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }
        [DataType(DataType.Date)]
        public string BirthYear { get; set; }
        public Gender Gender { get; set; }
        public string ColorCode { get; set; }
    }
}
