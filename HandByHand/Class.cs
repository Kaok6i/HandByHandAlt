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

    public partial class Class
    {
        Entities entity = new Entities();
        public int ID { get; set; }
        public string Theme_Class { get; set; }
        public Nullable<int> ID_Direction { get; set; }

        public string Name_Direction
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
        public Nullable<int> ID_Student { get; set; }

        public string Name_Student
        {
            get
            {
                Student stud = new Student();
                stud = entity.Student.Where(x => x.ID == ID_Student).FirstOrDefault();
                if (stud != null)
                {
                    return stud.Name;
                }
                return null;
            }
        }
        public string Surname_Student
        {
            get
            {
                Student stud = new Student();
                stud = entity.Student.Where(x => x.ID == ID_Student).FirstOrDefault();
                if (stud != null)
                {
                    return stud.Surname;
                }
                return null;
            }
        }
        public string Patronymic_Student
        {
            get
            {
                Student stud = new Student();
                stud = entity.Student.Where(x => x.ID == ID_Student).FirstOrDefault();
                if (stud != null)
                {
                    return stud.Patronymic;
                }
                return null;
            }
        }
        public string Comment { get; set; }
        public Nullable<int> Time_Passed { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual Direction Direction { get; set; }
        public virtual Student Student { get; set; }
    }
}
