using Logistic.Core.Services;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Logistics.Wpf
{
    public static class ReportTypeEnumWrapper
    {
        public static IEnumerable<ReportType> AllReportTypes
        {
            get { return Enum.GetValues(typeof(ReportType)).Cast<ReportType>(); }
        }
    }
}
