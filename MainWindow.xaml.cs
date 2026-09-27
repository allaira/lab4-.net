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
            InitializeComponent();
            SetInitialUiState();
        }

        private void StudentTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetInitialUiState();
        }

        private void SetInitialUiState()
        {
            if (CreditsLabel == null) return;

            bool isBachelor = StudentTypeComboBox.SelectedIndex == 0;

            CreditsLabel.Visibility = isBachelor ? Visibility.Visible : Visibility.Collapsed;
            CreditsTextBox.Visibility = isBachelor ? Visibility.Visible : Visibility.Collapsed;

            ThesisLabel.Visibility = isBachelor ? Visibility.Collapsed : Visibility.Visible;
            ThesisTextBox.Visibility = isBachelor ? Visibility.Collapsed : Visibility.Visible;
            PublicationsLabel.Visibility = isBachelor ? Visibility.Collapsed : Visibility.Visible;
            PublicationsTextBox.Visibility = isBachelor ? Visibility.Collapsed : Visibility.Visible;
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
                else
                {
                    string topic = ThesisTextBox.Text;
                    if (!int.TryParse(PublicationsTextBox.Text, out int pubs))
                    {
                        MessageBox.Show("Введіть коректну кількість публікацій.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    student = new Master(fullName, faculty, course, topic, pubs);
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
            catch (Exception ex)
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
        }
    }
}