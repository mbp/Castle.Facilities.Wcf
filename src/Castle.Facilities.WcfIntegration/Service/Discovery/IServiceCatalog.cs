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

namespace Castle.Facilities.WcfIntegration
{
#if DOTNET40
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Discovery.Version11;

    [ServiceContract(Name = "ServiceCatalog", Namespace = WcfConstants.Namespace)]
    public interface IServiceCatalog
    {
		[OperationContract]
		EndpointDiscoveryMetadata11[] FindServices(FindCriteria11 criteria);

        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginFindServices(FindCriteria11 criteria, AsyncCallback callback, object state);
		EndpointDiscoveryMetadata11[] EndFindServices(IAsyncResult result);

		[OperationContract]
		EndpointDiscoveryMetadata11[] ListServices();

		[OperationContract(AsyncPattern = true)]
        IAsyncResult BeginListServices(AsyncCallback callback, object state);
        EndpointDiscoveryMetadata11[] EndListServices(IAsyncResult result);
    }
#endif
}

