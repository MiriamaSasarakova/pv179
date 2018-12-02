using System.Collections.Generic;
using BL.DTOs.Base;

///////////////////////////////////////////
/// Pri zobrazeni vsech userovych aukci ///
///////////////////////////////////////////

namespace BL.DTOs.Users
{
    public class UserShowAuctions
    {
        public string Name { get; set; }
        
        public List<AuctionDto> Auctions { get; set; }
    }
}