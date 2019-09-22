using System;
using System.IO;
using System.Text.RegularExpressions;

namespace C_Projects
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the program of performing the first test task by Aleksandr Solovyov.");
            Console.WriteLine("The program able to manipulate file you choose.");
            Console.WriteLine("The file can be read, written, also some words less than definite length can be deleted such as a punctuation marks.");
            Console.WriteLine("After all manipulations the changes happened through the process save in the new file.");
            Console.WriteLine("So let's start!");
            Console.WriteLine("\nWould you like to delete ");
            
            bool flag = true;
            int simbolValue;

            //input interface
            while (flag)
            {
                try
                {
                    simbolValue = Convert.ToInt32(Console.ReadLine());
                    flag = false;
                } catch (Exception)
                {
                    Console.WriteLine("Number allowed only. Try again please.");
                }
            }
            bool anotherFlag = true;

            while (anotherFlag)
            {
                try
                {
                    Console.WriteLine("Input full path to file please: ");
                    string file = Console.ReadLine();

                    string fileDir = Path.GetDirectoryName(file); // ?
                    string fileName = Path.GetFileName(file); // ?

                    string[] lines = File.ReadAllLines(file);

                    foreach (string line in lines)
                    {
                        var lineWithoutRegEx = Regex.Replace(line, "[-.?!)(,:;][\"/}{']", " ");

                        // may be bug due to StringSplitOptions.RemoveEmptyEntries
                        string[] words = lineWithoutRegEx.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string word in words)
                        {
                            if (word.Length < simbolValue)
                            {
                                word.Replace(word, "");
                            }
                        }
                    }
                    anotherFlag = false;
                } catch (Exception)
                {
                    Console.WriteLine("The file doesn't exist. Enter the correct file name please.");
                }
            }
            
        }
    }
}
