using LocalComminication.Server.Entities;

namespace LocalComminication.Server.Services
{
    public class ClientsServices : IClientServices
    {
        public ClientsServices()
        {

        }

        public void AddClient(ClientEntity client)
        {
            throw new NotImplementedException();
        }

        public Task<ClientEntity> GetClient(ushort clientCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasClient(ushort clientCode)
        {
            throw new NotImplementedException();
        }

        public void RemoveClient(ushort clientCode)
        {
            throw new NotImplementedException();
        }
    }
}
