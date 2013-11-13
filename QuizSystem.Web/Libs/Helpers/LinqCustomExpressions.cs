using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace QuizSystem.Web.Libs.Helpers
{
    public class LinqCustomExpressions
    {
        public static Expression<Func<T, string>> AsString<T>(T item)
        {
            Expression parameter = Expression.Constant(item);
            MethodInfo toString = ExtractMethod(typeof(T), "ToString", 0);
            return Expression.Lambda<Func<T, string>>(Expression.Call(toString, parameter));
        }


        private static MethodInfo ExtractMethod(Type objType, string methodName, int parametersCount)
        {
            return objType.GetMethods()
                .FirstOrDefault(x => x.Name == methodName &&
                x.GetParameters().Length == parametersCount);
        }
    }
}