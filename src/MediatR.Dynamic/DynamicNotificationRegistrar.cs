﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Dynamic
{
    internal class DynamicNotificationRegistrar<TNoficiation> : IDynamicNotificationRegistrar<TNoficiation>
        where TNoficiation : INotification
    {
        private List<IDynamicNotificationHandler<TNoficiation>> _handlers = new List<IDynamicNotificationHandler<TNoficiation>>();

        public async Task Handle(TNoficiation notification, CancellationToken cancellationToken)
        {
            // Copy the collection so that the collection is static (not modified)
            List<IDynamicNotificationHandler<TNoficiation>> handlersToExecute;
            lock (_collectionLock)
            {
                handlersToExecute = new List<IDynamicNotificationHandler<TNoficiation>>(this._handlers);
            }

            foreach (var handler in handlersToExecute)
                await handler.Handle(notification, cancellationToken);
        }

        private static readonly object _collectionLock = new object();
        public void RegisterHandler(IDynamicNotificationHandler<TNoficiation> handler)
        {
            lock (_collectionLock)
            {
                if (!this._handlers.Contains(handler))
                    this._handlers.Add(handler);
            }
        }

        public void UnregisterHandler(IDynamicNotificationHandler<TNoficiation> handler)
        {
            lock (_collectionLock)
            {
                if (this._handlers.Contains(handler))
                    this._handlers.Remove(handler);
            }
        }
    }
}
