using ChatbotNext.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotNext.Models
{
    public class UserInfoData : IDataCollection
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }
    }
}
