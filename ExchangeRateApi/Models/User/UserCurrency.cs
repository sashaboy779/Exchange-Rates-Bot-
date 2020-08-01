using System.Collections.Generic;

namespace ExchangeRateApi.Models.User
{
    public class UserCurrency
    {
        public int Id { get; set; } 
        public Currencies Currency { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}