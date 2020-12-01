using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADBackend.DAL.Interfaces
{
    public interface IUserRepo
    {
        bool IsUserAdmin(string token);
    }
}
