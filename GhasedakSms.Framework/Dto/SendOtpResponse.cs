using GhasedakSms.Fremework.Enum;
using System;
using System.Collections.Generic;

namespace GhasedakSms.Fremework.Dto
{
    public class SendOtpResponse
    {
        public string LineNumber { get; set; }
        public string MessageBody { get; set; }
        public List<Data> Items { get; set; }
        public int TotalCost { get; set; }


        public class Data
        {
            public string Receptor { get; set; }
            public int Cost { get; set; }
            public string MessageId { get; set; }
            public string ClientReferenceId { get; set; }
            public DateTime SendDate { get; set; }
        }
    }

}
