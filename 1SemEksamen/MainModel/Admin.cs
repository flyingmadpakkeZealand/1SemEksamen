using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1SemEksamen.MainModel
{
    //Admin inherits from user just in case Admin gets more specialized.
    public class Admin:User
    {
        public Admin(string userName, string password) : base(true, userName, password)
        {
        }
    }
}
