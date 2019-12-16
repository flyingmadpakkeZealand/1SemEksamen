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
        public MainPageVM()
        {
            _pressLoginCommand = new RelayCommand(Login);
            //User user1 = new User("Poul","Zealand123");
            //User user2 = new User("Charlotte","Zealand321");
            //User user3 = new Admin("flyingmadpakke","qwerty");
            //Dictionary<string,User> users = new Dictionary<string, User>();

            //users.Add(user1.UserName + user1.Password, user1);
            //users.Add(user2.UserName + user2.Password, user2);
            //users.Add(user3.UserName + user3.Password, user3);

            //PersistencyFacade.SaveObjectsAsync(users, ProgramSaveFiles.Users, SaveMode.Continuous);
        }

        private RelayCommand _pressLoginCommand;

        public ICommand PressLoginCommand
        {
            get { return _pressLoginCommand; }
        }

        public string TypedUserName { get; set; }
        public string TypedPassword { get; set; }
        public bool ProgressRingIsEnabled { get; set; }

        private async void Login()
        {
            ProgressRingIsEnabled = true;
            OnPropertyChanged(nameof(ProgressRingIsEnabled));
            object loadedUsers =
                await PersistencyFacade.LoadObjectsAsync(ProgramSaveFiles.Users,
                    typeof(List<Dictionary<string, User>>));
            List<Dictionary<string, User>> UsersContinuous = (List<Dictionary<string, User>>) loadedUsers;
            Dictionary<string, User> UsersInstance = UsersContinuous[0];
            await Task.Run(() => Thread.Sleep(2000));
            if (UsersInstance.ContainsKey(TypedUserName+TypedPassword))
            {
                MessageDialogHelper.Show("User found!","User Found");
            }
            else
            {
                MessageDialogHelper.Show("No match!","User Not Found");
            }

            ProgressRingIsEnabled = false;
            OnPropertyChanged(nameof(ProgressRingIsEnabled));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
