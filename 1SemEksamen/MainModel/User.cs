using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1SemEksamen.MainModel
{
    public class User
    {
		private bool _isAdmin; //A method override approach could replace this, but for now this is probably best. 

		public bool IsAdmin
		{
			get { return _isAdmin; }
        }


		private string _userName;

		public string UserName
		{
			get { return _userName; }
        }

		private string _password;

		public string Password
		{
			get { return _password; }
        }

        public User(string userName, string password)
        {
            _userName = userName;
            _password = password;
            _isAdmin = false;
        }

        protected User(bool isAdmin, string userName, string password)
        {
            _isAdmin = isAdmin;
            _userName = userName;
            _password = password;
        }
	}
}
