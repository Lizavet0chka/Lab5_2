using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Text;

namespace ConsoleApplication2
{
    internal class Program
    {
        internal struct Student
        {
            public string surName;
            public string firstName;
            public string patronymic;
            public char sex;
            public string dateOfBirth;
            public char mathematicsMark;
            public char physicsMark;
            public char informaticsMark;
            public int scholarship;

            public Student(string str)
            {
                string[] student = Regex.Split(str, @" ");
                surName = student[0];
                firstName = student[1];
                patronymic = student[2];
                sex = Convert.ToChar(student[3]);
                dateOfBirth = student[4];
                mathematicsMark = Convert.ToChar(student[5]);
                physicsMark = Convert.ToChar(student[6]);
                informaticsMark = Convert.ToChar(student[7]);
                scholarship = Convert.ToInt32(student[8]);
            }

            public override string ToString() => $"{surName} {firstName} {patronymic} {sex} {dateOfBirth} {mathematicsMark} {physicsMark} {informaticsMark} {scholarship}";
        }
        public static List<Student> ReadData()
        {
            FileStream fs = File.Open(@"data.txt", FileMode.Open);
            List<Student> st = new List<Student>();
            using (var reader = new StreamReader(fs))
            {
                while (reader.Peek()>=0)
                {
                    string str = reader.ReadLine()??string.Empty;
                    Student student = new Student(str);
                    st.Add(student);
                }
            }
            return st;
        }
        public static void SaveData(List<Student> students)
        {
            string fileName=Assembly.GetExecutingAssembly().Location+".txt";
            using (var StringWriter = new StreamWriter(fileName))
            {
                for (int i = 0; i < students.Count; i++)
                {
                    StringWriter.WriteLine(students[i]);
                }
            }
        }

        public static List<Student> var_5(List<Student> allSt)
        {
            List<Student> result = new List<Student>();
            for (int i = 0; i < allSt.Count; i++)
            {
                Student oneSt = allSt[i];
                if (oneSt.mathematicsMark == '-')
                {
                    oneSt.mathematicsMark = '2';
                }
                if (oneSt.physicsMark == '-')
                {
                    oneSt.physicsMark = '2';
                }
                if (oneSt.informaticsMark == '-')
                {
                    oneSt.informaticsMark = '2';
                }
                double mathematik = Char.GetNumericValue(oneSt.mathematicsMark);
                double physik = Char.GetNumericValue(oneSt.physicsMark);
                double informatik = Char.GetNumericValue(oneSt.informaticsMark);
                if (mathematik < 3 || physik < 3 || informatik<3)
                {
                    oneSt.scholarship = 0;
                    Console.WriteLine($"{oneSt.surName} {oneSt.mathematicsMark} {oneSt.physicsMark} {oneSt.informaticsMark}");
                }
                result.Add(oneSt);
            }
            return result;
        }
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            List<Student> students = ReadData();
            SaveData(var_5(students));
            Console.ReadKey();
        }
    }
}