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
    
    public partial class Client
    {
        public Client()
        {
            this.Checkout = new HashSet<Checkout>();
            this.ClientAddress = new HashSet<ClientAddress>();
            this.Wishlist = new HashSet<Wishlist>();
        }
    
        public int idClient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public System.DateTime BirthDay { get; set; }
        public int idGender { get; set; }
        public string Password { get; set; }
    
        public virtual ICollection<Checkout> Checkout { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual ICollection<ClientAddress> ClientAddress { get; set; }
        public virtual ICollection<Wishlist> Wishlist { get; set; }
    }
}
