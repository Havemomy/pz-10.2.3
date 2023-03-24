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
using System.IO;
using Microsoft.Win32;

namespace pz_10._2._3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            rtbEditor.Document.Blocks.Clear();
        }

        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontFamily.SelectedItem != null)
                rtbEditor.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, cmbFontFamily.SelectedItem);
        }

        private void cmbFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rtbEditor.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, cmbFontSize.SelectedItem);
        }

        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));
            temp = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cmbFontFamily.SelectedItem = temp;
            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontSizeProperty);
            cmbFontSize.Text = temp.ToString();
        }


        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Open);
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                range.Save(fileStream, DataFormats.Rtf);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Open_Executed(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Executed(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Brush b = btn_Black.Background;
            Color color = (b as SolidColorBrush).Color;
            rtbEditor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(color));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Brush b = btn_Green.Background;
            Color color = (b as SolidColorBrush).Color;
            rtbEditor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(color));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Brush b = btn_Yellow.Background;
            Color color = (b as SolidColorBrush).Color;
            rtbEditor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(color));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Brush b = btn_Red.Background;
            Color color = (b as SolidColorBrush).Color;
            rtbEditor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(color));
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }
        private void SetLineSpacing1(object sender, RoutedEventArgs e)
        {
            double height = 1;
            SetLineSpacing(height);
        }

        private void SetLineSpacing15(object sender, RoutedEventArgs e)
        {
            double height = 1.5;
            SetLineSpacing(height);
        }

        private void SetLineSpacing2(object sender, RoutedEventArgs e)
        {
            double height = 2;
            SetLineSpacing(height);
        }

        private void SetLineSpacing(double height)
        {
            var selected = rtbEditor.Selection;
            if (selected.IsEmpty)
            {
                return;
            }

            MessageBox.Show(selected.GetPropertyValue(TextElement.FontSizeProperty).ToString());

            height *= (double)selected.GetPropertyValue(TextElement.FontSizeProperty) * 1.3;

            selected.ApplyPropertyValue(Paragraph.LineHeightProperty, height);
        }
    }
}
