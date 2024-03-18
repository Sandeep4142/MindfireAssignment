using CRUDUsingDotNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CRUDUsingDotNetCore
{
    public class StudentDA
    {
        public static bool AddStudent(Student student)
        {
            try
            {
                using (var context = new SchoolContext())
                {               
                    context.Students.Add(student);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
                return false;
            }
        }

        public static bool DeleteStudent(int id)
        {
            try
            {
                using (var context = new SchoolContext())
                {
                    Student student = context.Students.FirstOrDefault(s => s.StudentId == id);
                    if (student != null) 
                    {
                        context.Students.Remove(student);
                        context.SaveChanges();
                        return true;
                    }
                    return false;                 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
                return false;
            }
        }

        public static bool UpdateStudentPhoneNo(int id, string phoneNo)
        {
            try
            {
                using (var context = new SchoolContext())
                {
                    Student student = context.Students.FirstOrDefault(s => s.StudentId == id);
                    if(student != null)
                    {
                        student.PhoneNo = phoneNo;
                        context.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
                return false;
            }
        }

        public static Student GetStudent(int id)
        {
            try
            {
                using (var context = new SchoolContext())
                {
                    Student student = context.Students.FirstOrDefault(s => s.StudentId == id);
                    return student;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
                return null;
            }
        }

        public static List<Student> GetAllStudent()
        {
            try
            {
                using(var context = new SchoolContext())
                {
                    List<Student> studentList  = context.Students.ToList();
                    return studentList;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
                return new List<Student>();
            }
        }

    }
}
