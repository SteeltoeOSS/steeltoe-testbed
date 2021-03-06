﻿#pragma warning disable SA1636 // File header copyright text should match

// <copyright file="AggregationType.cs" company="OpenTelemetry Authors">
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

using System;

namespace Steeltoe.Management.OpenTelemetry.Metrics.Export
{
    [Obsolete("OpenTelemetry Metrics API is not considered stable yet, see https://github.com/SteeltoeOSS/Steeltoe/issues/711 more information")]
    public enum AggregationType
    {
        /// <summary>
        /// Sum of type Double which is reported with <see cref="SumData{T}"/>
        /// </summary>
        DoubleSum,

        /// <summary>
        /// Sum of type Long which is reported with <see cref="SumData{T}"/>
        /// </summary>
        LongSum,

        /// <summary>
        /// Summary of measurements (Min, Max, Sum, Count), which is reported with <see cref="SummaryData{T}"/>
        /// </summary>
        Summary,
    }
}
