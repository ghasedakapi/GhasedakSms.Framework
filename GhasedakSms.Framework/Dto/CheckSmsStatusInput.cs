using GhasedakSms.Fremework.Enum;
using System.Collections.Generic;

namespace GhasedakSms.Fremework.Dto
{
    public class CheckSmsStatusInput
    {
        public List<string> Ids { get; set; }

        /// <summary>
        /// Gets or sets the type of ID used in the query.
        /// </summary>
        public MessageIdType Type { get; set; }
    }

}
