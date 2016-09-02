using System;
using System.Collections.Generic;

namespace DenodoAdapter
{
    public class DenodoError
    {
        public List<DenodoMessage> errors { get; set; }

        public string ReadAll()
        {
            string messages = string.Empty;
            errors.ForEach(m => messages += m.message + Environment.NewLine);
            return messages;
        }
    }
}
