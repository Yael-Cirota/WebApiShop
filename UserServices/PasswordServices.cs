using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Service
{
    public class PasswordServices : IPasswordServices
    {
        public Password GetStrength(Password password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password.PasswordValue);
            password.Strength = result.Score;
            return password;
        }
        public Password GetStrength(string password)
        {
            return GetStrength(new Password { PasswordValue = password });
        }
    }
}
