using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MindMap.Scripts.UI {

    public class ColorScheme {

        public SolidColorBrush Regular;
        public SolidColorBrush Disabled;
        public SolidColorBrush Focused;
        public SolidColorBrush Clicked;

        public ColorScheme(Color regular, Color disabled, Color focused, Color clicked) {
            Regular = new SolidColorBrush(regular);
            Disabled = new SolidColorBrush(disabled);
            Focused = new SolidColorBrush(focused);
            Clicked = new SolidColorBrush(clicked);
        }

        public ColorScheme(byte regular, byte disabled, byte focused, byte clicked) {
            Regular = new SolidColorBrush(Color.FromRgb(regular, regular, regular));
            Disabled = new SolidColorBrush(Color.FromRgb(disabled, disabled, disabled));
            Focused = new SolidColorBrush(Color.FromRgb(focused, focused, focused));
            Clicked = new SolidColorBrush(Color.FromRgb(clicked, clicked, clicked));
        }

        public ColorScheme(byte rr, byte rg, byte dr, byte dg, byte db, byte rb, byte fr, byte fg, byte fb, byte cr, byte cg, byte cb) {
            Regular = new SolidColorBrush(Color.FromRgb(rr, rg, rb));
            Disabled = new SolidColorBrush(Color.FromRgb(dr, dg, db));
            Focused = new SolidColorBrush(Color.FromRgb(fr, fg, fb));
            Clicked = new SolidColorBrush(Color.FromRgb(cr, cg, cb));
        }

        public ColorScheme(ColorScheme scheme) {
            Regular = new SolidColorBrush(scheme.Regular.Color);
            Disabled = new SolidColorBrush(scheme.Disabled.Color);
            Focused = new SolidColorBrush(scheme.Focused.Color);
            Clicked = new SolidColorBrush(scheme.Clicked.Color);
        }

    }

    public class ButtonTemplate {

        public ColorScheme Background = new ColorScheme(255, 255, 230, 210);
        public ColorScheme Foreground = new ColorScheme(0, 100, 0, 0);
        public ColorScheme Stroke = new ColorScheme(100, 150, 0, 0);
        public double RadiusX = 3, RadiusY = 3;
        public double Width = 100;
        public Thickness Padding = new Thickness(10, 2, 10, 2);
        public Thickness Margin = new Thickness(1);
        public double StrokeThickness = 1;
        public double FontSize = 11;
        public HorizontalAlignment TextAligment = HorizontalAlignment.Center;

        public ButtonTemplate() { }

        public ButtonTemplate(ButtonTemplate template) {
            Background = new ColorScheme(template.Background);
            Foreground = new ColorScheme(template.Foreground);
            Stroke = new ColorScheme(template.Stroke);
            RadiusX = template.RadiusX;
            RadiusY = template.RadiusY;
            Padding = new Thickness(template.Padding.Left, template.Padding.Top, template.Padding.Right, template.Padding.Bottom);
            Margin = new Thickness(template.Margin.Left, template.Margin.Top, template.Margin.Right, template.Margin.Bottom);
            StrokeThickness = template.StrokeThickness;
            FontSize = template.FontSize;
            TextAligment = template.TextAligment;
            Width = template.Width;
        }

    }

}