using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using _1SemEksamen.Annotations;
using _1SemEksamen.Common;
using _1SemEksamen.MainModel;
using User = _1SemEksamen.MainModel.User;

namespace _1SemEksamen.MainViewModel
{
    public class MainPageVM:INotifyPropertyChanged
    {
        private static Type[] _userTypes = new Type[]{typeof(Dictionary<string,User>), typeof(Dictionary<string,Admin>)};
        public static MainPageVM MainPageVmInstance { get; set; }


        public MainPageVM()
        {
            MainPageVmInstance = this;
            ////Uncomment this if you don't have the default users saved. Remember to delete all forms of prior Users save file. 
            //User user1 = new User("User","User1");
            //User admin1 = new Admin("Admin","Admin1");
            //Dictionary<string,User> defaultUsers = new Dictionary<string, User>();
            //defaultUsers.Add(user1.UserName,user1);
            //defaultUsers.Add(admin1.UserName,admin1);
            //SaveDefaultUsers(defaultUsers);
            TypedUserName = "";
        }

        private async void SaveDefaultUsers(Dictionary<string, User> defaultUsers)
        {
            await PersistencyFacade.SaveCollectionWithPolymorphism(defaultUsers, ProgramSaveFiles.Users);
        }

        public string TypedUserName { get; set; }
        public string TypedPassword { get; set; }

        public async Task<User> Login()
        {

            object loadedUsers = await PersistencyFacade.LoadCollectionWithPolymorphism(ProgramSaveFiles.Users,
                typeof(Dictionary<string, User>), _userTypes);
            Dictionary<string, User> users = loadedUsers as Dictionary<string, User>;
            if (users.ContainsKey(TypedUserName))
            {
                if (users[TypedUserName].Password==TypedPassword)
                {
                    return users[TypedUserName];
                }
                MessageDialogHelper.Show("Password is case sensitive, please type the correct Password", "Incorrect Password");
            }
            else
            {
                MessageDialogHelper.Show("Could not find User. Remember your username is case sensitive","User not found");
            }

            return null;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
