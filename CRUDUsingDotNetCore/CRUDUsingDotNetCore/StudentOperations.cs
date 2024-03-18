using CRUDUsingDotNetCore.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRUDUsingDotNetCore
{
    public class StudentOperations
    {
        public static void AddStudent()
        {
            try
            {
                Console.Write("Student ID: ");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("Class number: ");
                int classNo = Convert.ToInt32(Console.ReadLine());

                Console.Write("Phone number: ");
                string phoneNo = Console.ReadLine();

                Console.Write("Admission year: ");
                int admissionYear = Convert.ToInt32(Console.ReadLine());

                Student newStudent = new Student()
                {
                    StudentId = id,
                    Name = name,
                    PhoneNo = phoneNo,
                    ClassNo = classNo,
                    AdmissionYear = admissionYear,
                };
                
                bool response = StudentDA.AddStudent(newStudent);

                if(response == true)
                {
                    Console.WriteLine("New Student saved successfully !");
                }
                else 
                {
                    Console.WriteLine("Failed to save student !");
                }     
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteStudent() 
        {
            try
            {
                Console.Write("Enter student id : ");
                int id = Convert.ToInt32(Console.ReadLine());

                bool response = StudentDA.DeleteStudent(id);
                if (response == true)
                {
                    Console.WriteLine("Student removed successfully .");
                }
                else
                {
                    Console.WriteLine("Failed to remove student .");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }  

        public static void UpdateStudentPhoneNo() 
        {
            try
            {
                Console.Write("Enter student id : ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter new phone no : ");
                string phoneNo = Console.ReadLine();

                bool response = StudentDA.UpdateStudentPhoneNo(id, phoneNo);
                if (response == true)
                {
                    Console.WriteLine("Phone no updated successfully .");
                }
                else
                {
                    Console.WriteLine("Update Phone no failed .");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }  

        public static void GetStudent() 
        {
            try
            {
                Console.Write("Enter student id : ");
                int id = Convert.ToInt32(Console.ReadLine());
                Student student = StudentDA.GetStudent(id);

                if(student == null)
                {
                    Console.WriteLine("Student with id " + id + " not found !");
                }
                else
                {
                    Console.WriteLine("Name : " + student.Name +
                                  "\nClass : " + student.ClassNo +
                                  "\nPhone No : " + student.PhoneNo +
                                  "\nAdmission Year : " + student.AdmissionYear);
                }            
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void GetAllStudents()
        {
            List<Student> students = StudentDA.GetAllStudent();

            Console.WriteLine("Student Details :");

            Console.Write("StudentId".PadRight(10) + "  " +
                              "Name".PadRight(15) + "  " +
                              "ClassNo".PadRight(7) + "  " +
                              "Phone_no".PadRight(12) + "  " +
                              "Admission year".PadRight(7));
            Console.WriteLine();
            foreach (Student student in students)
            {
                Console.Write(student.StudentId.ToString().PadRight(10) + "  " +
                              student.Name.ToString().PadRight(15) + "  " +
                              student.ClassNo.ToString().PadRight(7) + "  " +
                              student.PhoneNo.ToString().PadRight(12) + "  " +
                              student.AdmissionYear.ToString().PadRight(7));
                Console.WriteLine();
            }
        }

    }
}
