using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Interfaces
{
   public interface IAccountService
    {
        public string HashPassword(string password);
    }
}
