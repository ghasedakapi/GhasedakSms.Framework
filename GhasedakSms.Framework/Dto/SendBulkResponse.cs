using GhasedakSms.Fremework.Enum;
using System;
using System.Collections.Generic;

namespace GhasedakSms.Fremework.Dto
{
    public class SendBulkResponse
    {
        public int Cost { get; set; }
        public string LineNumber { get; set; }
        public List<ReceptionInfo> Receptors { get; set; }

        //are these outputs considered necessary?
        public string Message { get; set; }
        public DateTime SendDate { get; set; }

        public class ReceptionInfo
        {
            public string Receptor { get; set; }
            public string MessageId { get; set; }
        }
    }

}
