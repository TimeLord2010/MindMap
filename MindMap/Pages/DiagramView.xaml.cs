using MindMap.Scripts;
using MindMap.Scripts.Logics;
using MindMap.Scripts.UI;
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

    public partial class DiagramView : Page {

        public DiagramView(int id) {
            InitializeComponent();
            Logic.BuildDiagramView(MyCanvas, id);
        }
    }
}
