﻿#pragma warning disable SA1636 // File header copyright text should match

// <copyright file="Int64BoundMeasureMetricSdk.cs" company="OpenTelemetry Authors">
// Copyright 2018, OpenTelemetry Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
#pragma warning restore SA1636 // File header copyright text should match

using OpenTelemetry.Trace;
using Steeltoe.Management.OpenTelemetry.Metrics.Aggregators;
using System;

namespace Steeltoe.Management.OpenTelemetry.Metrics
{
    [Obsolete("OpenTelemetry Metrics API is not considered stable yet, see https://github.com/SteeltoeOSS/Steeltoe/issues/711 more information")]
    internal class Int64BoundMeasureMetricSdk : BoundMeasureMetricSdkBase<long>
    {
        private readonly Int64MeasureMinMaxSumCountAggregator _measureAggregator = new ();

        public override void Record(in SpanContext context, long value) => _measureAggregator.Update(value);

        public override Aggregator<long> GetAggregator() => _measureAggregator;
    }
}
