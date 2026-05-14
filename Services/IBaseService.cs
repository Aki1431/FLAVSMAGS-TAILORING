using System;
using System.Collections.Generic;
using System.Text;

namespace FLAVSMAGS_TAILORING.Services
{
    /// <summary>
    /// Base interface for all services - ensures consistent event handling
    /// </summary>
    public interface IBaseService
    {
        /// <summary>
        /// Event fired when data changes - allows UI to refresh automatically
        /// </summary>
        event EventHandler? DataChanged;
    }
}
