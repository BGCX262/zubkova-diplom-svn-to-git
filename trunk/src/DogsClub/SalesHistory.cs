//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DogsClub
{
    using System;
    using System.Collections.Generic;
    
    public partial class SalesHistory
    {
        public int Id { get; set; }
        public int IdDog { get; set; }
        public int OldOwnerId { get; set; }
        public decimal CostPrice { get; set; }
        public decimal ClubCommission { get; set; }
        public int NewOwnerId { get; set; }
        public System.DateTime SaleDate { get; set; }
    
        public virtual Dog Dog { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
