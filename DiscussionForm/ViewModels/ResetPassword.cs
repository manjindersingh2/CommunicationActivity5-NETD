/* Name - Manjinder Singh
 * Date - December 11, 2020
 * Course - NETD3202
 * Description - This is the model page which helps the user to reset their password.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionForm.ViewModels
{
    public class ResetPassword
    {
        //reset password
        [Key]
        [Required]
        [EmailAddress]
        public String Email { get; set; }
    }
}
