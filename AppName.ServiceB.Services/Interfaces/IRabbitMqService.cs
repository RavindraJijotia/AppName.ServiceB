namespace AppName.ServiceB.Services.Interfaces
{
    public interface IRabbitMqService
    {
        void CreateConnection();
        void StartMessageListener();
        void CloseConnection();
    }
}
