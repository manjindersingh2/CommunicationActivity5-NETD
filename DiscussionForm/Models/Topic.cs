/* Name - Manjinder Singh
 * Date - December 11, 2020
 * Course - NETD3202
 * Description - This is the model page that shows the topic of the discussion.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionForm.Models
{
    public class Topic
    {
        //topic of the discussion
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Topic ID")]
        public int TopicID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        [Display(Name = "Your Name ")]
        public string PostedBy { get; set; }
        [Display(Name = "Post on ")]
        public DateTime PostTime { get; set; }


        public IList<Comment> Comments { get; set; }

        public Category Category { get; set; }
    }
}
