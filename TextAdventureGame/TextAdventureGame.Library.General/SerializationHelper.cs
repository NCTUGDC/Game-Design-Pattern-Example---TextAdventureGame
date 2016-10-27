﻿using MsgPack.Serialization;
using System.IO;

namespace TextAdventureGame.Library.General
{
    public static class SerializationHelper
    {
        public static T Deserialize<T>(byte[] data)
        {
            var serializer = MessagePackSerializer.Get<T>();
            using (MemoryStream ms = new MemoryStream(data))
            {
                return serializer.Unpack(ms);
            }
        }

        public static byte[] Serialize<T>(T data)
        {
            var serializer = MessagePackSerializer.Get<T>();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.Pack(memoryStream, data);
                return memoryStream.ToArray();
            }
        }
    }
}
