using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using MindMap.Scripts;
using MindMap.Scripts.Logics;

namespace MindMap.Pages {

    public partial class MainPage : Page {

        bool IsDiagram = false;

        public MainPage(int id) {
            InitializeComponent();
            Thread thread = new Thread(() => {
                CustomUILogic uiLogic = null;
                App.Current.Dispatcher.Invoke(() => {
                    uiLogic = new CustomUILogic(MyFrame);
                });
                Logic.UI = uiLogic;
                App.Current.Dispatcher.Invoke(() => {
                    Logic.ChangePage(new ViewNode(id));
                });
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e) {
        }

        private void NavigateMainMenuMI_Click(object sender, RoutedEventArgs e) {
            Logic.UI = null;
            Logic.ChangePage(new Welcome());
        }

        private void NavigateRootMI_Click(object sender, RoutedEventArgs e) {
            Logic.UI = null;
            Logic.ChangePage(new ChooseRoot());
        }

        private void DiagramViewMI_Click(object sender, RoutedEventArgs e) {
            var id = (int)App.Current.Properties["id"];
            if (!IsDiagram) {
                DiagramViewMI.Header = "Simple view";
                Logic.ChangePage(new DiagramView(id));
            } else {
                DiagramViewMI.Header = "Diagram view";
                Logic.ChangePage(new ViewNode(id));
            }
            IsDiagram = !IsDiagram;
        }
    }

    class CustomUILogic : IUILogic {

        Frame Frame;

        public CustomUILogic(Frame frame) {
            Frame = frame;
        }

        public void ChangePage(Page page) {
            Frame.Content = page;
        }

    }

}
