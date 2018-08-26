using System;
using System.Runtime.Serialization;

namespace RedisPubSubTest
{
    public interface IGrainIdentifierBase : ISerializable
    {
        
    }

    public interface IGrainIdentifierAsGuid : IGrainIdentifierBase
    {
        Guid Id { get; set; }
    }

    public interface IGrainIdentifierAsInt : IGrainIdentifierBase
    {
        int Id { get; set;}
    }

    public interface IGrainIdentifierAsString : IGrainIdentifierBase
    {
        string Id { get; set;}
    }
}