using NextUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotNext.ViewModels.Models
{
    public class ChatbotTypeItemVm : UIVm
    {
        private string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged(); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        private bool _isEnabled = true;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; }
        }
    }
}
