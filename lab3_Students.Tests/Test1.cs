namespace lab_3_students
{
    [TestClass]
    public sealed class Test1
    {
        // Бакалавр

        [TestMethod]
        public void Bachelor_Constructor()
        {
            var bachelor = new Bachelor("Іваненко Іван", "ФІТ", 3, 30);

            Assert.AreEqual("Іваненко Іван", bachelor.FullName);
            Assert.AreEqual("ФІТ", bachelor.Faculty);
            Assert.AreEqual(3, bachelor.Course);
            Assert.AreEqual(30, bachelor.CreditsPerSemester);
        }

        [TestMethod]
        public void Bachelor_Constructor_WithEmptyName()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Bachelor("", "ФІТ", 3, 30);
            });
        }

        [TestMethod]
        public void Bachelor_Constructor_WithEmptyFaculty()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Bachelor("Іваненко Іван", "", 3, 30);
            });
        }

        [TestMethod]
        public void Bachelor_Course0_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
                new Bachelor("Іван", "ФІТ", 0, 30));
        }

        [TestMethod]
        public void Bachelor_Constructor_WithInvalidCourse()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Bachelor("Іваненко Іван", "ФІТ", 7, 30);
            });
        }

        [TestMethod]
        public void Bachelor_Course5_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
                new Bachelor("Іван", "ФІТ", 5, 30));
        }

        [TestMethod]
        public void Bachelor_Constructor_WithNegativeCredits()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Bachelor("Іваненко Іван", "ФІТ", 3, -5);
            });
        }

        //Магістр

        [TestMethod]
        public void Master_Constructor_WithValidData_InitializesCorrectly()
        {
            var master = new Master("Петренко Петро", "ФІТ", 5, "Тема", 3);

            Assert.AreEqual("Петренко Петро", master.FullName);
            Assert.AreEqual("Тема", master.ThesisTopic);
            Assert.AreEqual(3, master.PublicationsCount);
        }
        [TestMethod]
        public void Master_Constructor_WithEmptyName_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Master("", "ФІТ", 5, "Тема", 3);
            });
        }

        [TestMethod]
        public void Master_Constructor_WithEmptyFaculty_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Master("Петренко Петро", "", 5, "Тема", 3);
            });
        }

        [TestMethod]
        public void Master_Constructor_WithInvalidCourse_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Master("Петренко Петро", "ФІТ", 0, "Тема", 3);
            });
        }
        
        [TestMethod]
        public void Master_Course4_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
                new Master("Петро", "ФІТ", 4, "Тема", 3));
        }
        
        [TestMethod]
        public void Master_Course7_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
                new Master("Петро", "ФІТ", 7, "Тема", 3));
        }
        
        [TestMethod]
        public void Master_Constructor_WithEmptyThesis()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Master("Петренко Петро", "ФІТ", 5, "", 3);
            });
        }

        [TestMethod]
        public void Master_Constructor_WithNegativePublications()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Master("Петренко Петро", "ФІТ", 5, "Тема", -1);
            });
        }

        // AddGrade


        [TestMethod]
        public void AddGrade_ValidGrade_AddsToGrades()
        {
            var bachelor = new Bachelor("Іваненко Іван", "ФІТ", 3, 30);
            bachelor.AddGrade(85);
            bachelor.AddGrade(90);

            Assert.AreEqual(2, bachelor.Grades.Count());
            Assert.AreEqual(85, bachelor.Grades[0]);
            Assert.AreEqual(90, bachelor.Grades[1]);
        }

        [TestMethod]
        public void AddGrade_InvalidGrade_ThrowsArgumentException()
        {
            var bachelor = new Bachelor("Іваненко Іван", "ФІТ", 3, 30);

            Assert.Throws<ArgumentException>(() =>
            {
                bachelor.AddGrade(150);
            });
        }

        [TestMethod]
        public void AddGrade_InvalidGradeWithMinus_ThrowsArgumentException()
        {
            var bachelor = new Bachelor("Іваненко Іван", "ФІТ", 3, 30);

            Assert.Throws<ArgumentException>(() =>
            {
                bachelor.AddGrade(-5);
            });
        }

        [TestMethod]
        public void AddGrade_Zero_IsValid()
        {
            var b = new Bachelor("Іван", "ФІТ", 3, 30);
            b.AddGrade(0);
            Assert.AreEqual(1, b.Grades.Count());
            Assert.AreEqual(0, b.Grades[0]);
        }


        //  CalculateAverageGrade

        [TestMethod]
        public void CalculateAverageGrade_ReturnsCorrectValue()
        {
            var bachelor = new Bachelor("Іваненко Іван", "ФІТ", 3, 30);
            bachelor.AddGrade(80);
            bachelor.AddGrade(90);
            bachelor.AddGrade(100);

            double avg = bachelor.CalculateAverageGrade();
            Assert.AreEqual(90.0, avg, 0.001);
        }

        [TestMethod]
        public void CalculateAverageGrade_EmptyGrades_ReturnsZero()
        {
            var bachelor = new Bachelor("Іваненко Іван", "ФІТ", 3, 30);
            double avg = bachelor.CalculateAverageGrade();
            Assert.AreEqual(0.0, avg, 0.001);
        }

        // GetAcademicRating

        [TestMethod]
        public void Bachelor_GetAcademicRating_ReturnsCorrectValue()
        {
            var bachelor = new Bachelor("Іваненко Іван", "ФІТ", 3, 50);
            bachelor.AddGrade(80);
            bachelor.AddGrade(90);

            double rating = bachelor.GetAcademicRating();
            Assert.AreEqual(90, rating, 0.001);
        }

        [TestMethod]
        public void Master_GetAcademicRating_ReturnsCorrectValue()
        {
            var master = new Master("Петренко Петро", "ФІТ", 5, "ШІ в освіті", 3);
            master.AddGrade(90);
            master.AddGrade(95);

            double rating = master.GetAcademicRating();
            Assert.AreEqual(98.5, rating, 0.001);
        }

        // getInfo

        [TestMethod]
        public void GetInfo_BachelorAndMaster_ReturnDifferentStrings()
        {
            var bachelor = new Bachelor("Іван", "ФІТ", 3, 30);
            bachelor.AddGrade(90);

            var master = new Master("Петро", "ФІТ", 5, "Тема", 3);
            master.AddGrade(90);

            string bachelorInfo = bachelor.GetInfo();
            string masterInfo = master.GetInfo();

            Assert.AreNotEqual(bachelorInfo, masterInfo);
            StringAssert.Contains(bachelorInfo, "Кредитів за семестр");
            StringAssert.Contains(masterInfo, "Публікацій");
        }

        //  ListOfStudents.GetTopStudent


        [TestMethod]
        public void GetTopStudent_WithMixedStudents_ReturnsTopStudent()
        {
            var students = new List<Student>
            {
                new Bachelor("Студент1", "ФІТ", 3, 30),
                new Bachelor("Студент2", "ФІТ", 3, 30),
                new Master("Студент3", "ФІТ", 5, "Тема", 2)
            };

            students[0].AddGrade(70);
            students[0].AddGrade(90);

            students[1].AddGrade(95);
            students[1].AddGrade(85);

            students[2].AddGrade(80);
            students[2].AddGrade(85);

            var top = ListOfStudents.GetTopStudent(students);

            Assert.AreEqual("Студент2", top.FullName);
        }

        [TestMethod]
        public void GetTopStudent_EmptyList_ReturnsNull()
        {
            var students = new List<Student>();
            var top = ListOfStudents.GetTopStudent(students);
            Assert.IsNull(top);
        }

        [TestMethod]
        public void GetTopStudent_OneStudent_ReturnsThatStudent()
        {
            var students = new List<Student>
            {
                new Bachelor("Єдиний", "ФІТ", 3, 30)
            };
            students[0].AddGrade(85);

            var top = ListOfStudents.GetTopStudent(students);
            Assert.AreEqual("Єдиний", top.FullName);
        }
        [TestMethod]
        
        public void Aspirant_Constructor_InitializesCorrectly()
        {
            var aspirant = new Aspirant("Сидоренко", "ФІТ", 2, "AI Models", 4, "Проф. Бондар");
            Assert.AreEqual("Сидоренко", aspirant.FullName);
            Assert.AreEqual("AI Models", aspirant.ThesisTopic);
            Assert.AreEqual(4, aspirant.PublicationsCount);
            Assert.AreEqual("Проф. Бондар", aspirant.ScientificAdvisor);
        }

        [TestMethod]
        public void Aspirant_InvalidCourse_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Aspirant("Сидоренко", "ФІТ", 5, "Тема", 1, "Керівник"));
        }

        [TestMethod]
        public void Bachelor_CalculateScholarship_HighGrade_ReturnsIncreasedScholarship()
        {
            var b = new Bachelor("Тест", "ФІТ", 2, 30);
            b.AddGrade(95);
            b.AddGrade(95);
            double scholarship = b.CalculateScholarship(2000.0);
            Assert.AreEqual(2800.0, scholarship, 0.01);
        }

        [TestMethod]
        public void Master_CalculateScholarship_WithPublications()
        {
            var m = new Master("Тест", "ФІТ", 5, "Тема", 2);
            m.AddGrade(92);
            double scholarship = m.CalculateScholarship(2000.0);
            Assert.AreEqual(2300.0, scholarship, 0.01);
        }

        [TestMethod]
        public void ListOfStudents_GetTotalScholarshipBudget_DemonstratesInterfacePolymorphism()
        {
            var list = new List<IScholarshipCalculable>();

            var b = new Bachelor("Бакалавр", "ФІТ", 2, 30);
            b.AddGrade(80);
            list.Add(b);

            var m = new Master("Магістр", "ФІТ", 5, "Тема", 0);
            m.AddGrade(95);
            list.Add(m);

            double total = ListOfStudents.GetTotalScholarshipBudget(list, 2000.0);
            Assert.AreEqual(4800.0, total, 0.01);
        }
    }
}
