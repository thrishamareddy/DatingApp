using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDtos
    {
        [Required]
        public string UserName{get;set;}
        [Required]
        public string Password { get; set; }
    }
}