/* Name - Manjinder Singh
 * Date - December 11, 2020
 * Course - NETD3202
 * Description - This is the model page that get and set the category of discussion topic.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionForm.Models
{
    public class Category
    {
        //category of the discussion topic
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CategoryID { get; set; }
        public string Name { get; set; }

        public IList<Topic> Topics { get; set; }
    }
}
