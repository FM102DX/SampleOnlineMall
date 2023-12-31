﻿using System;
using SampleOnlineMall.DataAccess.Abstract;

namespace SampleOnlineMall.DataAccess.Models
{
    public class BaseEntity:IBaseEntity
    {
        public Guid Id { get; set; }

        public BaseEntity()
        {
            Id = System.Guid.NewGuid();
            CreatedDateTime = DateTime.Now;
        }

        public BaseEntity(Guid id, DateTime createdDateTime, DateTime lastModifiedDateTime)
        {
            Id = id;
            CreatedDateTime = createdDateTime;
            LastModifiedDatTime = lastModifiedDateTime;
        }

        public string SysMessage { get; set; } // for diagnostic purposes
        public string ShortTimeStamp { get { return $"{CreatedDateTime.ToShortDateString()} {CreatedDateTime.ToShortTimeString()}"; } }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastModifiedDatTime { get; set; }

        public virtual BaseEntity Clone()
        {
            return new BaseEntity() { Id = Id, SysMessage = SysMessage, CreatedDateTime = CreatedDateTime, LastModifiedDatTime = LastModifiedDatTime };
        }

        public virtual string ShortString() { return ""; }
    }
}