using System;
using BL.DTOs.Common;

namespace BL.DTOs.Filter
{
    public class AuctionFilterDto : FilterDtoBase
    {
        public string AuctionSearchedName { get; set; }

        public double MinimalPrice { get; set; } = 0;

        public double MaximalPrice { get; set; } = double.MaxValue;

        public DateTime ActualDateTime { get; set; } = DateTime.MaxValue;

        public bool IncludeEnded { get; set; } = false;

        public int AuctionerID { get; set; }

    }
}
