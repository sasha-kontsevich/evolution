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
    
    public partial class Awards
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Awards()
        {
            this.UsersAwards = new HashSet<UsersAwards>();
        }
    
        public int AwardID { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public Nullable<byte> Level { get; set; }
        public byte[] Image { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsersAwards> UsersAwards { get; set; }
    }
}
