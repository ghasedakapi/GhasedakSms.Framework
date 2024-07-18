using System;
using System.Collections.Generic;

namespace GhasedakSms.Fremework.Dto
{
    public class SendPairToPairInput
    {
        public List<SendPairToPairSmsWebServiceDto> Items { get; set; }
        public bool Udh { get; set; }
    }
    public class SendPairToPairSmsWebServiceDto
    {
        public string LineNumber { get; set; }
        public  string Receptor { get; set; }
        public  string Message { get; set; }
        public string ClientReferenceId { get; set; }
        public DateTime SendDate { get; set; } = DateTime.Now;
    }


}
