using System;
using System.Collections.Generic;

namespace GhasedakSms.Fremework.Dto
{
    public class SendBulkInput
    {
        public DateTime? SendDate { get; set; }
        public string LineNumber { get; set; }
        public List<string> Receptors { get; set; }
        public string Message { get; set; }
        public string ClientReferenceId { get; set; } = null;
        public bool IsVoice { get; set; } = false;
        public bool Udh { get; set; }
    }

}
