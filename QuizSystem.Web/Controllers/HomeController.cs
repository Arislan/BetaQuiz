using QuizSystem.Model;
using QuizSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using QuizSystem.Web.Libs.Helpers;

namespace QuizSystem.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            IQueryable<Quiz> query = this.context.Quizzes.All();

            if (this.User != null)
            {
                string userId = this.User.Identity.GetUserId();
                query = query.Where(x => x.State == QuizState.Active &&
                            !x.Results.Any(r => r.UserId == userId));
            }
          
            HomeQuizzesViewModel model = new HomeQuizzesViewModel();

            model.NewestQuizzes = query.OrderByDescending(x => x.PublishDate)
                           .Take(6)
                           .Select(ModelConvertor.QuizToViewModel);

            model.MostRatedQuizzes = query.OrderByDescending(x => x.Rating)
                           .Take(6)
                           .Select(ModelConvertor.QuizToViewModel);

            return View(model);
        }
    }
}