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
    
    public partial class Avard
    {
        public Avard()
        {
            this.ExpositionWinners = new HashSet<ExpositionWinner>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<ExpositionWinner> ExpositionWinners { get; set; }
    }
}
