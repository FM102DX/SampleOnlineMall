﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SampleOnlineMall.Service;

namespace SampleOnlineMall.DataAccess.Abstract
{
    public interface IRepositoryRequest<T> where T : IBaseEntity
    {
        public ISearchOptions<T> SearchOptions { get; set; }
        public bool UsePagination { get; set; }
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }

    }
}