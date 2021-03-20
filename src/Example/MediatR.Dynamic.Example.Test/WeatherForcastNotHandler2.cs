﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Dynamic.Example.Test
{
    public class WeatherForcastNotHandler2 : IDynamicNotificationHandler<WeatherForecastRequest>
    {

        private readonly IDynamicNotificationManager<WeatherForecastRequest> _registrar;
        public WeatherForcastNotHandler2(IDynamicNotificationManager<WeatherForecastRequest> registrar)
        {

            _registrar = registrar;

            // Dynamically add this class as a handler for the notification type 'YourNotificationTypeHere'
            _registrar.RegisterHandler(this);
        }

        public async Task Handle(WeatherForecastRequest notification, CancellationToken cancellationToken)
        {

        }

        ~WeatherForcastNotHandler2()
        {
            // Un-register this class as an event handler for the notification type
            _registrar.UnRegisterHandler(this);
        }
    }
}