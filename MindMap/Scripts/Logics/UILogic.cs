using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MindMap.Scripts.Logics {

    class UILogic : IUILogic {

        public void ChangePage(Page page) {
            App.Current.MainWindow.Content = page;
        }

    }

}
