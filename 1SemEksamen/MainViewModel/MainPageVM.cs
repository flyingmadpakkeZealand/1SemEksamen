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
        public UserCatalogSingleton UserCatalogInstance { get; set; }
        public User LoginUser { get; set; }

        public MainPageVM()
        {
            MainPageVmInstance = this;
            UserCatalogInstance = UserCatalogSingleton.UserCatalogInstance;
            LoginUser = null;
            ////Uncomment this if you don't have the default users saved. Remember to delete all forms of prior Users save file. Run the program once, confirm you have a Users save file in appdata, then out comment this again.
            //User user1 = new User("User","User1");
            //User admin1 = new Admin("Admin","Admin1");
            //Dictionary<string,User> defaultUsers = new Dictionary<string, User>();
            //defaultUsers.Add(user1.UserName,user1);
            //defaultUsers.Add(admin1.UserName,admin1);
            //SaveDefaultUsers(defaultUsers);
            TypedUserName = "";
            TypedPassword = "";
        }

        private async void SaveDefaultUsers(Dictionary<string, User> defaultUsers)
        {
            await PersistencyFacade.SaveCollectionWithPolymorphism(defaultUsers, ProgramSaveFiles.Users);
        }

        public string TypedUserName { get; set; }
        public string TypedPassword { get; set; }
        public string SamePassword { get; set; }

        public async Task<User> Login()
        {
            string userName = TypedUserName;
            string password = TypedPassword;
            if (UserCatalogInstance.UserDictionary.Count==0)
            {
                await UserCatalogInstance.LoadUsersToCatalogAsync();
            }
            Dictionary<string, User> users = UserCatalogInstance.UserDictionary;
            if (users.ContainsKey(userName))
            {
                if (users[userName].Password==password)
                {
                    LoginUser = users[userName];
                    return users[userName];
                }
                MessageDialogHelper.Show("Password is case sensitive, please type the correct Password", "Incorrect Password");
            }
            else
            {
                MessageDialogHelper.Show("Could not find User. Remember your username is case sensitive","User not found");
            }

            return null;
        }

        public async Task<bool> SignUp()
        {
            string userName = TypedUserName;
            string password = TypedPassword;
            string samePassword = SamePassword;
            if (userName==""||password=="")
            {
                MessageDialogHelper.Show("Please type a valid Username and/or Password. An empty space is not valid","Invalid Format");
                return false;
            }
            if (UserCatalogInstance.UserDictionary.Count==0)
            {
                await UserCatalogInstance.LoadUsersToCatalogAsync();
            }

            Dictionary<string, User> users = UserCatalogInstance.UserDictionary;
            if (!users.ContainsKey(userName))
            {
                if (password==samePassword)
                {
                    users.Add(userName,new User(userName,password));
                    await PersistencyFacade.SaveCollectionWithPolymorphism(users, ProgramSaveFiles.Users);
                    LoginUser = users[userName];
                    MessageDialogHelper.Show("You are now signed up as: " + TypedUserName,"Thanks for signing up!");
                    return true;
                }
                else
                {
                    MessageDialogHelper.Show("Your passwords do not match, please make sure both passwords are equal.","Password mismatch");
                }
            }
            else
            {
                MessageDialogHelper.Show("Sorry, but this username is already taken :(","Username already taken");
            }

            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
