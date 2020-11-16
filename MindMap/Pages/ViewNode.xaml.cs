using MindMap.Scripts;
using MindMap.Scripts.UI;
using MindMap.Windows;
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

    public partial class ViewNode : Page {

        #region Vars

        List<int> ids = new List<int>();

        int parentID;
        int ParentID {
            get => parentID;
            set {
                ParentName = Logic.DBLogic.GetNodeName(parentID = value);
                if (value < 0) {
                    ParentNameTBL.Cursor = Cursors.Arrow;
                    ParentNameTBL.MouseDown -= ParentNameTBL_MouseDown;
                }
            }
        }
        string ParentName {
            get => ParentNameTBL.Text;
            set => ParentNameTBL.Text = value;
        }

        int currentID;
        int CurrentID {
            get => currentID;
            set {
                CurrentName = Logic.DBLogic.GetNodeName(currentID = value);
                var founds = Database.SelectNodes(value);
                if (founds.Count == 1) {
                    Conteudo = founds[0].Content;
                }
                var r = Database.SelectChildren(null, value);
                if (r.Count == 1) {
                    ParentID = r[0].Parent.ID;
                } else {
                    ParentID = -1;
                }
            }
        }

        string CurrentName {
            get => CurrentNameTBL.Text;
            set => CurrentNameTBL.Text = value;
        }

        string Conteudo {
            get => ContentTBL.Text;
            set => ContentTBL.Text = value;
        }

        #endregion

        public ViewNode(int id, bool normalize = true) {
            InitializeComponent();
            App.Current.Properties["id"] = id;
            var w = App.Current.MainWindow;
            w.WindowStyle = WindowStyle.ThreeDBorderWindow;
            if (w.WindowState == WindowState.Normal && normalize) {
                w.Width = 600;
                w.Height = 400;
                WindowsSO.ToCenterOfScreen(w);
            }
            MyButton salvar = new MyButton(UndoAllG, "Undo all");
            salvar.ButtonTemplate = ApplicationButtonStyle.Generic;
            salvar.ApplyStyle();
            var criar = new MyButton(CriarG, "Criar");
            criar.ButtonTemplate = ApplicationButtonStyle.Generic;
            criar.ApplyStyle();
            Logic.FillChildren(ChildrenSP, ids, CurrentID = id);
            TBL_ToolTip(CurrentNameTBL);
            TBL_ToolTip(ParentNameTBL);
        }

        private void TBL_ToolTip(TextBlock tbl) {
            var tt = new ToolTip {
                Content = tbl.Text
            };
            tbl.ToolTip = tt;
            tbl.MouseEnter += (s, e) => {
                tt.IsOpen = true;
            };
            tbl.MouseLeave += (s, e) => {
                tt.IsOpen = false;
            };
        }

        private void UndoAllG_MouseDown(object sender, MouseButtonEventArgs e) {
            Conteudo = Logic.DBLogic.GetNodeContent(CurrentID);
        }

        private void CriarG_MouseDown(object sender, MouseButtonEventArgs e) {
            var a = new AddChild(CurrentID);
            a.ShowDialog();
            Logic.FillChildren(ChildrenSP, ids, CurrentID);
        }

        private void ParentNameTBL_MouseDown(object sender, MouseButtonEventArgs e) {
            Logic.ChangePage(new ViewNode(ParentID, false));
        }

        private void ContentTBL_LostFocus(object sender, RoutedEventArgs e) {
            Database.UpdateNode(CurrentID, null, Conteudo);
        }
    }
}
