using AppName.ServiceB.Services.Interfaces;
using System.Text.RegularExpressions;

namespace AppName.ServiceB.Services.Implementations
{
    public class MessageValidatorService : IMessageValidatorService
    {
        public bool IsValidMessage(string message)
        {
            return !string.IsNullOrWhiteSpace(message) &&
                   (message.Split(", ").Length == 2);
        }
    }
}
