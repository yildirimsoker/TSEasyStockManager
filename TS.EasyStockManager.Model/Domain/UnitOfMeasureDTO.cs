using System;
using System.Collections.Generic;
using System.Text;

namespace TS.EasyStockManager.Model.Domain
{
    public class UnitOfMeasureDTO : BaseDTO
    {
        public string UnitOfMeasureName { get; set; }
        public string Isocode { get; set; }
    }
}
