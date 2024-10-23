using System.ComponentModel.DataAnnotations;

namespace ProductsAPI.DTO
{
    public class LoginDTO
    {
        // tum bilgileri gondermek istiyorum fakat categoryID gondermek istemiyorum
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}