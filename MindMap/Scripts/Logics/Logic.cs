using MindMap.Pages;
using MindMap.Scripts.UI;
using MindMap.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MindMap.Scripts.Logics {

    class Logic {

        static DBLogic dBLogic;
        public static IDBLogic DBLogic {
            get {
                if (dBLogic == null) dBLogic = new DBLogic();
                return dBLogic;
            }
        }

        static IUILogic ui;
        public static IUILogic UI {
            get {
                if (ui == null) ui = new UILogic();
                return ui;
            }
            set {
                ui = value;
            }
        }

        public static void ChangePage(Page page) {
            UI.ChangePage(page);
        }

        public static void EnsureDatabase() {
            if (!DBLogic.EnsureDatabase()) {
                var message = "There was an error while getting database instance.";
                MessageBox.Show(message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(Environment.ExitCode);
            }
        }

        public static void InitializeStyles() {
            ApplicationButtonStyle.Generic.Background.Regular = new SolidColorBrush(Color.FromRgb(173, 216, 230));
            ApplicationButtonStyle.Generic.Background.Focused = new SolidColorBrush(Color.FromRgb(136, 196, 219));
            ApplicationButtonStyle.Generic.FontSize = 14;
        }

        public static void InsertRootAndMainPage(string root) {
            if (DBLogic.InsertRoot(root)) {
                var list = Database.SelectRoots(root);
                ChangePage(new MainPage(list.Last().ID));
            }
        }

        public static void FillChildren(StackPanel children, List<int> ids, int id) {
            children.Children.Clear();
            ids.Clear();
            var r = Database.SelectChildren(id, null).Select(x => x.Child);
            foreach (var item in r) {
                ChildButton childButton = new ChildButton(item.Nome);
                childButton.TextBlock.HorizontalAlignment = HorizontalAlignment.Left;
                var cm = new ContextMenu();
                MenuItem editar = new MenuItem() {
                    Header = "Renomear"
                };
                cm.Items.Add(editar);
                editar.Click += new RoutedEventHandler((s, e) => {
                    var a = new AddChild(item.ID, true);
                    a.ShowDialog();
                    FillChildren(children, ids, id);
                });
                var deletar = new MenuItem() {
                    Header = "Deletar"
                };
                cm.Items.Add(deletar);
                deletar.Click += new RoutedEventHandler((s, e) => {
                    AskDeleteCascade(item.ID);
                    FillChildren(children, ids, id);
                });
                childButton.Grid.ContextMenu = cm;
                childButton.Grid.MouseLeftButtonDown += new MouseButtonEventHandler((s, e) => {
                    ChangePage(new ViewNode(item.ID, false));
                });
                children.Children.Add(childButton.Grid);
            }
        }

        public static void BuildDiagramView(Canvas canvas, int id) {
            Tree root = GetTree(id);
            var template = new ButtonTemplate {
                TextAligment = HorizontalAlignment.Left,
                Width = 150
            };
            var depth = root.GetDepth();
            for (int i = depth; i >= 0; i--) {
                var row = root.GetRowAt(i);

            }
        }

        private static Tree GetTree (int id) {
            var parents = Database.SelectChildren(null, id);
            if (parents.Count > 0) {
                var parent = parents[0].Parent;
                return Tree.Build(parent.ID);
            } else {
                return Tree.Build(id);
            }
        }

        private static void LinkNodes(Canvas canvas, MyButton begin, MyButton end) {
            CanvasH.GetLT(begin.Grid, out double gLeft, out double gTop);
            CanvasH.GetLT(end.Grid, out double g2Left, out double g2Top);
            var h = begin.ButtonTemplate.Padding.Left + begin.ButtonTemplate.Padding.Right;
            Point
                pbegin = new Point(gLeft + begin.Grid.ActualWidth, gTop + (begin.Grid.ActualHeight / 2)),
                pend = new Point(g2Left, g2Top + (end.Grid.ActualHeight / 2));
            LinkPoints(canvas, pbegin, pend);
        }

        private static void LinkPoints(Canvas canvas, Point begin, Point end) {
            var points = new List<Point>();
            points.Add(new Point(begin.X, end.Y));
            points.Add(new Point(end.X, begin.Y));
            points.Add(end);
            BezierSegment(canvas, begin, points);
        }

        private static void BezierSegment(Canvas canvas, Point begin, List<Point> points) {
            var path = new Path();
            path.Stroke = Brushes.Black;
            path.StrokeThickness = 1;
            var pathGeometry = new PathGeometry();
            var pathFigureCollection = new PathFigureCollection();
            var segments = new List<PathSegment>();
            var poly = new PolyBezierSegment(points, true);
            segments.Add(poly);
            var pathFigure = new PathFigure(begin, segments, false);
            pathFigureCollection.Add(pathFigure);
            pathGeometry.Figures = pathFigureCollection;
            path.Data = pathGeometry;
            canvas.Children.Add(path);
        }

        public static void AskDeleteCascade(int id) {
            var rs = MessageBox.Show("Do you wish to delete it's children as well?", "Cascade delete?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (rs == MessageBoxResult.Cancel) return;
            if (rs == MessageBoxResult.Yes) {
                DBLogic.DeleteParentCascade(id);
            } else {
                Database.DeleteNode(id);
            }
        }

    }
}
