//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace skeleton_market.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderStatus
    {
        public int idOrderStatus { get; set; }
        public Nullable<int> idEmployee { get; set; }
        public System.DateTime Date { get; set; }
        public int idStatus { get; set; }
        public int idCheckout { get; set; }
    
        public virtual Checkout Checkout { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Status Status { get; set; }
    }
}
