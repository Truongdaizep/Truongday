﻿using NETCORE.Data.Entities;

namespace NETCORE.Data.Entities.Catalog
{
    public class Message
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime Timestamp { get; set; }
        public User? FromUser { get; set; }
        public int ToRoomId { get; set; }
        public Room? ToRoom { get; set; }
    }
}