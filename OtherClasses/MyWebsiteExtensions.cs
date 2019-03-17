using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CoreWebsiteTest1.OtherClasses
{
    public static class MyWebsiteExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }

        public static bool IsPost(this HttpContext httpContext)
        {
            return httpContext.Request.Method == "POST";
        }

        public static bool IsPost(this ViewContext viewContext)
        {
            return viewContext.HttpContext.Request.Method == "POST";
        }

        public static bool IsPost(this PageModel pageModel)
        {
            return pageModel.HttpContext.Request.Method == "POST";
        }
    }
}
