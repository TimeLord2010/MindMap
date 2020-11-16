using MindMap.Properties;
using MindMap.Scripts;
using MindMap.Scripts.Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MindMap.Pages {

    public partial class Welcome : Page {

        //public Logger Logger = new Logger("log.log");

        Settings Settings {
            get => Properties.Settings.Default;
        }

        public Welcome() {
            //Logger.Info("Welcome page instanciated.");
            InitializeComponent();
            var w = App.Current.MainWindow;
            if (w.WindowState == WindowState.Normal) {
                w.Width = 200;
                w.Height = 300;
                WindowsSO.ToCenterOfScreen(w);
            }
            UserTB.Text = Settings.User;
            PasswordTB.Text = Settings.Password;
            ServerTB.Text = Settings.Server;
        }

        private void CriarG_MouseDown(object sender, MouseButtonEventArgs e) {
            try {
                Ensure();
                Logic.ChangePage(new CreateRoot());
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void AbrirG_MouseDown(object sender, MouseButtonEventArgs e) {
            try {
                Ensure();
                Logic.ChangePage(new ChooseRoot());
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        void Ensure() {
            Settings.User = UserTB.Text;
            Settings.Password = PasswordTB.Text;
            Settings.Server = ServerTB.Text;
            Settings.Save();
            Database.Server = ServerTB.Text;
            Database.User = UserTB.Text;
            Database.Password = PasswordTB.Text;
            Database.sql = null;
            Database.EnsureDatabase();
            Database.SQL.QueryR("SHOW DATABASES;", (r) => { });
        }

    }
}
