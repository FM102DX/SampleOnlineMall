using SampleOnlineMall.DataAccess.Abstract;
using SampleOnlineMall.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOnlineMall.Core.Models
{
    public class Supplier : BaseEntity, IBaseEntity
    {
        public string Name { get; set; }

    }
}
