using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_3_students
{
    public class Master : Student
    {
        private string thesisTopic;
        private int publicationsCount;

        public string ThesisTopic
        {
            get { return thesisTopic; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Тема роботи не може бути порожньою", nameof(value));
                thesisTopic = value;
            }
        }

        public int PublicationsCount
        {
            get { return publicationsCount; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Кількість публікацій не може бути від'ємною", nameof(value));
                publicationsCount = value;
            }
        }

        public Master(string fullName, string faculty, int course, string thesisTopic, int publicationsCount)
            : base(fullName, faculty, course)
        {
            ThesisTopic = thesisTopic;
            PublicationsCount = publicationsCount;
        }

        protected override void ValidateCourse(int value)
        {
            if (value < 5 || value > 6)
                throw new ArgumentException("Курс магістра повинен бути 5 або 6.", nameof(value));
        }

        public double GetAcademicRating()
        {
            return CalculateAverageGrade() + publicationsCount * 2;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $", Тема роботи: {ThesisTopic}, " +
                   $"Публікацій: {PublicationsCount}, " +
                   $"Академічний рейтинг: {GetAcademicRating():F2}";
        }
    }
}
