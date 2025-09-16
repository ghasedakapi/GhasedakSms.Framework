using GhasedakSms.Framework.Enum;
using System.Collections.Generic;

namespace GhasedakSms.Framework.Dto
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
