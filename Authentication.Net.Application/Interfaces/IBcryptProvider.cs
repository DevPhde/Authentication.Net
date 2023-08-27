using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Net.Application.Interfaces
{
    public interface IBcryptProvider
    {
        string HashPassword(string password);
        bool IsIdenticalPassword(string password, string hashedPassword);
    }
}
