using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.MauiApp.Infrastructure.DataManager
{
    public class Token
    {
        public string Name { get; set; }
        public string BaseUrl { get; set; }
        public string GetUrlApiService(string controllerName)
        {
            //if (string.IsNullOrEmpty(Name))
            //{
            //    throw new ArgumentNullException("Token не получен");
            //}

            return $"{BaseUrl}/api/{controllerName}";
            return $"{BaseUrl}/api/{controllerName}?token={Name}";
        }
    }
}
