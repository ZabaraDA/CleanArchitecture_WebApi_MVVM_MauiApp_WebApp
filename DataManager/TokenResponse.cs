using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Frontend.MauiApp.Infrastructure.DataManager
{
    public class TokenResponse
    {
        /// <summary>
        /// Строка токена
        /// </summary>
        [JsonPropertyName("token")]
        public string Token { get; set; }

        /// <summary>
        /// Идентификатор ??
        /// </summary>
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        /// <summary>
        /// Строка для случая ошибочной аутентификации
        /// </summary>
        [JsonPropertyName("result")]
        public string Result { get; set; }
    }
}
