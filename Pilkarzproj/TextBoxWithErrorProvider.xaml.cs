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

namespace Pilkarzproj
{
    /// <summary>
    /// Logika interakcji dla klasy TextBoxWithErrorProvider.xaml
    /// </summary>
    public partial class TextBoxWithErrorProvider : UserControl
    {
        
        public string Text
        {
            get
            {
                return textBox.Text;
            }
            set
            {
                textBox.Text = value;
            }
        }
        public static Brush BrushForAll { get; set; } = Brushes.Red;

        public Brush TextBoxBorderBrush
        {
            get
            {
                return border.BorderBrush;
            }
            set
            {
                border.BorderBrush = value;
            }
        }
      
        public TextBoxWithErrorProvider()
        {
            InitializeComponent();
            border.BorderBrush = BrushForAll;
        }
        public void SetError(string errorText)
        {
            textBlockToolTip.Text = errorText;

            if (errorText == "")
            {
                
                border.BorderThickness = new Thickness(0);
                toolTip.Visibility = Visibility.Hidden;
            }
            else
            {
                
                border.BorderThickness = new Thickness(1);
                toolTip.Visibility = Visibility.Visible;
            }
        }

        public void SetFocus()
        {
            textBox.Focus();
        }
      
    }
}
