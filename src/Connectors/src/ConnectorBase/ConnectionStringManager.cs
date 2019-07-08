// Copyright 2017 the original author or authors.
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

using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace Steeltoe.CloudFoundry.Connector
{
    public class ConnectionStringManager
    {
        private readonly IConfiguration _configuration;

        public ConnectionStringManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Connection Get<T>(string serviceName = null)
            where T : IConnectionInfo, new()
        {
            return new T().Get(_configuration, serviceName);
        }
    }
}