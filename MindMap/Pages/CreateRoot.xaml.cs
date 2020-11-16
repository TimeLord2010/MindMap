using MindMap.Scripts;
using MySql.Data.MySqlClient;
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
using MindMap.Scripts.Logics;

namespace MindMap.Pages {

    public partial class CreateRoot : Page {

        #region Vars

        TimedEvent TimedEvent;

        string Root {
            get => NomeTB.Text;
        }

        bool Valido {
            set {
                InvalidoTBL.Visibility = value ? Visibility.Hidden : Visibility.Visible;
            }
        }

        #endregion

        public CreateRoot() {
            InitializeComponent();
            BackI.Source = ImageH.ToBitmapImage(Properties.Resources.arrow.ToBitmap());
            TimedEvent = new TimedEvent(TimeSpan.FromMilliseconds(500), 2, true, Validate);
        }

        private void Validate() {
            Valido = Logic.DBLogic.IsRootNameValid(Root);
        }

        private void BackI_MouseDown(object sender, MouseButtonEventArgs e) {
            Logic.ChangePage(new Welcome());
        }

        private void OkG_MouseDown(object sender, MouseButtonEventArgs e) {
            Logic.InsertRootAndMainPage(Root);
        }

        private void NomeTB_TextChanged(object sender, TextChangedEventArgs e) {
            TimedEvent.TryTigger();
        }
    }
}
