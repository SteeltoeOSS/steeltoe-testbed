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

using Steeltoe.Stream.Binder;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Steeltoe.Stream.Binding
{
    public class DynamicDestinationsBindable : AbstractBindable
    {
        private readonly ConcurrentDictionary<string, IBinding> _outputBindings = new ConcurrentDictionary<string, IBinding>();

        public DynamicDestinationsBindable()
            : base()
        {
        }

        public override Type BindingType => typeof(DynamicDestinationsBindable);

        public override ICollection<string> Outputs
        {
            get
            {
                return _outputBindings.Keys;
            }
        }

        public void AddOutputBinding(string name, IBinding binding)
        {
            _outputBindings.TryAdd(name, binding);
        }

        public override void UnbindOutputs(IBindingService adapter)
        {
            foreach (var entry in _outputBindings)
            {
                _outputBindings.TryRemove(entry.Key, out var binding);
                if (binding != null)
                {
                    binding.Unbind();
                }
            }

            return;
        }
    }
}
