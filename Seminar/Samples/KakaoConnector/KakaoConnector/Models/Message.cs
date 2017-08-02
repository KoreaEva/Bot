using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KakaoConnector.Models
{
    public class MessageResponse
    {
        public Message message { get; set; }
    }

    public class Message
    {
        public string text { get; set; }
    }
}