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
    
    public partial class Address
    {
        public Address()
        {
            this.ClientAddress = new HashSet<ClientAddress>();
        }
    
        public int idAddress { get; set; }
        public int idCity { get; set; }
        public string MailIndex { get; set; }
        public string Street { get; set; }
    
        public virtual City City { get; set; }
        public virtual ICollection<ClientAddress> ClientAddress { get; set; }
    }
}