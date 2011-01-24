﻿// Copyright 2004-2011 Castle Project - http://www.castleproject.org/
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

namespace Castle.Facilities.WcfIntegration.Tests
{
	using System;
	using System.ServiceModel;

	using Castle.Facilities.WcfIntegration.Demo;
	using Castle.Facilities.WcfIntegration.Service;
	using Castle.Facilities.WcfIntegration.Service.Default;
	using Castle.MicroKernel.Registration;
	using Castle.Windsor;

	using NUnit.Framework;

	[TestFixture]
	public class ServiceHostFactoryFixture
	{
		[Test]
		[Ignore("This test requires the Castle.Facilities.WcfIntegration.Demo running")]
		public void CanCallHostedService()
		{
			var client = ChannelFactory<IAmUsingWindsor>.CreateChannel(
				new BasicHttpBinding(), new EndpointAddress("http://localhost:27197/UsingWindsorWithoutConfig.svc"));
			Assert.AreEqual(42, client.GetValueFromWindsorConfig());
		}

		[Test]
		public void CanCreateServiceByName()
		{
			var windsorContainer = new WindsorContainer()
				.Register(Component.For<IOperations>().ImplementedBy<Operations>().Named("operations"),
				          Component.For<IServiceHostBuilder<DefaultServiceModel>>().ImplementedBy<DefaultServiceHostBuilder>()
				);

			var factory = new DefaultServiceHostFactory(windsorContainer.Kernel);
			var serviceHost = factory.CreateServiceHost("operations",
			                                            new[] { new Uri("http://localhost/Foo.svc") });
			Assert.IsNotNull(serviceHost);
		}

		[Test]
		public void CanCreateWindsorHostFactory()
		{
			var factory = new DefaultServiceHostFactory(new WindsorContainer().Kernel);
			Assert.IsNotNull(factory);
		}
	}
}