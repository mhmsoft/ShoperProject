using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Data.Validator
{
    public class ErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError InvalidUserName(string userName) => new() { Code = "InvalidUserName", Description = $"\"{userName}\" kullanıcı adı geçersiz." };

        public override IdentityError DuplicateEmail(string email) => new() { Code = "DuplicateEmail", Description = $"\"{email}\" adresi kullanımdadır." };

        public override IdentityError PasswordTooShort(int length) => new() { Code = "PasswordTooShort", Description = $"Şifre en az {length} karakter olmalıdır." };
    }
}
