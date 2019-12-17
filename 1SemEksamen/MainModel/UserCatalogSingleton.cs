using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1SemEksamen.Common;
using _1SemEksamen.Exceptions;

namespace _1SemEksamen.MainModel
{
    public class UserCatalogSingleton
    {
        private static Type[] _userTypes = { typeof(Dictionary<string, User>), typeof(Dictionary<string, Admin>) };
        private static UserCatalogSingleton _userCatalogInstance = new UserCatalogSingleton();

        public static UserCatalogSingleton UserCatalogInstance
        {
            get { return _userCatalogInstance; }
        }


        public Dictionary<string,User> UserDictionary { get; set; }

        private UserCatalogSingleton()
        {
            UserDictionary = new Dictionary<string, User>();
        }

        public async Task LoadUsersToCatalogAsync()
        {
            object loadedUsers = null;
            try
            {
                loadedUsers = await PersistencyFacade.LoadCollectionWithPolymorphism(ProgramSaveFiles.Users,
                    typeof(Dictionary<string, User>), _userTypes);
            }
            catch (FileNotSavedException fnsx)
            {
                throw new FileNotSavedException("No users have been saved on this machine. See UserPageVM for instructions on how to get the default users",fnsx.ActualException);
            }

            UserDictionary = loadedUsers as Dictionary<string, User>;
        }
    }
}
