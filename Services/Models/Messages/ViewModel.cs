using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models.Messages
{
    public class ViewModel
    {
        public DateTime DateTimeSendedUTC { get; set; }
        public string Text { get; set; }
        public int Status { get; set; }
        public string SenderName { get; set; }
        public string SenderId { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverId { get; set; }
    }
}
