﻿using SampleOnlineMall.DataAccess.Abstract;
using SampleOnlineMall.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOnlineMall.DataAccess.Models
{
    public class RepositoryResponce<T> where T : IBaseEntity
    {
        public bool UsedPagination { get; set; }
        public bool UsedSearch { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }

        public CommonOperationResult Result { get; set; }

        public IEnumerable<T> Items{ get; set; }
    }
}
