using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.MauiApp.Infrastructure.Utils
{
    /// <summary>
    /// Простейшие расширения
    /// </summary>
    public static class Extends
    {
        /// <summary>
        /// Формирует строку запроса для HTTP Get из словаря параметров с предшествующим &
        /// </summary>
        /// <remarks>
        ///     Словарь {
        ///         {"page",1},
        ///         {"data","some_date"}
        ///     }
        ///     Результат:
        ///          &page=1&data=some_date
        ///     Если словарь пуст, то просто пустая строка
        /// </remarks>
        /// <param name="parameters">Параметры</param>
        /// <returns>Строка для запроса</returns>
        public static string GetParameters(this Dictionary<string, string> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return "";
            }
            var paramString = "&" + string.Join("&", parameters.Select(x => $"{x.Key}={x.Value}"));
            return paramString;
        }
    }
}
