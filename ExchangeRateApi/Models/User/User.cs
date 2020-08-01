using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExchangeRateApi.Models.User
{
    public class User
    {
        [Key]
        public int Id { get; set; }
 
        [Index(IsUnique = true)]
        public int UserTelegramId { get; set; }
        public string LanguageCode { get; set; }
        public virtual ICollection<UserCurrency> Currencies { get; set; }
        public string UserName { get; internal set; }

        public User()
        {
            Currencies = new List<UserCurrency>();
        }

        public User(int id, string languageCode)
        {
            UserTelegramId = id;
            LanguageCode = languageCode;
        }
    }
}