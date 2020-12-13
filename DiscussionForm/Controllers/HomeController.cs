/* Name - Manjinder Singh
 * Date - December 11, 2020
 * Course - NETD3202
 * Description - This is the home controller page which deal with all the database operations
 *               and also do some of the required validation.
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DiscussionForm.Models;
using Microsoft.AspNetCore.Authorization;

namespace DiscussionForm.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDBContext context;
        public HomeController(AppDBContext dbContext) {
            //to get the Database context to deal with database operations
            this.context = dbContext;
            
        }
        public IActionResult Index()
        {
            return View();

        }
        //returning rules page
        public IActionResult Rules()
        {
            return View();
        }
        [Authorize]
        //to add new topic 
        public ViewResult AddTopic()
        {
            //list of categorie for drop down
            List<Category> categories = context.Categories.ToList<Category>();
  
            ViewBag.CategoryID = categories;
            return View();
             

        }
        [Authorize]
        //to return the list of topics posted
        public ViewResult TopicList()
        {

            return View(context.Topics);


        }

        //to allow user to add topic
        [HttpPost]
        [Authorize]
        public ViewResult AddTopic(Topic model)
        {
            Debug.WriteLine("Add topic");
            //setting the post time
            model.PostTime = DateTime.Now;
            //to get the category based on id
            model.Category=(Category)context.Categories.Where(c => c.CategoryID == model.CategoryID )
                       .FirstOrDefault<Category>();
            if (ModelState.IsValid)
            {

                Debug.WriteLine("Topic added");
                //adding to database
                context.Topics.Add(model);
                context.SaveChanges();
            }
            else
            {
                Debug.WriteLine("Not Added topic");
            }
            return View("TopicList", context.Topics);

        }

        //to show comment on a particular topic by id
        [HttpGet]
        [Authorize]
        public ViewResult ViewComments(int id)
        {
            //getting the topic
            Topic topic = context.Topics.Where(t => t.TopicID == id).FirstOrDefault<Topic>();
            if (topic != null)
            {
                //if found getting the list of comments and showing them on view
                ViewBag.TopicID = id;
                ViewBag.TopicTitle = topic.Title;
                ViewBag.Description = topic.Description;
                List<Comment> comments = context.Comments.Where(c => c.TopicId == id).ToList<Comment>();
                return View(comments);
            }
            else {
                //else redirecting back to the topic list view
                return View("TopicList", context.Topics);
            }
        }

        //to add comment for a particular topic
        //return view with form to add the comment
        [HttpGet]
        [Authorize]
        public ViewResult AddComment(int id)
        {
            Debug.WriteLine("ADD Comment");
            Topic topic = context.Topics.Where(t => t.TopicID == id).FirstOrDefault<Topic>();
            if (topic != null)
            {
                ViewBag.TopicID = id;
                ViewBag.TopicTitle = topic.Title;
                ViewBag.Description = topic.Description;
                return View();
            }
            else
            {//if invalid topic id go back to topic list
                return View("TopicList", context.Topics);
            }
        }


        //to add comment and storing it to database
        [HttpPost]
        [Authorize]
        public RedirectToActionResult AddComment(Comment model)
        {
            Debug.WriteLine("Add comment");
            //to set the post time
            model.PostTime = DateTime.Now;
            if (ModelState.IsValid)
            {

                Debug.WriteLine("Comment added");

                context.Comments.Add(model);
                context.SaveChanges();
            }
            else
            {
                Debug.WriteLine("Not Added topic");
            }
            return RedirectToAction("ViewComments", "Home", new { id = model.TopicId });

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
