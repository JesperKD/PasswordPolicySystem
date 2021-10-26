using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebLoginDemo.Data.FormModels
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Brugernavn er påkrævet!")]
        [StringLength(100, ErrorMessage = "Et brugernavn/email kan maks være 100 karaktere.")]
        public string Username { get; set; }
      
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password er påkrævet!")]
        [StringLength(maximumLength: 100, MinimumLength = 12, ErrorMessage = "Password er for kort (minimum 12 tegn).")]
        public string Password { get; set; }
        

        public int Attempts { get; set; }
    }
}
