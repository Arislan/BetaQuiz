﻿using QuizSystem.Model;
using QuizSystem.Web.Areas.Administration.Models;
using QuizSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace QuizSystem.Web.Libs.Helpers
{
    public static class ModelConvertor
    {
        public static  Expression<Func<Quiz, QuizUpdateModel>> QuizToUpdateModel
        {
            get
            {
                return x =>
                    new QuizUpdateModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        CategoryId = x.CategoryId,
                        State = x.State
                    };
            }
        }

        public static Expression<Func<Quiz, QuizAuthorViewModel>> QuizToAuthorViewModel
        {
            get
            {
                return x =>
                    new QuizAuthorViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Category = x.Category.Name,
                        Questions = x.Questions.Count,
                        State = x.State,
                    };
            }
        }

        public static Expression<Func<Quiz, QuizAdminViewModel>> QuizToAdminViewModel
        {
            get
            {
                return x =>
                    new QuizAdminViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Category = x.Category.Name,
                        Questions = x.Questions.Count,
                        Creator = x.Creator.UserName,
                        State = x.State
                    };
            }
        }

        public static Expression<Func<Quiz, QuizViewModel>> QuizToViewModel
        {
            get
            {
                return x =>
                    new QuizViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Category = x.Category.Name,
                        Questions = x.Questions.Count,
                        Creator = x.Creator.UserName,
                        PublishDate = (DateTime)x.PublishDate,
                        Rating = x.Rating
                    };
            }
        }

        public static Expression<Func<Quiz, QuizPreviewModel>> QuizToPreviewModel
        {
            get
            {
                return x =>
                    new QuizPreviewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        State = x.State,
                        Questions = x.Questions.Select(q => 
                            new QuestionPreviewModel 
                            {  
                                Id = q.Id,
                                Content = q.Content,
                                RightAnswerId = q.RightAnswerId,
                                Answers = q.Answers.Select(a =>
                                new AnswerViewModel
                                {
                                    Id = a.Id,
                                    Content = a.Content
                                })
                            }).ToList()
                    };
            }
        }

        public static Expression<Func<Quiz, QuizSolveModel>> QuizToSolveModel
        {
            get
            {
                return x =>
                    new QuizSolveModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Questions = x.Questions.Select(q =>
                            new QuestionSolveModel
                            {
                                Id = q.Id,
                                Content = q.Content,
                                Answers = q.Answers.Select(a =>
                                new AnswerViewModel
                                {
                                    Id = a.Id,
                                    Content = a.Content
                                })
                            }).ToList()
                    };
            }
        }

        public static Expression<Func<Quiz, QuizSolvedModel>> QuizToSolvedModel
        {
            get
            {
                return x =>
                    new QuizSolvedModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Rating = x.Rating,
                        Questions = x.Questions.Select(q =>
                            new QuestionSolvedModel
                            {
                                Id = q.Id,
                                Content = q.Content,
                                RightAnswerId = q.RightAnswerId,
                                Answers = q.Answers.Select(a =>
                                new AnswerViewModel
                                {
                                    Id = a.Id,
                                    Content = a.Content
                                })
                            }).ToList()
                    };
            }
        }

        public static Expression<Func<QuizResult, QuizArchiveModel>> ResultToQuizArchiveModel
        {
            get
            {
                return x =>
                    new QuizArchiveModel
                    {
                        Id = x.Id,
                        Title = x.Quiz.Title,
                        Rating = x.Quiz.Rating,
                        Creator = x.Quiz.Creator.UserName,
                        Category = x.Quiz.Category.Name,
                        FirstResult = x.FirstResult,
                        LastResult = x.LastResult
                    };
            }
        }

        public static Expression<Func<Question, QuizEditQuestionViewModel>> QuestionToQuizEditViewModel
        {
            get
            {
                return x =>
                    new QuizEditQuestionViewModel
                    {
                        Id = x.Id,
                        Content = x.Content,
                        RightAnswerId = x.RightAnswerId,
                        Answers = x.Answers.Select(a => new QuizEditAnswerViewModel { Id = a.Id, Content = a.Content })
                    };
            }
        }

        public static Expression<Func<Answer, AnswerEditModel>> AnswerToEditModel
        {
            get
            {
                return x =>
                    new AnswerEditModel
                    {
                       Answer = x,
                       QuestionsCount = x.Questions.Count
                    };
            }
        }
    }
}