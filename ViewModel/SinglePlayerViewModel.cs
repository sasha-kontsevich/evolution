using evolution.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace evolution.ViewModel
{
    public class SinglePlayerViewModel : BaseViewModel
    {
        MainWindowViewModel mainWindowViewModel;
        Dictionary<int, Player> players = new Dictionary<int, Player>();
        public SinglePlayerViewModel(MainWindowViewModel _mainWindowViewModel)
        {
            mainWindowViewModel = _mainWindowViewModel;
            for (int i = 0; i < 6; i++)
            {
                players.Add(i, new Player());
            }
        }

        public void SignIn(string login, string password, int i)
        {
            MessageBox.Show(login + password + i.ToString());
        }

        public RelayCommand BackToMenu                               //Вернуться в главное меню
        {
            get
            {
                return new RelayCommand(obj => { mainWindowViewModel.ChangePage(mainWindowViewModel.MainMenuPage); });
            }
        }
        public RelayCommand StartGame                              //Начало игры
        {
            get
            {
                return new RelayCommand(obj => { mainWindowViewModel.ChangePage(mainWindowViewModel.SettingsPage); });
            }
        }

        public RelayCommand SignIn1                              //Войти
        {
            get
            {
                return new RelayCommand(obj => { SignIn(login1,password1,0); });
            }
        }
        public RelayCommand SignIn2
        {
            get
            {
                return new RelayCommand(obj => { SignIn(login2, password2, 1); });
            }
        }
        public RelayCommand SignIn3
        {
            get
            {
                return new RelayCommand(obj => { SignIn(login3, password3, 2); });
            }
        }
        public RelayCommand SignIn4
        {
            get
            {
                return new RelayCommand(obj => { SignIn(login4, password4, 3); });
            }
        }
        public RelayCommand SignIn5
        {
            get
            {
                return new RelayCommand(obj => { SignIn(login5, password5, 4); });
            }
        }
        public RelayCommand SignIn6
        {
            get
            {
                return new RelayCommand(obj => { SignIn(login6, password6, 5); });
            }
        }


        private string nickName1;                       //Ник
        public string NickName1
        {
            get { return nickName1; }
            set
            {
                if (nickName1 == value)
                    return;

                nickName1 = value;
                RaisePropertyChanged("NickName1");
            }
        }
        private string nickName2;
        public string NickName2
        {
            get { return nickName2; }
            set
            {
                if (nickName2 == value)
                    return;

                nickName2 = value;
                RaisePropertyChanged("NickName2");
            }
        }
        private string nickName3;
        public string NickName3
        {
            get { return nickName3; }
            set
            {
                if (nickName3 == value)
                    return;

                nickName3 = value;
                RaisePropertyChanged("NickName3");
            }
        }
        private string nickName4;
        public string NickName4
        {
            get { return nickName4; }
            set
            {
                if (nickName4 == value)
                    return;

                nickName4 = value;
                RaisePropertyChanged("NickName4");
            }
        }
        private string nickName5;
        public string NickName5
        {
            get { return nickName5; }
            set
            {
                if (nickName5 == value)
                    return;

                nickName5 = value;
                RaisePropertyChanged("NickName5");
            }
        }
        private string nickName6;
        public string NickName6
        {
            get { return nickName6; }
            set
            {
                if (nickName6 == value)
                    return;

                nickName6 = value;
                RaisePropertyChanged("NickName6");
            }
        }


        private int rating1;                       //Рейтинг
        public int Rating1
        {
            get { return rating1; }
            set
            {
                if (rating1 == value)
                    return;

                rating1 = value;
                RaisePropertyChanged("Rating1");
            }
        }
        private int rating2;
        public int Rating2
        {
            get { return rating2; }
            set
            {
                if (rating2 == value)
                    return;

                rating2 = value;
                RaisePropertyChanged("Rating2");
            }
        }
        private int rating3;
        public int Rating3
        {
            get { return rating3; }
            set
            {
                if (rating3 == value)
                    return;

                rating3 = value;
                RaisePropertyChanged("Rating3");
            }
        }
        private int rating4;
        public int Rating4
        {
            get { return rating4; }
            set
            {
                if (rating4 == value)
                    return;

                rating4 = value;
                RaisePropertyChanged("Rating4");
            }
        }
        private int rating5;
        public int Rating5
        {
            get { return rating5; }
            set
            {
                if (rating5 == value)
                    return;

                rating5 = value;
                RaisePropertyChanged("Rating5");
            }
        }
        private int rating6;
        public int Rating6
        {
            get { return rating6; }
            set
            {
                if (rating6 == value)
                    return;

                rating6 = value;
                RaisePropertyChanged("Rating6");
            }
        }


        private string login1;                       //Логин
        public string Login1
        {
            get { return login1; }
            set
            {
                if (login1 == value)
                    return;

                login1 = value;
                RaisePropertyChanged("Login1");
            }
        }
        private string login2;
        public string Login2
        {
            get { return login2; }
            set
            {
                if (login2 == value)
                    return;

                login2 = value;
                RaisePropertyChanged("Login2");
            }
        }
        private string login3;
        public string Login3
        {
            get { return login3; }
            set
            {
                if (login3 == value)
                    return;

                login3 = value;
                RaisePropertyChanged("Login3");
            }
        }
        private string login4;
        public string Login4
        {
            get { return login4; }
            set
            {
                if (login4 == value)
                    return;

                login4 = value;
                RaisePropertyChanged("Login4");
            }
        }
        private string login5;
        public string Login5
        {
            get { return login5; }
            set
            {
                if (login5 == value)
                    return;

                login5 = value;
                RaisePropertyChanged("Login5");
            }
        }
        private string login6;
        public string Login6
        {
            get { return login6; }
            set
            {
                if (login6 == value)
                    return;

                login6 = value;
                RaisePropertyChanged("Login6");
            }
        }


        private string password1;                       //Пароль
        public string Password1
        {
            get { return password1; }
            set
            {
                if (password1 == value)
                    return;

                password1 = value;
                RaisePropertyChanged("Password1");
            }
        }
        private string password2;
        public string Password2
        {
            get { return password2; }
            set
            {
                if (password2 == value)
                    return;

                password2 = value;
                RaisePropertyChanged("Password2");
            }
        }
        private string password3;
        public string Password3
        {
            get { return password3; }
            set
            {
                if (password3 == value)
                    return;

                password3 = value;
                RaisePropertyChanged("Password3");
            }
        }
        private string password4;
        public string Password4
        {
            get { return password4; }
            set
            {
                if (password4 == value)
                    return;

                password4 = value;
                RaisePropertyChanged("Password4");
            }
        }
        private string password5;
        public string Password5
        {
            get { return password5; }
            set
            {
                if (password5 == value)
                    return;

                password5 = value;
                RaisePropertyChanged("Password5");
            }
        }
        private string password6;
        public string Password6
        {
            get { return password6; }
            set
            {
                if (password6 == value)
                    return;

                password6 = value;
                RaisePropertyChanged("Password6");
            }
        }

    }
}
