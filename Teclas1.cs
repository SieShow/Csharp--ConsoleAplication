using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP_Resolução
{
    class Program
    {
        static string PassMask()
        {
            string Pass ="";
            ConsoleKeyInfo info = Console.ReadKey(true);
     
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    Pass += info.Key;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(Pass))
                    {
                        int pos = Console.CursorLeft;

                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Pass = Pass.Substring(0, Pass.Length - 1);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            return Pass;
        }
        static void Main(string[] args)
        {
            //testing password mask
            string password;
            Console.Write("Enter the password: ");
            password = PassMask();

            Console.Write("\n\n" + password);
            Console.ReadKey();

        }
    }
}
