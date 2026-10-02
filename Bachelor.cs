using lab3_Students.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_3_students
{
    public class Bachelor : Student, IScholarshipCalculable, IRateable
    {
        private int creditsPerSemester;

        public int CreditsPerSemester
        {
            get { return creditsPerSemester; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Кредити не можуть бути від'ємними", nameof(value));
                creditsPerSemester = value;
            }
        }

        public Bachelor(string fullName, string faculty, int course, int creditsPerSemester)
            : base(fullName, faculty, course)
        {
            CreditsPerSemester = creditsPerSemester;
        }

        protected override void ValidateCourse(int value)
        {
            if (value < 1 || value > 4)
                throw new ArgumentException("Курс бакалавра повинен бути від 1 до 4", nameof(value));
        }

        public double GetAcademicRating()
        {
            return CalculateAverageGrade() + creditsPerSemester * 0.1;
        }

        public double CalculateScholarship(double baseAmount)
        {
            if (baseAmount < 0) throw new ArgumentException("Стипендія не може бути від'ємною");
            double avg = CalculateAverageGrade();
            if (avg <= 93) { return baseAmount; }
            else if (avg > 93) { return baseAmount * 1.4; }
            return 0.0;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $", Кредитів за семестр: {CreditsPerSemester}, " +
                   $"Академічний рейтинг: {GetAcademicRating():F2}";
        }
    }
}
