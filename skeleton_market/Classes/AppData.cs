using skeleton_market.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skeleton_market.Classes
{
    internal class AppData
    {
        public static EF.Entities Context { get; } = new EF.Entities();
        public static EF.Client current_user = null;
        public static EF.Merchandise last_merch = null;
        public static bool isLastPageWasAuth = false;
        public static List<MerchSize> user_bag { get; set; } = new List<MerchSize>();
        public static EF.PickUpPoint pick_point = null;
       
    }
}
