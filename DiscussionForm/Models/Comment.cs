/* Name - Manjinder Singh
 * Date - December 11, 2020
 * Course - NETD3202
 * Description - This is the model page that shows all the values of the comments posted 
 *               on the discussion topic.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionForm.Models
{
    public class Comment
    {
        //comments posted on the discussion topic
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Comment ID")]
        public int CommentID { get; set; }
        public string Description { get; set; }
        [Display(Name = "Your Name ")]
        public string PostedBy { get; set; }
        [Display(Name = "Post on ")]
        public DateTime PostTime { get; set; }
        [Display(Name = "Topic ID")]
        public int TopicId { get; set; }
        public Topic CommentTopic { get; set; }

    }
}
