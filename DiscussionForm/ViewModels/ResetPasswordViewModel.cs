/* Name - Manjinder Singh
 * Date - December 11, 2020
 * Course - NETD3202
 * Description - This is the model page which shows the reset password form to the user.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionForm.ViewModels
{
    public class ResetPasswordViewModel
    {
        //Reset password form
        [Key]
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        public String Token { get; set; }
    }
}
