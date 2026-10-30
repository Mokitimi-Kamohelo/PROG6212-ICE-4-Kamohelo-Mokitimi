using StudentLibrary;

var studentService = new StudentDataService();

bool running = true;

while (running)
{
    Console.Clear();

    Console.WriteLine("           STUDENT LIBRARY APPLICATION");
    Console.WriteLine("1. Display all students");
    Console.WriteLine("2. Find student by student number");
    Console.WriteLine("3. Filter students by programme");
    Console.WriteLine("4. Search students by name");
    Console.WriteLine("5. Display top five students");
    Console.WriteLine("6. Calculate programme average");
    Console.WriteLine("0. Exit");
    Console.Write("Enter your choice: ");

    string? choice = Console.ReadLine();

    Console.WriteLine();

    try
    {
        switch (choice)
        {
            case "1":
                DisplayAllStudents();
                break;

            case "2":
                FindStudent();
                break;

            case "3":
                FilterByProgramme();
                break;

            case "4":
                SearchStudents();
                break;

            case "5":
                DisplayTopFiveStudents();
                break;

            case "6":
                CalculateProgrammeAverage();
                break;

            case "0":
                running = false;
                Console.WriteLine("Thank you for using the Student Library Application.");
                continue;

            default:
                Console.WriteLine("Invalid menu choice. Please select an option from 0 to 6.");
                break;
        }
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Invalid input: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
    }

    Console.WriteLine();
    Console.WriteLine("Press any key to return to the menu...");
    Console.ReadKey();
}

void DisplayAllStudents()
{
    var students = studentService.GetAllStudents();

    Console.WriteLine("ALL STUDENTS");

    PrintStudents(students);
}

void FindStudent()
{
    Console.Write("Enter student number: ");
    string? studentNumber = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(studentNumber))
    {
        Console.WriteLine("Student number cannot be blank.");
        return;
    }

    var student = studentService.GetByStudentNumber(studentNumber.Trim());

    if (student is null)
    {
        Console.WriteLine($"No student found with student number '{studentNumber}'.");
        return;
    }

    Console.WriteLine("STUDENT DETAILS");

    PrintStudents(new[] { student });
}

void FilterByProgramme()
{
    Console.Write("Enter programme code (BCAD, BCA, or BIT): ");
    string? programme = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(programme))
    {
        Console.WriteLine("Programme code cannot be blank.");
        return;
    }

    var students = studentService.GetByProgramme(programme.Trim());

    if (students.Count == 0)
    {
        Console.WriteLine($"No students found for programme '{programme}'.");
        return;
    }

    Console.WriteLine($" STUDENTS IN {programme.Trim().ToUpperInvariant()} ");

    PrintStudents(students);
}

void SearchStudents()
{
    Console.Write("Enter a name or part of a name: ");
    string? searchText = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(searchText))
    {
        Console.WriteLine("Search text cannot be blank.");
        return;
    }

    var students = studentService.SearchByName(searchText.Trim());

    if (students.Count == 0)
    {
        Console.WriteLine($"No students found matching '{searchText}'.");
        return;
    }

    Console.WriteLine($" SEARCH RESULTS FOR '{searchText}");

    PrintStudents(students);
}

void DisplayTopFiveStudents()
{
    var students = studentService.GetTopStudents(5);

    Console.WriteLine("TOP FIVE STUDENTS");

    PrintStudents(students);
}

void CalculateProgrammeAverage()
{
    Console.Write("Enter programme code (BCAD, BCA, or BIT): ");
    string? programme = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(programme))
    {
        Console.WriteLine("Programme code cannot be blank.");
        return;
    }

    decimal average = studentService.GetProgrammeAverage(programme.Trim());

    Console.WriteLine($"Average mark for {programme.Trim().ToUpperInvariant()}: {average:F2}%");
}

void PrintStudents(IEnumerable<Student> students)
{
    Console.WriteLine(
        $"{"Student Number",-18} {"Full Name",-28} {"Programme",-12} {"Year",-6} {"Average",-10} {"Active",-8}");

    Console.WriteLine(new string('-', 88));

    foreach (var student in students)
    {
        Console.WriteLine(
            $"{student.StudentNumber,-18} " +
            $"{student.FullName,-28} " +
            $"{student.Programme,-12} " +
            $"{student.YearOfStudy,-6} " +
            $"{student.AverageMark,-10:F2} " +
            $"{(student.IsActive ? "Yes" : "No"),-8}");
    }
}