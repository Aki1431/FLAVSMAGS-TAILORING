using System;
using System.Collections.Generic;
using System.Text;

namespace FLAVSMAGS_TAILORING.Models
{
    /// <summary>
    /// Singleton pattern for user session management
    /// </summary>
    public sealed class UserSession
    {
        private static UserSession? _instance;
        private static readonly object _lock = new object();

        public string Username { get; private set; } = string.Empty;
        public DateTime LoginTime { get; private set; }
        public bool IsAuthenticated { get; private set; }

        private UserSession() { }

        public static UserSession Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new UserSession();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Log in user and start session
        /// </summary>
        public void Login(string username)
        {
            Username = username;
            LoginTime = DateTime.Now;
            IsAuthenticated = true;
        }

        /// <summary>
        /// Log out and clear session
        /// </summary>
        public void Logout()
        {
            Username = string.Empty;
            IsAuthenticated = false;
        }
    }
}
