using ELTE.TravelAgency.Admin.Model;
using ELTE.TravelAgency.Admin.Persistence;
using System;
using System.Windows.Controls;

namespace ELTE.TravelAgency.Admin.ViewModel
{
    /// <summary>
    /// A bejelentkezés nézetmodell típusa.
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        private ITravelAgencyModel _model;

        /// <summary>
        /// Kilépés parancsának lekérdezése.
        /// </summary>
        public DelegateCommand ExitCommand { get; private set; }

        /// <summary>
        /// Bejelentkezés parancs lekérdezése.
        /// </summary>
        public DelegateCommand LoginCommand { get; private set; }

        /// <summary>
        /// Felhasználónév lekérdezése, vagy beállítása.
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// Alkalmazásból való kilépés eseménye.
        /// </summary>
        public event EventHandler ExitApplication;

        /// <summary>
        /// Sikeres bejelentkezés eseménye.
        /// </summary>
        public event EventHandler LoginSuccess;

        /// <summary>
        /// Sikertelen bejelentkezés eseménye.
        /// </summary>
        public event EventHandler LoginFailed;

        /// <summary>
        /// Nézetmodell példányosítása.
        /// </summary>
        /// <param name="model">A modell.</param>
        public LoginViewModel(ITravelAgencyModel model)
        { 
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _model = model;
            UserName = String.Empty;

            ExitCommand = new DelegateCommand(param => OnExitApplication());

            LoginCommand = new DelegateCommand(param => LoginAsync(param as PasswordBox));
        }

        /// <summary>
        /// Bejelentkezés
        /// </summary>
        /// <param name="passwordBox">Jelszótároló vezérlő.</param>
        private async void LoginAsync(PasswordBox passwordBox)
        {
            if (passwordBox == null)
                return;

            try
            {
                // a bejelentkezéshez szükségünk van a jelszótároló vezérlőre, mivel a jelszó tulajdonság nem köthető
                Boolean result = await _model.LoginAsync(UserName, passwordBox.Password);

                if (result)
                    OnLoginSuccess();
                else
                    OnLoginFailed();
            }
            catch (PersistenceUnavailableException)
            {
                OnMessageApplication("Nincs kapcsolat a kiszolgálóval.");
            }
        }

        /// <summary>
        /// Sikeres bejelentkezés eseménykiváltása.
        /// </summary>
        private void OnLoginSuccess()
        {
            if (LoginSuccess != null)
                LoginSuccess(this, EventArgs.Empty);
        }

        /// <summary>
        /// Alkalmazásból való kilépés eseménykiváltása.
        /// </summary>
        private void OnExitApplication()
        {
            if (ExitApplication != null)
                ExitApplication(this, EventArgs.Empty);
        }

        /// <summary>
        /// Sikertelen bejelentkezés eseménykiváltása.
        /// </summary>
        private void OnLoginFailed()
        {
            if (LoginFailed != null)
                LoginFailed(this, EventArgs.Empty);
        }

    }
}
