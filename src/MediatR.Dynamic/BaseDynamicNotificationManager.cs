﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Dynamic
{
    public abstract class BaseDynamicNotificationManager<TNotification> 
        : IDynamicNotificationHandler<TNotification>, IDisposable
            where TNotification : INotification
    { 
        private readonly IDynamicNotificationManager<TNotification> _manager;
        public BaseDynamicNotificationManager(IDynamicNotificationManager<TNotification> registrar)
        {
            _manager = registrar; 
            _manager.RegisterHandler(this);
        }
        ~BaseDynamicNotificationManager()
        {
            _manager.UnRegisterHandler(this);
        }
        public void Dispose()
        {
            _manager.UnRegisterHandler(this);
        }
        public abstract Task Handle(TNotification notification, CancellationToken cancellationToken);
    }
}
