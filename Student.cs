using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_3_students
{
    public abstract class Student
    {
        private string fullName, faculty;
        private int course;
        private List<int> grades;

        public string FullName
        {
            get { return fullName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))//відсутнє знач., порожній рядок, пробіли
                    throw new ArgumentException("ПІБ не може бути порожнім", nameof(value));
                fullName = value;
            }
        }
        public string Faculty
        {
            get { return faculty; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Факультет не може бути порожнім", nameof(value));
                faculty = value;
            }
        }
        public int Course
        {
            get { return course; }
            set
            {
                ValidateCourse(value);
                course = value;
            }
        }

        protected virtual void ValidateCourse(int value)
        {
            if (value < 1 || value > 6)
                throw new ArgumentException("Курс повинен бути від 1 до 6.", nameof(value));
        }
        
        public List<int> Grades {
            get {
                return grades;
            }
        }
        public Student(string fullName, string faculty, int course)
        {
            FullName = fullName;
            Faculty = faculty;
            Course = course;
            grades = new List<int>();
           
        }
        public void AddGrade(int grade)
        {
            if (grade < 0 || grade > 100)
                throw new ArgumentException("Оцінка повинна бути від 0 до 100.", nameof(grade));
            grades.Add(grade);
        }

        public double CalculateAverageGrade()
        {
            if (grades == null || grades.Count == 0)
                return 0;
            return grades.Average();
        }

        public virtual string GetInfo()
        {
            return $"ПІБ: {FullName}, Факультет: {Faculty}, Курс: {Course}, " +
                   $"Середній бал: {CalculateAverageGrade():F2}";
        }
    }
}
