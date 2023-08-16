using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandByHand
{
    public partial class MentorWorkClass : Student
    {
        public string Comment { get; set; }
        static public List<string> CommentList { get; set; }
        public MentorWorkClass()
        {
            CommentList = new List<string>
            {
                "Выполняет действие самостоятельно",
                "Выполняет действие по инструкции",
                "Выполняет действие по образцу",
                "Выполняет действие с помощью",
                "Действие не выполняет"
            };
        } 
        public MentorWorkClass ConvertStudent(Student oldStudent)
        {
            MentorWorkClass newStudent = new MentorWorkClass();
            newStudent.ID = oldStudent.ID;
            newStudent.Name = oldStudent.Name;
            newStudent.Surname = oldStudent.Surname;
            newStudent.Patronymic = oldStudent.Patronymic;
            newStudent.ID_Direction = oldStudent.ID_Direction;
            return newStudent;
        }
        public MentorWorkClass ConvertStudent(Student oldStudent,Class studentClass)
        {
            MentorWorkClass newStudent = new MentorWorkClass();
            newStudent.ID = oldStudent.ID;
            newStudent.Name = oldStudent.Name;
            newStudent.Surname = oldStudent.Surname;
            newStudent.Patronymic = oldStudent.Patronymic;
            newStudent.ID_Direction = oldStudent.ID_Direction;
            newStudent.Comment = studentClass.Comment;
            return newStudent;
        }
    }
}
