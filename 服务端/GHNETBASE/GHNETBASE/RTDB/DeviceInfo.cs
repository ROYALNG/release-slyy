using GHRestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHNETBASE.RTDB
{
    public class DeviceInfo : IClientInfo
    {
        string resourceName = "";
        private GHRestClient.GHRestClient client;

        public DeviceInfo()
        { }


        public GHRTDBResponse AddChannel(IRequest input)
        {
            return this.client.execute<GHRTDBResponse>(GHRestClient.GHRestClient.Method.POST, this.resourceName, new Dictionary<string, string>(), input);
        }
        public GHRTDBResponse AddController(IRequest input)
        {
            return this.client.execute<GHRTDBResponse>(GHRestClient.GHRestClient.Method.POST, this.resourceName, new Dictionary<string, string>(), input);
        }

        public GHRTDBResponse AddVariable(IRequest input)
        {
            return this.client.execute<GHRTDBResponse>(GHRestClient.GHRestClient.Method.POST, this.resourceName, new Dictionary<string, string>(), input);
        }

        public GHRTDBResponse WriteVariable(IRequest input)
        {
            return this.client.execute<GHRTDBResponse>(GHRestClient.GHRestClient.Method.POST, this.resourceName, new Dictionary<string, string>(), input);
        }

        public GHRTDBResponse ReadVariable()
        {
            return this.client.execute<GHRTDBResponse>(GHRestClient.GHRestClient.Method.GET, this.resourceName, new Dictionary<string, string>());
        }

        public GHRTDBResponse ReadVariableMult(IRequest input)
        {
            return this.client.execute<GHRTDBResponse>(GHRestClient.GHRestClient.Method.POST, this.resourceName, new Dictionary<string, string>(), input);
        }

        public GHRTDBResponse ReadChannel()
        {
            return this.client.execute<GHRTDBResponse>(GHRestClient.GHRestClient.Method.GET, this.resourceName, new Dictionary<string, string>());
        }
        public GHRTDBResponse ReadController()
        {
            return this.client.execute<GHRTDBResponse>(GHRestClient.GHRestClient.Method.GET, this.resourceName, new Dictionary<string, string>());
        }

        public GHRTDBResponse WriteAlarm(IRequest input)
        {
            return this.client.execute<GHRTDBResponse>(GHRestClient.GHRestClient.Method.POST, this.resourceName, new Dictionary<string, string>(), input);
        }

        public void SetClient(GHRestClient.GHRestClient client)
        {
            this.client = client;
        }

        public void SetName(string name)
        {
            this.resourceName = name;
        }
    }
}
