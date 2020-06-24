using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventHorizon.Game.Client.Engine.Lifecycle.Api
{
    public interface IServiceEntity
    {
        /// <summary>
        /// Controls the call order of Initialize and Dispose for all Service Entites.
        /// 
        /// For Initialize call Lowest number goes First.
        /// For Dispose call Lowest Number goes Last.
        /// </summary>
        int Priority { get; }
        Task Initialize();
        Task Dispose();
    }
}
