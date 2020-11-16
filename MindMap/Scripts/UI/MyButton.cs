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

    class MyButton {

        public ButtonTemplate ButtonTemplate = new ButtonTemplate();

        public Grid Grid;
        public Rectangle Rectangle;
        public TextBlock TextBlock;

        public MyButton(Grid grid, string text) {
            Grid = grid;
            Grid.Cursor = Cursors.Hand;
            Grid.MouseEnter += Grid_MouseEnter;
            Grid.MouseLeave += Grid_MouseLeave;
            Rectangle = new Rectangle() {
                Fill = ButtonTemplate.Background.Regular,
                Stroke = ButtonTemplate.Stroke.Regular,
                RadiusX = ButtonTemplate.RadiusX,
                RadiusY = ButtonTemplate.RadiusY,
                SnapsToDevicePixels = true
            };
            Grid.Children.Add(Rectangle);
            TextBlock = new TextBlock() {
                Text = text,
                FontSize = ButtonTemplate.FontSize,
                Foreground = ButtonTemplate.Foreground.Regular,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = ButtonTemplate.Padding
            };
            Grid.Children.Add(TextBlock);
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e) {
            if (Grid.IsEnabled) {
                Rectangle.Fill = ButtonTemplate.Background.Regular;
            }
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e) {
            if (Grid.IsEnabled) {
                Rectangle.Fill = ButtonTemplate.Background.Focused;
            }
        }

        public static MyButton Create(Grid grid, string text) {
            return new MyButton(grid, text);
        }

        public static MyButton Create(string text) {
            Grid g = new Grid() {
                Cursor = Cursors.Hand
            };
            MyButton myButton = new MyButton(g, text);
            return myButton;
        }

        public static MyButton Create (ButtonTemplate template, string text) {
            var b = Create(text);
            b.ButtonTemplate = new ButtonTemplate(template);
            b.ApplyStyle();
            return b;
        }

        public void ApplyStyle() {
            SolidColorBrush foreground = null;
            SolidColorBrush background = null;
            SolidColorBrush stroke = null;
            if (Grid.IsEnabled) {
                background = ButtonTemplate.Background.Regular;
                stroke = ButtonTemplate.Stroke.Regular;
                foreground = ButtonTemplate.Foreground.Regular;
            } else {
                background = ButtonTemplate.Background.Disabled;
                stroke = ButtonTemplate.Stroke.Disabled;
                foreground = ButtonTemplate.Foreground.Disabled;
            }
            Grid.Margin = ButtonTemplate.Margin;
            if (!double.IsNaN(ButtonTemplate.Width)) {
                Grid.Width = ButtonTemplate.Width;
            }
            Rectangle.Fill = background;
            Rectangle.Stroke = stroke;
            Rectangle.StrokeThickness = ButtonTemplate.StrokeThickness;
            Rectangle.RadiusX = ButtonTemplate.RadiusX;
            Rectangle.RadiusY = ButtonTemplate.RadiusY;
            TextBlock.Foreground = foreground;
            TextBlock.Margin = ButtonTemplate.Padding;
            TextBlock.FontSize = ButtonTemplate.FontSize;
            TextBlock.HorizontalAlignment = ButtonTemplate.TextAligment;
        }

        public void IsEnabled(bool a) {
            Grid.IsEnabled = a;
            Rectangle.Fill = a ? ButtonTemplate.Background.Regular : ButtonTemplate.Background.Disabled;
            Rectangle.Stroke = a ? ButtonTemplate.Stroke.Regular : ButtonTemplate.Stroke.Disabled;
            TextBlock.Foreground = a ? ButtonTemplate.Foreground.Regular : ButtonTemplate.Foreground.Disabled;
        }

    }
}
