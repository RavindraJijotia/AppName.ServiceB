using AppName.ServiceB.Messages;

namespace AppName.ServiceB.Models
{
    public class NameMessage : INameMessage
    {
        public string Message { get; set; }
    }
}
