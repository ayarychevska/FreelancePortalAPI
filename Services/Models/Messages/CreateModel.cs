using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models.Messages
{
    public class CreateModel
    {
        public DateTime DateTimeSendedUTC { get; set; }
        public string Text { get; set; }
        public int Status { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }
}
