using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotNext.Models
{
    public class ChatbotData : UserInfoData
    {
        public string ChatbotType { get; set; }

        public virtual string Settings { get; set; }
    }
}
