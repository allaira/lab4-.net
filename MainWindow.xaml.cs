using lab_3_students;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace lab_3_students
{
    public partial class MainWindow : Window
    {
        private readonly List<Student> _students = new List<Student>();

        public MainWindow()
        {
            InitializeComponent(); //малює вікно і створює всі елементи з XAML
            SetInitialUiState();   //налаштування видимості полів форми
        }

        //спрацьовує, коли користувач клацає на випадний список і обирає інший тип студента
        private void StudentTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetInitialUiState();
        }

        private void SetInitialUiState()
        {
            if (CreditsLabel == null) return;//якщо зміна вибору спрацює до ств полів бакалавра в пам'яті-виходимо

            int type = StudentTypeComboBox.SelectedIndex;

            CreditsLabel.Visibility = (type == 0) ? Visibility.Visible : Visibility.Collapsed;
            CreditsTextBox.Visibility = (type == 0) ? Visibility.Visible : Visibility.Collapsed;
            
            ThesisLabel.Visibility = (type == 1 || type == 2) ? Visibility.Visible : Visibility.Collapsed;
            ThesisTextBox.Visibility = (type == 1 || type == 2) ? Visibility.Visible : Visibility.Collapsed;
            PublicationsLabel.Visibility = (type == 1 || type == 2) ? Visibility.Visible : Visibility.Collapsed;
            PublicationsTextBox.Visibility = (type == 1 || type == 2) ? Visibility.Visible : Visibility.Collapsed;

            AdvisorLabel.Visibility = (type == 2) ? Visibility.Visible : Visibility.Collapsed;
            AdvisorTextBox.Visibility = (type == 2) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string fullName = FullNameTextBox.Text;
                string faculty = FacultyTextBox.Text;

                if (!int.TryParse(CourseTextBox.Text, out int course))
                {
                    MessageBox.Show("Введіть коректний номер курсу.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Student student;
                if (StudentTypeComboBox.SelectedIndex == 0)
                {
                    if (!int.TryParse(CreditsTextBox.Text, out int credits))
                    {
                        MessageBox.Show("Введіть коректну кількість кредитів.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    student = new Bachelor(fullName, faculty, course, credits);
                }
                else if(StudentTypeComboBox.SelectedIndex == 1)
                {
                    string topic = ThesisTextBox.Text;
                    if (!int.TryParse(PublicationsTextBox.Text, out int pubs))
                    {
                        MessageBox.Show("Введіть коректну кількість публікацій.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    student = new Master(fullName, faculty, course, topic, pubs);
                }
                else
                {
                    string topic = ThesisTextBox.Text;
                    string advisor = AdvisorTextBox.Text;
                    if (!int.TryParse(PublicationsTextBox.Text, out int pubs))
                    {
                        MessageBox.Show("Введіть коректну кількість публікацій.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    student = new Aspirant(fullName, faculty, course, topic, pubs, advisor);
                }

                if (!string.IsNullOrWhiteSpace(GradesTextBox.Text))
                {
                    var grades = GradesTextBox.Text.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var g in grades)
                    {
                        if (int.TryParse(g, out int val))
                            student.AddGrade(val);
                        else
                        {
                            MessageBox.Show($"Некоректна оцінка: {g}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                }

                _students.Add(student);
                StudentsListBox.Items.Add(student.GetInfo());
                UpdateAnalytics();

                FullNameTextBox.Clear();
                FacultyTextBox.Clear();
                CourseTextBox.Clear();
                CreditsTextBox.Clear();
                ThesisTextBox.Clear();
                PublicationsTextBox.Clear();
                GradesTextBox.Clear();
            }
            catch (Exception ex) //виводимо текст помилки, яку викинули класи
            {
                MessageBox.Show(ex.Message, "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateAnalytics()
        {
            var top = ListOfStudents.GetTopStudent(_students);
            if (top != null)
                TopStudentTextBlock.Text = $"{top.FullName} ({top.Faculty}, Курс {top.Course}) — Сер. бал: {top.CalculateAverageGrade():F2}";
            else
                TopStudentTextBlock.Text = "—";

            var scholarshipEligible = _students.OfType<IScholarshipCalculable>();
            double budget = ListOfStudents.GetTotalScholarshipBudget(scholarshipEligible, 2000.0);
            ScholarshipBudgetTextBlock.Text = $"{budget:F2} грн";
        }
    }
}