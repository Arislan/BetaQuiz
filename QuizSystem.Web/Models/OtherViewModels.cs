using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizSystem.Web.Models
{
    public class HomeQuizzesViewModel
    {
        public IEnumerable<QuizViewModel> NewestQuizzes { get; set; }
        public IEnumerable<QuizViewModel> MostRatedQuizzes { get; set; }
    }
}