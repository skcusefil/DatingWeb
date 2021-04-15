using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.SignalR
{
    public class PresenceTracker
    {
        private static readonly Dictionary<string, List<string>> OnLineUsers = new Dictionary<string, List<string>>();

        #region Methods for online status
        public Task UserConnected(string username, string connectionId)
        {
            lock(OnLineUsers)
            {
                if(OnLineUsers.ContainsKey(username))
                {
                    OnLineUsers[username].Add(connectionId);
                }
                else
                {
                    OnLineUsers.Add(username, new List<string>{connectionId});
                }
            }
            return Task.CompletedTask;
        }

        public Task UserDisconnected(string username, string connectionId)
        {
            lock(OnLineUsers)
            {
                if(!OnLineUsers.ContainsKey(username)) return Task.CompletedTask;

                OnLineUsers[username].Remove(connectionId);
                if(OnLineUsers[username].Count == 0)
                {
                    OnLineUsers.Remove(username);
                }
            }
            return Task.CompletedTask;
        }

        public Task<string[]> GetOnlineUser()
        {
            string[] onlineUsers;
            lock(OnLineUsers)
            {
                onlineUsers = OnLineUsers.OrderBy(k => k.Key).Select(k => k.Key).ToArray();
            }

            return Task.FromResult(onlineUsers);
        }
        #endregion

        #region Method for chatting
        public Task<List<string>> GetConnectionForUser(string usesrname)
        {
            List<string> connectionIds;
            lock(OnLineUsers)
            {
                connectionIds = OnLineUsers.GetValueOrDefault(usesrname);
            }

            return Task.FromResult(connectionIds);
        }
        #endregion
    }
}