using System;
using System.Reflection;

namespace hase.DevLib.Framework.Contract
{
    public class AppRequestMessage : AppMessage
    {
        // todo: 06/05/18 gph. Move this into a header.
        public string ServiceTypeName { get; set; }

        public AppRequestMessage()
        {
            this.ServiceTypeName = GetServiceNameFromRequestName(this.GetType().Name);
        }

        public override string ToString()
        {
            return $"RequestId[{this.Headers.MessageId}]";
        }

        private string GetServiceNameFromRequestName(string requestName)
        {
            // todo: 06/05/18 gph. Multiple convention assumptions. Need to revisit. 
            var svcName = requestName.Replace("Request", "Service");

            var fqn = default(string);
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.Name == svcName)
                {
                    fqn = type.AssemblyQualifiedName;
                    break;
                }
            }
            return fqn;
        }
    }
}
