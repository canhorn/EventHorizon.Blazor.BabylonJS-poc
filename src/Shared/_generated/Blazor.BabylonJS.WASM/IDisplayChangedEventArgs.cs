/// Generated - Do Not Edit
namespace BabylonJS
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using EventHorizon.Blazor.Interop;
    using Microsoft.JSInterop;

    public interface IDisplayChangedEventArgs : ICachedEntity { }
    
    [JsonConverter(typeof(CachedEntityConverter))]
    public class IDisplayChangedEventArgsCachedEntity : CachedEntityObject, IDisplayChangedEventArgs
    {
        #region Static Accessors

        #endregion

        #region Static Properties

        #endregion

        #region Static Methods

        #endregion

        #region Accessors

        #endregion

        #region Properties
        
        public CachedEntity vrDisplay
        {
            get
            {
            return EventHorizonBlazorInterop.Get<CachedEntity>(
                    this.___guid,
                    "vrDisplay"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "vrDisplay",
                    value
                );
            }
        }

        
        public bool vrSupported
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "vrSupported"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "vrSupported",
                    value
                );
            }
        }
        #endregion
        
        #region Constructor
        public IDisplayChangedEventArgsCachedEntity() : base() { }

        public IDisplayChangedEventArgsCachedEntity(
            ICachedEntity entity
        ) : base(entity)
        {
        }


        #endregion

        #region Methods

        #endregion
    }
}