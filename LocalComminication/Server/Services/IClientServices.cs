using LocalComminication.Server.Entities;

namespace LocalComminication.Server.Services
{
    public interface IClientServices
    {
        public Task<ClientEntity> GetClient(ushort clientCode);
        public Task<bool> HasClient(ushort clientCode);
        public void AddClient(ClientEntity client);
        public void RemoveClient(ushort clientCode);
    }
}
