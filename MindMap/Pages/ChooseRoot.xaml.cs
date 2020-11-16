using MindMap.Scripts;
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
using MindMap.Scripts.Logics;

namespace MindMap.Pages {

    public partial class ChooseRoot : Page {

        #region Vars

        TimedEvent TimedEvent;
        List<int> ids = new List<int>();
        MyButton Selecionar, Deletar;

        string Nome {
            get => NomeTB.Text;
        }

        #endregion

        public ChooseRoot() {
            InitializeComponent();
            var w = App.Current.MainWindow;
            if (w.WindowState == WindowState.Normal) {
                w.Height = 300;
                w.Width = 350;
                WindowsSO.ToCenterOfScreen(w);
            }
            BackI.Source = ImageH.ToBitmapImage(Properties.Resources.arrow.ToBitmap());
            Selecionar = MyButton.Create(SelecionarG, "Selecionar");
            Selecionar.IsEnabled(false);
            Deletar = new MyButton(DeletarG, "Deletar");
            Deletar.TextBlock.FontSize = 11;
            Deletar.IsEnabled(false);
            ControlsH.CreateColumns(RootsDG, typeof(Table1));
            TimedEvent = new TimedEvent(TimeSpan.FromMilliseconds(500), 2, true, UpdateRoots);
            TimedEvent.TriggerNow();
        }

        public void UpdateRoots() {
            RootsDG.Items.Clear();
            ids.Clear();
            var r = Database.SelectRoots(Nome);
            for (int i = 0; i < r.Count; i++) {
                var item = r[i];
                RootsDG.Items.Add(new Table1() {
                    Id = item.ID,
                    Nome = item.Nome
                });
            }
        }

        private void RootsDG_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Selecionar.IsEnabled(RootsDG.SelectedIndex >= 0);
            Deletar.IsEnabled(RootsDG.SelectedIndex >= 0);
        }

        private void NomeTB_TextChanged(object sender, TextChangedEventArgs e) {
            TimedEvent.TryTigger();
        }

        private void SelecionarG_MouseDown(object sender, MouseButtonEventArgs e) {
            if (RootsDG.SelectedItem is Table1 t1) {
                var m = new MainPage(t1.Id);
                Logic.ChangePage(m);
            }
        }

        class Table1 {
            public int Id;
            public string Nome { get; set; }
        }

        private void BackI_MouseDown(object sender, MouseButtonEventArgs e) {
            Logic.ChangePage(new Welcome());
        }

        private void RootsDG_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            SelecionarG_MouseDown(null,null);
        }

        private void DeletarG_MouseDown(object sender, MouseButtonEventArgs e) {
            if (RootsDG.SelectedItem is Table1 t1) {
                Logic.AskDeleteCascade(t1.Id);
                UpdateRoots();
            }
        }
    }
}
