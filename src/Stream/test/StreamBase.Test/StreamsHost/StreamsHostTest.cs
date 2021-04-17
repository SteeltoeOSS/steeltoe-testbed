﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using Steeltoe.Messaging.RabbitMQ.Config;
using Steeltoe.Stream.Attributes;
using Steeltoe.Stream.Config;
using Steeltoe.Stream.Messaging;
using System;
using System.ComponentModel;
using Xunit;

namespace Steeltoe.Stream.StreamsHost
{
    public class StreamsHostTest
    {
        [Fact]
        public void HostCanBeStarted()
        {
            FakeHostedService service;
            using (var host = StreamsHost.CreateDefaultBuilder<SampleSink>()
                .ConfigureServices(svc => svc.AddSingleton<IHostedService, FakeHostedService>())
                .Start())
            {
                Assert.NotNull(host);
                service = (FakeHostedService)host.Services.GetRequiredService<IHostedService>();
                Assert.NotNull(service);
                Assert.Equal(1, service.StartCount);
                Assert.Equal(0, service.StopCount);
                Assert.Equal(0, service.DisposeCount);
            }

            Assert.Equal(1, service.StartCount);
            Assert.Equal(0, service.StopCount);
            Assert.Equal(1, service.DisposeCount);
        }
    }

    [EnableBinding(typeof(ISink))]
    public class SampleSink
    {
        [StreamListener("input")]
        public void HandleInputMessage(string foo)
        {
        }
    }
}
