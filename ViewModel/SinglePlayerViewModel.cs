using evolution.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace evolution.ViewModel
{
    public class SinglePlayerViewModel : BaseViewModel
    {
        MainWindowViewModel mainWindowViewModel;
        List< Player> players = new List< Player>();
        public static string playerLabel = "Player";
        static BitmapImage ready = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "../../../Resources/Images/Ready.png", UriKind.RelativeOrAbsolute));
        static BitmapImage notReady = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "../../../Resources/Images/NotReady.png", UriKind.RelativeOrAbsolute));


        public SinglePlayerViewModel(MainWindowViewModel _mainWindowViewModel)
        {
            mainWindowViewModel = _mainWindowViewModel;
        }

        public User SignIn(string _login, string _password )
        {
            User user = new User();
            var context = new EvolutionDBContext();
            var query = from u in context.Users
                        where u.Login == _login && u.Password == _password
                        select u;
            try
            {
                user = query.ToList().First();
            }
            catch
            {
                if (App.Language.Name == "en-US")
                {
                    MessageBox.Show("Invalid login or password");
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль");
                }
                return null;
            }
            
            return user;
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
                return new RelayCommand(obj => {
                    if(players.Count >=2)
                    {
                        mainWindowViewModel.ChangePage(mainWindowViewModel.GamePage);
                        mainWindowViewModel.GameContext.Players = players;
                    }
                    else
                    {
                    }
                });
            }
        }

        public RelayCommand SignIn1                              //Войти
        {
            get
            {
                return new RelayCommand(obj => {
                    User user = SignIn(Login1, Password1);
                    if(user != null)
                    {
                        player1 = new Player(user, 0);
                        NickName1 = user.NickName;
                        Rating1 = (int)user.Rating;
                        AvatarImage1 = ImageController.ConvertByteArrayToImage(user.Avatar);
                    }
                });
            }
        }
        public RelayCommand SignIn2
        {
            get
            {
                return new RelayCommand(obj => {
                    User user = SignIn(Login2, Password2);
                    if (user != null)
                    {
                        player2 = new Player(user, 1);
                        NickName2 = user.NickName;
                        Rating2 = (int)user.Rating;
                        AvatarImage2 = ImageController.ConvertByteArrayToImage(user.Avatar);
                    }
                });
            }
        }
        public RelayCommand SignIn3
        {
            get
            {
                return new RelayCommand(obj => {
                    User user = SignIn(Login3, Password3);
                    if (user != null)
                    {
                        player3 = new Player(user, 2);
                        NickName3 = user.NickName;
                        Rating3 = (int)user.Rating;
                        AvatarImage3 = ImageController.ConvertByteArrayToImage(user.Avatar);
                    }
                });
            }
        }
        public RelayCommand SignIn4
        {
            get
            {
                return new RelayCommand(obj => {
                    User user = SignIn(Login4, Password4);
                    if (user != null)
                    {
                        player4 = new Player(user, 3);
                        NickName4 = user.NickName;
                        Rating4 = (int)user.Rating;
                        AvatarImage4 = ImageController.ConvertByteArrayToImage(user.Avatar);
                    }
                });
            }
        }
        public RelayCommand SignIn5
        {
            get
            {
                return new RelayCommand(obj => {
                    User user = SignIn(Login5, Password5);
                    if (user != null)
                    {
                        player5 = new Player(user, 4);
                        NickName5 = user.NickName;
                        Rating5 = (int)user.Rating;
                        AvatarImage5 = ImageController.ConvertByteArrayToImage(user.Avatar);
                    }
                });
            }
        }
        public RelayCommand SignIn6
        {
            get
            {
                return new RelayCommand(obj => {
                    User user = SignIn(Login6, Password6);
                    if (user != null)
                    {
                        player6 = new Player(user, 5);
                        NickName6 = user.NickName;
                        Rating6 = (int)user.Rating;
                        AvatarImage6 = ImageController.ConvertByteArrayToImage(user.Avatar);
                    }
                });
            }
        }

        public RelayCommand TakePart1
        {
            get
            {
                return new RelayCommand(obj => {
                    if(players.Contains(player1))
                    {
                        players.Remove(player1);
                        StatusImage1 = notReady;
                    }
                    else
                    {
                        StatusImage1 = ready;
                        players.Add(player1);
                    }
                });
            }
        }
        public RelayCommand TakePart2
        {
            get
            {
                return new RelayCommand(obj => {
                    if(players.Contains(player2))
                    {
                        players.Remove(player2);
                        StatusImage2 = notReady;
                    }
                    else
                    {
                        StatusImage2 = ready;
                        players.Add(player2);
                    }
                });
            }
        }
        public RelayCommand TakePart3
        {
            get
            {
                return new RelayCommand(obj => {
                    if(players.Contains(player3))
                    {
                        players.Remove(player3);
                        StatusImage3 = notReady;
                    }
                    else
                    {
                        StatusImage3 = ready;
                        players.Add(player3);
                    }
                });
            }
        }
        public RelayCommand TakePart4
        {
            get
            {
                return new RelayCommand(obj => {
                    if(players.Contains(player4))
                    {
                        players.Remove(player4);
                        StatusImage4 = notReady;
                    }
                    else
                    {
                        StatusImage4 = ready;
                        players.Add(player4);
                    }
                });
            }
        }
        public RelayCommand TakePart5
        {
            get
            {
                return new RelayCommand(obj => {
                    if(players.Contains(player5))
                    {
                        players.Remove(player5);
                        StatusImage5 = notReady;
                    }
                    else
                    {
                        StatusImage5 = ready;
                        players.Add(player5);
                    }
                });
            }
        }
        public RelayCommand TakePart6
        {
            get
            {
                return new RelayCommand(obj => {
                    if(players.Contains(player6))
                    {
                        players.Remove(player6);
                        StatusImage6 = notReady;
                    }
                    else
                    {
                        StatusImage6 = ready;
                        players.Add(player6);
                    }
                });
            }
        }

        private Player player1 = new Player(new User() { NickName = "Player 1" }, 0);
        private Player player2 = new Player(new User() { NickName = "Player 2" }, 1);
        private Player player3 = new Player(new User() { NickName = "Player 3" }, 2);
        private Player player4 = new Player(new User() { NickName = "Player 4" }, 3);
        private Player player5 = new Player(new User() { NickName = "Player 5" }, 4);
        private Player player6 = new Player(new User() { NickName = "Player 6" }, 5);

        private string nickName1 = playerLabel + " 1";                       //Ник
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
        private string nickName2 = playerLabel + " 2";
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
        private string nickName3 = playerLabel + " 3";
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
        private string nickName4 = playerLabel + " 4";
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
        private string nickName5 = playerLabel + " 5";
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
        private string nickName6 = playerLabel + " 5";
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




        private string password1;                    //Пароль
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

        private BitmapSource avatarImage1;                       //Аватар
        public BitmapSource AvatarImage1
        {
            get { return avatarImage1; }
            set
            {
                if (avatarImage1 == value)
                    return;
                avatarImage1 = value;
                RaisePropertyChanged("AvatarImage1");
            }
        }
        private BitmapSource avatarImage2;
        public BitmapSource AvatarImage2
        {
            get { return avatarImage2; }
            set
            {
                if (avatarImage2 == value)
                    return;
                avatarImage2 = value;
                RaisePropertyChanged("AvatarImage2");
            }
        }
        private BitmapSource avatarImage3;
        public BitmapSource AvatarImage3
        {
            get { return avatarImage3; }
            set
            {
                if (avatarImage3 == value)
                    return;
                avatarImage3 = value;
                RaisePropertyChanged("AvatarImage3");
            }
        }
        private BitmapSource avatarImage4;
        public BitmapSource AvatarImage4
        {
            get { return avatarImage4; }
            set
            {
                if (avatarImage4 == value)
                    return;
                avatarImage4 = value;
                RaisePropertyChanged("AvatarImage4");
            }
        }
        private BitmapSource avatarImage5;
        public BitmapSource AvatarImage5
        {
            get { return avatarImage5; }
            set
            {
                if (avatarImage5 == value)
                    return;
                avatarImage5 = value;
                RaisePropertyChanged("AvatarImage5");
            }
        }
        private BitmapSource avatarImage6;
        public BitmapSource AvatarImage6
        {
            get { return avatarImage6; }
            set
            {
                if (avatarImage6 == value)
                    return;
                avatarImage6 = value;
                RaisePropertyChanged("AvatarImage6");
            }
        }


        private BitmapSource statusImage1 = notReady;                       //Статус
        public BitmapSource StatusImage1
        {
            get { return statusImage1; }
            set
            {
                if (statusImage1 == value)
                    return;
                statusImage1 = value;
                RaisePropertyChanged("StatusImage1");
            }
        }
        private BitmapSource statusImage2 = notReady;
        public BitmapSource StatusImage2
        {
            get { return statusImage2; }
            set
            {
                if (statusImage2 == value)
                    return;
                statusImage2 = value;
                RaisePropertyChanged("StatusImage2");
            }
        }
        private BitmapSource statusImage3 = notReady;
        public BitmapSource StatusImage3
        {
            get { return statusImage3; }
            set
            {
                if (statusImage3 == value)
                    return;
                statusImage3 = value;
                RaisePropertyChanged("StatusImage3");
            }
        }
        private BitmapSource statusImage4 = notReady;
        public BitmapSource StatusImage4
        {
            get { return statusImage4; }
            set
            {
                if (statusImage4 == value)
                    return;
                statusImage4 = value;
                RaisePropertyChanged("StatusImage4");
            }
        }
        private BitmapSource statusImage5 = notReady;
        public BitmapSource StatusImage5
        {
            get { return statusImage5; }
            set
            {
                if (statusImage5 == value)
                    return;
                statusImage5 = value;
                RaisePropertyChanged("StatusImage5");
            }
        }
        private BitmapSource statusImage6 = notReady;
        public BitmapSource StatusImage6
        {
            get { return statusImage6; }
            set
            {
                if (statusImage6 == value)
                    return;
                statusImage6 = value;
                RaisePropertyChanged("StatusImage6");
            }
        }

    }
}
