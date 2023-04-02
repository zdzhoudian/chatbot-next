using ChatbotNext.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotNext.Models
{
    public class ChatbotMessage : IDataCollection
    {
        public string ID { get; set; }

        public string Content { get; set; }

        public string ToUserID { get; set; }

        public string FromUserID { get; set; }

        public DateTime CreateTime { get; set; }

        public bool Success { get; set; } = false;
    }
}
