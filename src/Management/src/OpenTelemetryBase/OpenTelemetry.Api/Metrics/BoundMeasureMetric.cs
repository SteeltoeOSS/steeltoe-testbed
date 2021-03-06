﻿#pragma warning disable SA1636 // File header copyright text should match

// <copyright file="BoundMeasureMetric.cs" company="OpenTelemetry Authors">
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
using System;

namespace Steeltoe.Management.OpenTelemetry.Metrics
{
    /// <summary>
    /// Bound measure metric with the defined <see cref="LabelSet"/>.
    /// </summary>
    /// <typeparam name="T">The type of Measure. Only long and double are supported now.</typeparam>
    [Obsolete("OpenTelemetry Metrics API is not considered stable yet, see https://github.com/SteeltoeOSS/Steeltoe/issues/711 more information")]
    public abstract class BoundMeasureMetric<T>
        where T : struct
    {
        /// <summary>
        /// Record the given value to the bound measure metric.
        /// </summary>
        /// <param name="context">the associated span context.</param>
        /// <param name="value">the measurement to be recorded.</param>
        public abstract void Record(in SpanContext context, T value);
    }
}
