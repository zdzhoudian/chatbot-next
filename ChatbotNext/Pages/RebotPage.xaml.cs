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

namespace ChatbotNext.Pages
{
    /// <summary>
    /// RebotPage.xaml 的交互逻辑
    /// </summary>
    public partial class RebotPage : Page
    {
        public RebotPage()
        {
            InitializeComponent();
        }

        private void sendTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var index = this.sendTextBox.SelectionStart;
            if (e.Key == Key.Enter &&
                (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt)))
            {
                this.sendTextBox.Text = this.sendTextBox.Text.Insert(index, Environment.NewLine);
                this.sendTextBox.SelectionStart = index + Environment.NewLine.Length;
            }
        }
    }
}
