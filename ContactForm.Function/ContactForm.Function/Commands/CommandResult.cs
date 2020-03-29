using System.Collections.Generic;

namespace ContactForm.Function.Commands
{
    class CommandResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IList<string> Errors { get; set; }
    }
}
