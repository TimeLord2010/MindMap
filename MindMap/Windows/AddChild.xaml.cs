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
using System.Windows.Shapes;
using MindMap.Scripts.Logics;

namespace MindMap.Windows {

    public partial class AddChild : Window {

        #region Vars

        int ParentID;
        bool Editing;

        string Nome {
            get => NomeTB.Text;
            set => NomeTB.Text = value;
        }

        #endregion

        public AddChild(int parent, bool editing = false) {
            InitializeComponent();
            ParentID = parent;
            Editing = editing;
            var adicionar = new MyButton(AdicionarB, editing ? "Renomear" : "Adicionar");
            if (editing) {
                Nome = Logic.DBLogic.GetNodeName(parent);
            }
        }

        private void AdicionarB_MouseDown(object sender, MouseButtonEventArgs e) {
            if (Logic.DBLogic.IsValidNodeName(Nome)) {
                if (Editing) {
                    Database.UpdateNode(ParentID, Nome, null);
                } else {
                    Logic.DBLogic.InsertChild(ParentID, Nome);
                }
                Close();
            } else {
                MessageBox.Show("Nome é inválido. Symbolos '[' e ']' não são válidos, o tamanho do nome deve ser maior que zero (espaços não contabilizados).");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            NomeTB.Focus();
        }

        private void NomeTB_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) {
                AdicionarB_MouseDown(null, null);
            }
        }
    }
}
