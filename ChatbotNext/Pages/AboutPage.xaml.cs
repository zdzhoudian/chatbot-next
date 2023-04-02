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
    /// AboutPage.xaml 的交互逻辑
    /// </summary>
    public partial class AboutPage : Page
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.disclaimerTextBox.Text = @"
在使用本软件之前，请详细阅读本声明。任何人使用本软件即视为已经接受本声明。

1. 本软件是基于MIT协议开源软件方案设计的软件，您可以自由地使用、修改、复制、发布、出售或者分发本软件或者本软件的任何组件。

2. 本软件是按“原样”提供的，不做任何明示或暗示的保证，包括但不限于对软件品质、适用性、特定用途的适用性、无病毒性、无误差和无中断的担保等。任何风险均由用户自行承担，在使用本软件时，用户需自行采取安全和保护措施。

3. 不论在任何情况下，本软件的开发者均无需对使用本软件，或与本软件相关的行为、事件或者事物承担任何责任，在任何情况下包括但不限于在任何情况下对因使用本软件或无法使用本软件而导致的任何特殊、直接或间接损害（包括但不限于数据丢失或数据损害）

4. 本软件仅供个人或商业用途，不得用于非法活动，在任何情况下，本软件的开发者将不承担因为用户所造成的任何作用或行为的任何责任，包括但不限于任何直接、间接、意外或特殊的损害赔偿。

5. 本软件使用过程中产生的任何纠纷、权益争议和法律责任都应由用户自行承担，软件的开发者不承担任何责任。

总之，您在使用本软件之前应该审慎考虑并且明确风险，同时也应该自行采取安全和保护措施以减少因使用本软件所造成的损失。如果您对该免责声明有任何疑问，请勿使用本软件，并与我们的开发团队联系以获得更多信息。
";
        }
    }
}
