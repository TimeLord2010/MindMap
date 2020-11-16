using MindMap.Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MindMap.Scripts.UI {

    class ChildButton {

        public Grid Grid;
        public Rectangle Rectangle;
        public TextBlock TextBlock;
        public ButtonTemplate ButtonTemplate = new ButtonTemplate();

        public ChildButton (string name) {
            ButtonTemplate.Background.Regular = new SolidColorBrush(Color.FromRgb(217, 255, 220));
            ButtonTemplate.Background.Focused = new SolidColorBrush(Color.FromRgb(153, 255, 160));
            ButtonTemplate.Background.Clicked = new SolidColorBrush(Color.FromRgb(117, 199, 124));
            ButtonTemplate.Margin = new Thickness(2);
            Grid = new Grid() {
                Cursor = Cursors.Hand
            };
            var tt = new ToolTip();
            tt.Content = name;
            Grid.ToolTip = tt;
            Grid.MouseEnter += Grid_MouseEnter;
            Grid.MouseLeave += Grid_MouseLeave;
            Grid.MouseLeftButtonDown += Grid_MouseLeftButtonDown;
            Grid.MouseUp += Grid_MouseUp;
            Rectangle = new Rectangle() {
                SnapsToDevicePixels = true
            };
            Grid.Children.Add(Rectangle);
            TextBlock = new TextBlock() {
                Text = name,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.Children.Add(TextBlock);
            ApplyTemplate();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            Rectangle.Fill = ButtonTemplate.Background.Clicked;
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e) {
            Rectangle.Fill = ButtonTemplate.Background.Focused;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e) {
            Rectangle.Fill = ButtonTemplate.Background.Regular;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e) {
            Rectangle.Fill = ButtonTemplate.Background.Focused;
        }

        public void ApplyTemplate () {
            Grid.Margin = ButtonTemplate.Margin;
            Rectangle.RadiusX = ButtonTemplate.RadiusX;
            Rectangle.RadiusY = ButtonTemplate.RadiusY;
            Rectangle.Fill = ButtonTemplate.Background.Regular;
            Rectangle.Stroke = ButtonTemplate.Stroke.Regular;
            TextBlock.Foreground = ButtonTemplate.Foreground.Regular;
            TextBlock.Margin = ButtonTemplate.Padding;
        }

    }
}
