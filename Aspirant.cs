using lab3_Students.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_3_students
{
    public class Aspirant : Student, IRateable, IResearchable
    {
        private string thesisTopic;
        private int publicationsCount;
        private string scientificAdvisor;

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

        public string ScientificAdvisor
        {
            get { return scientificAdvisor; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("ПІБ керівника не може бути порожнім", nameof(value));
                }
                scientificAdvisor = value;
            }
        }
        public Aspirant(string fullName, string faculty, int course, string thesisTopic, int publicationsCount, string scientificAdvisor)
            : base(fullName, faculty, course)
        {
            ThesisTopic = thesisTopic;
            PublicationsCount = publicationsCount;
            ScientificAdvisor = scientificAdvisor;
        }

        protected override void ValidateCourse(int course)
        {
            if (course < 1 || course > 4)
                throw new System.ArgumentException("Курс аспіранта має бути від 1 до 4.");
        }

        public double GetAcademicRating()
        {
            return CalculateAverageGrade() + (PublicationsCount * 3.5);
        }

        public override string GetInfo()
        {
            return $"{base.GetInfo()}, Тема: {ThesisTopic}, Наук. керівник: {ScientificAdvisor}, Публікацій: {PublicationsCount}, Рейтинг: {GetAcademicRating():F2}";
        }
    }
}
