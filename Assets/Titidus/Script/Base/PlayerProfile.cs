using System;
using System.Runtime.Serialization;
using System.Xml.Linq;
using Unity.Collections;
using Unity.Netcode;


namespace Titidus
{
[Serializable]
    public struct PlayerProfile: INetworkSerializable, IEquatable<PlayerProfile>
    {
        public FixedString64Bytes name;
        public int avt_index;

        public bool Equals(PlayerProfile other)
        {
            return name.Equals(other.name)
                && avt_index == other.avt_index;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref name);
            serializer.SerializeValue(ref avt_index);
        }
    }
}