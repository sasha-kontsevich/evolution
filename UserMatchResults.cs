//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace evolution
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserMatchResults
    {
        public int UserID { get; set; }
        public int MatchID { get; set; }
        public Nullable<int> Place { get; set; }
        public Nullable<int> Score { get; set; }
    
        public virtual Match Matches { get; set; }
        public virtual User Users { get; set; }
    }
}