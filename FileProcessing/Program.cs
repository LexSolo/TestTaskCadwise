using System;
using System.IO;

namespace FileProcessing
{
    class FileProcessing
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the program of performing the first test task by Aleksandr Solovyov.");
            Console.WriteLine("The program able to manipulate file you choose.");
            Console.WriteLine("The file can be read, written, also some words less than definite length can be deleted, such as a punctuation marks.");
            Console.WriteLine("After all manipulations the changes happened through the process save in the new file.");
            Console.WriteLine("So let's start!\n");

            bool flag = true; // flag for checking of program proceeding
            int symbolValue; // amount of symbols the words deleted after reaching
            int punctMark; // user's answer about deleting puctuation marks
            bool mark = true; // flag of the truth of deleting punctuation marks according to user's answer
            string file; // path to files for reading and writing information

            // block for entering maximum allowable amount of simbols in word
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter the maximum allowed word length please: ");
                    symbolValue = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Numbers allowed only.");
                }
            }

            // block for asking of necessity for deleting punctuation marks in the text
            while (flag)
            {
                try
                {
                    Console.WriteLine("Should the program removes punctuation marks (1 - yes, 0 - no) ? ");
                    punctMark = Convert.ToInt32(Console.ReadLine());
                    switch (punctMark)
                    {
                        case 1:
                            flag = false;
                            break;
                        case 0:
                            flag = false;
                            mark = false;
                            break;
                        default:
                            Console.WriteLine("Enter 1 of 0 please.");
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Numbers allowed only.");
                }
            }

            // block for checking of truth the file existing
            while (true)
            {
                try
                {
                    Console.WriteLine("Input full path to file please: ");
                    file = Console.ReadLine();
                    break;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("The file doesn't exist. Enter the correct file name please.");
                }
            }

            // I couldn't use the regular expressions, errors were everywhere, so I did it with the help of punctuation marks array

            string[] lines = File.ReadAllLines(file); // array for storage all lines from file
            char[] regex = {'-', '.', '?', '!', ')', '(', ',', ':', ';', '"'}; // array for punctuation marks to delete

            // block for deleting punctuation marks according to user's answer
            if (mark)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    var lineToCharArray = lines[i].ToCharArray(); // separate line from file to array of characters
                    for (int j = 0; j < lineToCharArray.Length; j++)
                    {
                        for (int k = 0; k < regex.Length; k++)
                        {
                            // if character from line equals character from regex array it changes to space and program goes to the next character in line else it misses
                            if (lineToCharArray[j].Equals(regex[k]))
                            {
                                lineToCharArray[j] = ' ';
                                break;
                            }
                            else
                                continue;
                        }
                    }
                    // transform new arrays of characters to sample string
                    lines[i] = new string(lineToCharArray);
                }
            }

            // block for deleting words less length symbols than definited
            for (int i = 0; i < lines.Length; i++)
            {
                string[] words = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); // dividing lines to words by space to array
                string[] newWords = new string[words.Length];
                for (int j = 0; j < words.Length; j++)
                {
                    if (words[j].Length < symbolValue)
                        newWords[j] = words[j].Replace(words[j], " ");
                    else
                        newWords[j] = words[j];
                }
                lines[i] = string.Join(" ", newWords);
            }

            // block for replacing multiple space by one
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("      "))
                    lines[i] = lines[i].Replace("      ", " ");
                if (lines[i].Contains("     "))
                    lines[i] = lines[i].Replace("     ", " ");
                if (lines[i].Contains("     "))
                    lines[i] = lines[i].Replace("     ", "");
                if (lines[i].Contains("    "))
                    lines[i] = lines[i].Replace("    ", " ");
                if (lines[i].Contains("   "))
                    lines[i] = lines[i].Replace("   ", " ");
                if (lines[i].Contains("  "))
                    lines[i] = lines[i].Replace("  ", " ");
            }

            // block for writing final text to file
            Console.WriteLine("Input full path to file where information will be written please: ");
            while (true)
            {
                try
                {
                    string newPath = Console.ReadLine();
                    File.WriteAllLines(newPath, lines);
                    Console.WriteLine("The information was written to " + newPath + " path.");
                    break;
                }
                catch (Exception)
                {
                }
            }
        } // Main()
    }
}
