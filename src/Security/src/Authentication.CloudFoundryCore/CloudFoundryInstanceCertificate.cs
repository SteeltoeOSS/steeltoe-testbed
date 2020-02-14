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

using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Steeltoe.Security.Authentication.Mtls
{
    public class CloudFoundryInstanceCertificate
    {
        private const string CloudFoundryInstanceCertSubjectRegex =
            @"^CN=(?<instance>[0-9a-f-]+),\sOU=organization:(?<org>[0-9a-f-]+)\s\+\sOU=space:(?<space>[0-9a-f-]+)\s\+\sOU=app:(?<app>[0-9a-f-]+)$";

        private const string FallbackInstanceCertSubjectRegex =
            @"^CN=(?<instance>[0-9a-f-]+),\sOU=app:(?<app>[0-9a-f-]+)\s\+\sOU=space:(?<space>[0-9a-f-]+)\s\+\sOU=organization:(?<org>[0-9a-f-]+)$";

        public static bool TryParse(X509Certificate2 certificate, out CloudFoundryInstanceCertificate cloudFoundryInstanceCertificate)
        {
            cloudFoundryInstanceCertificate = null;
            if (certificate == null)
            {
                return false;
            }

            var cfInstanceMatch = Regex.Match(certificate.Subject.Replace("\"", string.Empty), CloudFoundryInstanceCertSubjectRegex);

            // TODO: handle differently - certs generated locally get the OUs in a different order
            if (!cfInstanceMatch.Success)
            {
                cfInstanceMatch = Regex.Match(certificate.Subject.Replace("\"", string.Empty), FallbackInstanceCertSubjectRegex);
            }

            if (cfInstanceMatch.Success)
            {
                Console.WriteLine("Subject matched an expected OU pattern: {0}", certificate.Subject);
                cloudFoundryInstanceCertificate = new CloudFoundryInstanceCertificate
                {
                    OrgId = cfInstanceMatch.Groups["org"].Value,
                    SpaceId = cfInstanceMatch.Groups["space"].Value,
                    AppId = cfInstanceMatch.Groups["app"].Value,
                    InstanceId = cfInstanceMatch.Groups["instance"].Value,
                    Certificate = certificate
                };
            }
            else
            {
                Console.WriteLine("Subject failed to match an expected OU pattern: {0}", certificate.Subject);
            }

            return cfInstanceMatch.Success;
        }

        public string OrgId { get; private set; }

        public string SpaceId { get; private set; }

        public string AppId { get; private set; }

        public string InstanceId { get; private set; }

        public X509Certificate2 Certificate { get; private set; }
    }
}