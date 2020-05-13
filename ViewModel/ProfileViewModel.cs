using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace evolution.ViewModel
{
    public class ProfileViewModel : BaseViewModel
    {
        MainWindowViewModel mainWindowViewModel;
        public ProfileViewModel(MainWindowViewModel _mainWindowViewModel)
        {
            mainWindowViewModel = _mainWindowViewModel;
            LoadMatchesSource();
            if (mainWindowViewModel.CurrentUser==null)
            {
                SignInVisibility = Visibility.Visible;
                RegistrationVisibility = Visibility.Hidden;
                ProfileVisibility = Visibility.Hidden;
            }
            else
            {
                SignInVisibility = Visibility.Hidden;
                RegistrationVisibility = Visibility.Hidden;
                ProfileVisibility = Visibility.Visible;
            }
        }
        private IEnumerable<object> matchesSource;
        public IEnumerable<object> MatchesSource
        {
            get { return matchesSource; }
            set
            {
                if (matchesSource == value)
                    return;
                matchesSource = value;
                RaisePropertyChanged("MatchesSource");
            }
        }

        public RelayCommand BackToMenu
        {
            get
            {
                return new RelayCommand(obj => { mainWindowViewModel.ChangePage(mainWindowViewModel.MainMenuPage); });
            }
        }
        public RelayCommand ToLeaderBoard
        {
            get
            {
                return new RelayCommand(obj => { mainWindowViewModel.ChangePage(mainWindowViewModel.LeaderBoardPage); });
            }
        }
        private Visibility signInVisibility;
        public Visibility SignInVisibility
        {
            get { return signInVisibility; }
            set
            {
                if (signInVisibility == value)
                    return;

                signInVisibility = value;
                RaisePropertyChanged("SignInVisibility");
            }
        }
        private Visibility profileVisibility;
        public Visibility ProfileVisibility
        {
            get { return profileVisibility; }
            set
            {
                if (profileVisibility == value)
                    return;

                profileVisibility = value;
                RaisePropertyChanged("ProfileVisibility");
            }
        }
        private Visibility registrationVisibility;
        public Visibility RegistrationVisibility
        {
            get { return registrationVisibility; }
            set
            {
                if (registrationVisibility == value)
                    return;

                registrationVisibility = value;
                RaisePropertyChanged("RegistrationVisibility");
            }
        }

        private string profileRating;
        public string ProfileRating
        {
            get { return profileRating; }
            set
            {
                if (profileRating == value)
                    return;

                profileRating = value;
                RaisePropertyChanged("ProfileRating");
            }
        }
        private string profileNickname;
        public string ProfileNickname
        {
            get { return profileNickname; }
            set
            {
                if (profileNickname == value)
                    return;

                profileNickname = value;
                RaisePropertyChanged("ProfileNickname");
            }
        }
        private string profileRegDate;
        public string ProfileRegDate
        {
            get { return profileRegDate; }
            set
            {
                if (profileRegDate == value)
                    return;

                profileRegDate = value;
                RaisePropertyChanged("ProfileRegDate");
            }
        }
        private string signInLogin;
        public string SignInLogin
        {
            get { return signInLogin; }
            set
            {
                if (signInLogin == value)
                    return;

                signInLogin = value;
                RaisePropertyChanged("SignInLogin");
            }
        }
        private string signPassword;
        public string SignPassword
        {
            get { return signPassword; }
            set
            {
                if (signPassword == value)
                    return;

                signPassword = value;
                RaisePropertyChanged("SignPassword");
            }
        }
        private string registrationLogin;
        public string RegistrationLogin
        {
            get { return registrationLogin; }
            set
            {
                if (registrationLogin == value)
                    return;

                registrationLogin = value;
                RaisePropertyChanged("RegistrationLogin");
            }
        }
        private string registrationPassword1;
        public string RegistrationPassword1
        {
            get { return registrationPassword1; }
            set
            {
                if (registrationPassword1 == value)
                    return;

                registrationPassword1 = value;
                RaisePropertyChanged("RegistrationPassword1");
            }
        }
        private string registrationPassword2;
        public string RegistrationPassword2
        {
            get { return registrationPassword2; }
            set
            {
                if (registrationPassword2 == value)
                    return;

                registrationPassword2 = value;
                RaisePropertyChanged("RegistrationPassword2");
            }
        }
        private string registrationNickname;
        public string RegistrationNickname
        {
            get { return registrationNickname; }
            set
            {
                if (registrationNickname == value)
                    return;

                registrationNickname = value;
                RaisePropertyChanged("RegistrationNickname");
            }
        }
        private BitmapSource avatarImage;
        public BitmapSource AvatarImage
        {
            get { return avatarImage; }
            set
            {
                if (avatarImage == value)
                    return;
                avatarImage = value;
                RaisePropertyChanged("AvatarImage");
            }
        }
        public RelayCommand ToRegistration
        {
            get
            {
                return new RelayCommand(obj => {
                    SignInVisibility = Visibility.Visible;
                    RegistrationVisibility = Visibility.Visible;
                });
            }
        }
        public RelayCommand ExitFromProfile
        {
            get
            {
                return new RelayCommand(obj => {
                    mainWindowViewModel.CurrentUser = null;
                    ProfileVisibility = Visibility.Hidden;
                    SignInVisibility = Visibility.Visible;
                    RegistrationVisibility = Visibility.Hidden;
                });
            }
        }
        public RelayCommand ChangeAvatar
        {
            get
            {
                return new RelayCommand(obj => {
                    var context = new EvolutionDBContext();
                    User user = (from u in context.Users
                                 where u.ID == mainWindowViewModel.CurrentUser.ID
                                 select u).First();
                    ImageController.addFoto(user);
                    context.SaveChanges();
                    AvatarImage = ImageController.ConvertByteArrayToImage(user.Avatar);

                });
            }
        }
        public RelayCommand ToSignIn
        {
            get
            {
                return new RelayCommand(obj => {
                    SignInVisibility = Visibility.Visible;
                    RegistrationVisibility = Visibility.Hidden;
                });
            }
        }
        public RelayCommand SignIn
        {
            get
            {
                return new RelayCommand(obj => {
                    if (SignInMethod(SignInLogin, SignPassword))
                    {
                        LoadMatchesSource();
                        AvatarImage = ImageController.ConvertByteArrayToImage(mainWindowViewModel.CurrentUser.Avatar);
                        ProfileNickname = mainWindowViewModel.CurrentUser.NickName;
                        ProfileRegDate = mainWindowViewModel.CurrentUser.RegisterDate.Value.ToString("d");
                        ProfileRating = mainWindowViewModel.CurrentUser.Rating.Value.ToString();
                        SignInVisibility = Visibility.Hidden;
                        RegistrationVisibility = Visibility.Hidden;
                        ProfileVisibility = Visibility.Visible;
                    }
                });
            }
        }
        public RelayCommand Registration
        {
            get
            {
                return new RelayCommand(obj => {
                    if (RegistrationMethod())
                    {
                        SignInVisibility = Visibility.Hidden;
                        RegistrationVisibility = Visibility.Hidden;
                        ProfileVisibility = Visibility.Visible;
                    }
                });
            }
        }

        private bool RegistrationMethod()
        {
            try
            {
                if (Regex.IsMatch(RegistrationLogin, @"^[\w@.-]{6,50}$") && Regex.IsMatch(RegistrationPassword1, @"^[\w]{6,30}$") && RegistrationPassword1 == RegistrationPassword2 && Regex.IsMatch(RegistrationNickname, @"^[\w]{4,30}$"))
                {
                    var context = new EvolutionDBContext();
                    int n = (from u in context.Users
                            select u.ID).Max();
                    var query = from u in context.Users
                                where u.Login == RegistrationLogin
                                select u;
                    int n1 = query.Count();
                    query = from u in context.Users
                                where u.NickName == RegistrationNickname
                                select u;
                    int n2 = query.Count();

                    if (n1 == 0 && n2 == 0)
                    {
                        context.Users.Add(new User { ID = n + 1, NickName = RegistrationNickname, Login = RegistrationLogin, Password = RegistrationPassword1, RegisterDate = DateTime.Now, Rating = 1000 });
                        context.SaveChanges();
                        SignInMethod(RegistrationLogin, RegistrationPassword1);
                        return true;
                    }
                    else
                    {
                        if (App.Language.Name == "en-US")
                        {
                            MessageBox.Show("This login or nickname already taken");
                        }
                        else
                        {
                            MessageBox.Show("Этот логин или имя пользователя уже используется");
                        }

                    }
                    return false;
                }
            }
            catch
            {

            }
            return false;
        }

        public bool SignInMethod(string _login, string _password)
        {
            User user = new User();
            var context = new EvolutionDBContext();
            var query = from u in context.Users
                        where u.Login== _login && u.Password == _password
                        select u;
            try
            {
                user = query.ToList().First();
                mainWindowViewModel.CurrentUser = user;
                return true;
            }
            catch
            {
                if(App.Language.Name=="en-US")
                {
                    MessageBox.Show("Invalid login or password");
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль");
                }
            }

            return false;
        }
        public void LoadMatchesSource()
        {
            if (mainWindowViewModel.CurrentUser != null)
            {
                int UserID = mainWindowViewModel.CurrentUser.ID;
                var contex = new EvolutionDBContext();
                IQueryable<object> query = from umr in contex.UserMatchResults
                                           join m in contex.Matches on umr.MatchID equals m.MatchID
                                           where umr.UserID == UserID
                                           select new { MatchID = m.MatchID, Date = m.Date, Place = umr.Place, Score = umr.Score, MatchDuration = m.GameDuration };
                MatchesSource = query.ToList();
            }
        }
    }
}
