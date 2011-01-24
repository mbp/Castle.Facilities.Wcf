// Copyright 2004-2011 Castle Project - http://www.castleproject.org/
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

namespace Castle.Facilities.WcfIntegration.Service
{
	using System;
	using System.ServiceModel;

	public class AdHocServiceHostAware : AbstractServiceHostAware
	{
		private Action<ServiceHost> onClosed;
		private Action<ServiceHost> onClosing;
		private Action<ServiceHost> onCreated;
		private Action<ServiceHost> onFaulted;
		private Action<ServiceHost> onOpened;
		private Action<ServiceHost> onOpening;

		public AdHocServiceHostAware OnClosed(Action<ServiceHost> action)
		{
			onClosed += action;
			return this;
		}

		public AdHocServiceHostAware OnClosing(Action<ServiceHost> action)
		{
			onClosing += action;
			return this;
		}

		public AdHocServiceHostAware OnCreated(Action<ServiceHost> action)
		{
			onCreated += action;
			return this;
		}

		public AdHocServiceHostAware OnFaulted(Action<ServiceHost> action)
		{
			onFaulted = (Action<ServiceHost>)Delegate.Combine(onFaulted, action);
			return this;
		}

		public AdHocServiceHostAware OnOpened(Action<ServiceHost> action)
		{
			onOpened += action;
			return this;
		}

		public AdHocServiceHostAware OnOpening(Action<ServiceHost> action)
		{
			onOpening += action;
			return this;
		}

		protected override void Closed(ServiceHost serviceHost)
		{
			Apply(serviceHost, onClosed);
		}

		protected override void Closing(ServiceHost serviceHost)
		{
			Apply(serviceHost, onClosing);
		}

		protected override void Created(ServiceHost serviceHost)
		{
			Apply(serviceHost, onCreated);
		}

		protected override void Faulted(ServiceHost serviceHost)
		{
			Apply(serviceHost, onFaulted);
		}

		protected override void Opened(ServiceHost serviceHost)
		{
			Apply(serviceHost, onOpened);
		}

		protected override void Opening(ServiceHost serviceHost)
		{
			Apply(serviceHost, onOpening);
		}

		private static void Apply(ServiceHost serviceHost, Action<ServiceHost> action)
		{
			if (action != null)
			{
				action(serviceHost);
			}
		}
	}
}