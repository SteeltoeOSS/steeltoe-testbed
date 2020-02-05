﻿// Copyright 2017 the original author or authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Microsoft.Extensions.DependencyInjection;
using Moq;
using Steeltoe.Common.Lifecycle;
using Steeltoe.Messaging;
using Steeltoe.Stream.Config;
using System.Threading.Tasks;
using Xunit;

namespace Steeltoe.Stream.Binder
{
    public class ArbitraryInterfaceWithBindingTargetsTest : AbstractTest
    {
        [Fact]
        public async Task TestArbitraryInterfaceChannelsBound()
        {
            var searchDirectories = GetSearchDirectories("MockBinder");
            var provider = CreateStreamsContainerWithBinding(
                searchDirectories,
                typeof(IFooChannels),
                "spring:cloud:stream:defaultBinder=mock",
                "spring:cloud:stream:bindings:Foo:destination=someQueue.0",
                "spring:cloud:stream:bindings:Bar:destination=someQueue.1",
                "spring:cloud:stream:bindings:Baz:destination=someQueue.2",
                "spring:cloud:stream:bindings:Qux:destination=someQueue.3")
                .BuildServiceProvider();

            await provider.GetRequiredService<ILifecycleProcessor>().OnRefresh(); // Only starts Autostart

            var factory = provider.GetService<IBinderFactory>();
            Assert.NotNull(factory);
            var binder = factory.GetBinder(null, typeof(IMessageChannel));
            Assert.NotNull(binder);
            var fooChannels = provider.GetService<IFooChannels>();
            Assert.NotNull(fooChannels);

            var mock = Mock.Get(binder);
            mock.Verify(b => b.BindConsumer("someQueue.0", null, fooChannels.Foo, It.IsAny<ConsumerOptions>()));
            mock.Verify(b => b.BindConsumer("someQueue.1", null, fooChannels.Bar, It.IsAny<ConsumerOptions>()));

            mock.Verify(b => b.BindProducer("someQueue.2", fooChannels.Baz, It.IsAny<ProducerOptions>()));
            mock.Verify(b => b.BindProducer("someQueue.3", fooChannels.Qux, It.IsAny<ProducerOptions>()));
        }
    }
}
