using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_3_students
{
    public class ListOfStudents
    {
        public static Student GetTopStudent(List<Student> students)
        {
            if(students == null || students.Count == 0)
            {
                return null;
            }
            Student topStudent = students[0];
            double maxAverage = topStudent.CalculateAverageGrade();
            foreach(Student student in students)
            {
                double average = student.CalculateAverageGrade();
                if (average > maxAverage)
                {
                    maxAverage = average;
                    topStudent = student;
                }
            }
            return topStudent;
        }
    }
}
