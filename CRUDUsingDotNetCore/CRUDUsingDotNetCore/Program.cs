using CRUDUsingDotNetCore;

int option;
do
{
    Console.WriteLine("\n---------------");
    Console.WriteLine("1. Add new student.");
    Console.WriteLine("2. Update student phone no");
    Console.WriteLine("3. Remove student");
    Console.WriteLine("4. Show student details");
    Console.WriteLine("5. Show all student details");
    Console.WriteLine("0. Exit ");
    Console.WriteLine("Select one option : ");
    option = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("---------------");

    switch (option)
    {
        case 1: StudentOperations.AddStudent(); break;
        case 2: StudentOperations.UpdateStudentPhoneNo(); break;
        case 3: StudentOperations.DeleteStudent(); break;
        case 4: StudentOperations.GetStudent(); break;
        case 5: StudentOperations.GetAllStudents(); break;
        default: Console.WriteLine("Invalid option."); break;
    }
} while (option != 0);
