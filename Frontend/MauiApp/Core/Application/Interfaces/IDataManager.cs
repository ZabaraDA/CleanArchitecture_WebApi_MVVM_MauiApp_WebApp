using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.MauiApp.Core.Application.Interfaces
{
    public interface IDataManager
    {
        /// <summary>
        /// Запрос списка событий
        /// </summary>
        /// <typeparam name="T">Возвращаемый тип</typeparam>
        /// <param name="pointName">Название точки доступа</param>
        /// <param name="getParams">Набор параметров Get запроса</param>
        /// <returns>Набор значений событий API</returns>
        Task<T> GetItems<T>(string pointName, Dictionary<string, string> getParams = null);

        /// <summary>
        /// Аутентификация на сервисе
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns>Устанавливает токен идентификации, или бросает ошибку в случае отказа сервера</returns>
        void Authorization(string login, string password);
    }
}
