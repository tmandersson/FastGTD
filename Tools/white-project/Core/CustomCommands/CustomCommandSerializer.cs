using System;
using System.Collections.Generic;
using System.IO;
using Bricks.Objects;

namespace White.Core.CustomCommands
{
    public class CustomCommandSerializer
    {
        private readonly BricksDataContractSerializer bricksDataContractSerializer = new BricksDataContractSerializer();

        private static readonly List<Type> standardKnownTypes = new List<Type>
                                                           {
                                                               typeof (object[]),
                                                               typeof (string[]),
                                                               typeof (int[]),
                                                               typeof (double[]),
                                                               typeof (long[]),
                                                               typeof (decimal[]),
                                                               typeof (float[]),
                                                               typeof (short[]),
                                                               typeof (bool[]),
                                                               typeof (DateTime[]),
                                                               typeof(Exception)
                                                           };

        public static void AddKnownTypes(params Type[] additionalKnownTypes)
        {
            standardKnownTypes.AddRange(additionalKnownTypes);
        }

        public virtual string SerializeAssembly(string assemblyFile)
        {
            var serializeAssemblyRequest = new object[]
                                               {new FileInfo(assemblyFile).Name, File.ReadAllBytes(assemblyFile)};
            return bricksDataContractSerializer.ToString(serializeAssemblyRequest, new Type[0]);
        }

        public virtual string Serialize(string assemblyName, string typeName, string method, object[] arguments)
        {
            var commandList = new object[] {typeName, method, @arguments};
            string payload = bricksDataContractSerializer.ToString(commandList, standardKnownTypes);

            var request = new object[] {assemblyName, payload};
            return bricksDataContractSerializer.ToString(request, standardKnownTypes);
        }

        public virtual object[] ToObject(string @string)
        {
            return bricksDataContractSerializer.ToObject<object[]>(@string, standardKnownTypes);
        }

        public virtual string SerializeEndSession()
        {
            return bricksDataContractSerializer.ToString(new object[0], new Type[0]);
        }
    }
}