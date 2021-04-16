using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IMessageRepository
    {
        #region  methods for chat and connection with signalr
        void AddGroup(Group group);
        Task<Group> GetGroupForConnection(string connectionId);
        Task<Connection> GetConnection(string connectionId);
        Task<Group> GetmessageGroup(string groupName);
        void RemoveConnection(Connection connection);
        #endregion
        void AddMessage(Message message);
        void DeleteMessage(Message message);
        Task<Message> GetMessage(int id);
        Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams);
        Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername);
        Task<bool> SaveAllAnsync();
    }
}