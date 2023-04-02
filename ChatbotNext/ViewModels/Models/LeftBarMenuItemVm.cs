using NextUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ChatbotNext.ViewModels.Models
{
    public class AppMenuItemVm : SelectionItem<AppMenuItemCollection, AppMenuItemVm>
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private Geometry _icon;

        public Geometry Icon
        {
            get { return _icon; }
            set { _icon = value; OnPropertyChanged(); }
        }

        private string _pagePath;

        public string PagePath
        {
            get { return _pagePath; }
            set { _pagePath = value; OnPropertyChanged(); }
        }

    }

    public class AppMenuItemCollection : SelectionCollection<AppMenuItemCollection, AppMenuItemVm>
    {

    }
}
