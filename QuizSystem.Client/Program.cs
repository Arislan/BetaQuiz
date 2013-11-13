using QuizSystem.Data;
using QuizSystem.Model;
using QuizSystem.Web.Context;
using QuizSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuizSystem.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(Regex.IsMatch("f$3$Title", @"f\$\d+\$\w+"));
            DateTime? hola = DateTime.Now;

            var t = hola.GetType();
            Console.WriteLine(t);

            //var db = new QuizUnitOfWork(new QuizContext());
            //var q = db.Questions.GetById(1);

            //var sd = q.Answers.ToDictionary(x => x.Content, x => x.Questions.Count);
            
            //foreach(var a in sd)
            //{
            //    Console.WriteLine(a.Key  + " | " + " | " + a.Value);
            //}

            //db.Answers.Remove(153);
            //db.SaveChanges();
        //    db.Categories.Add(new Category { Name = "OLA" });
        //    db.Quizzes.Add(new Quiz { Title = "AHA QUIZ", CreatorId = "92",CategoryId = 2 });
        //    db.SaveChanges();
        //    var q = new Question { Content = "TEST", QuizId = 3 };

        //    var a1 = new Answer { Content = "a1" };
        //    var a2 = new Answer { Content = "a2" };
        //    var a3 = new Answer { Content = "a3" };
        //    var a4 = new Answer { Content = "a4" };
        //    var a5 = new Answer { Content = "a5" };
        //    var a6 = new Answer { Content = "a6" };
        //    q.Answers.Add(new Answer { Content = "a7" });
        //    var arr = new[] { a1, a2, a3, a4, a5, a6 };
        //    q.Answers = arr;
        //    //q.Answers.Add(db.Answers.All().FirstOrDefault(x => x.Content == "a1"));
        //    //q.Answers.Add(a2);
        //    //q.Answers.Add(a3);
        //    //q.Answers.Add(a4);
        //    //q.Answers.Add(a5);
        //    //q.Answers.Add(a6);
        //    for (int i = 0; i < arr.Length; i++)
        //    {
        //        db.Answers.Add(arr[i]);
        //    }
        //    q.RightAnswerId = a1.Id;
        //    q = db.Questions.Add(q);

        //    db.SaveChanges();
        //    Console.WriteLine(a1.Id);
        //    Console.WriteLine(q.Id + " | " + q.Content + " | " + q.RightAnswerId);
        //    foreach (var a in q.Answers)
        //    {
        //        Console.WriteLine(a1.Id);
        //        Console.WriteLine(a.Id + " - " + a.Content);
        //    }

        //    Console.WriteLine(db.Quizzes.GetById(3).Questions.Count);
        //    Console.WriteLine(db.Answers.All().Any(x => x.Content == "a1"));
        }
    }
}
