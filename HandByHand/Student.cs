//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HandByHand
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            this.Class = new HashSet<Class>();
        }
        Entities entity = new Entities();
        public int ID { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public System.DateTime Date_Of_Birthday { get; set; }
        public string Start_Year { get; set; }
        public Nullable<int> ID_Direction { get; set; }
        public string NameDirection
        {
            get
            {
                Direction direction = new Direction();
                direction = entity.Direction.Where(d => d.ID == ID_Direction).FirstOrDefault();
                if (direction != null)
                {
                    return direction.Name_Direction;
                }
                return null;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Class> Class { get; set; }
        public virtual Direction Direction { get; set; }
    }
}
