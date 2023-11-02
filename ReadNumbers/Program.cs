/* Reading Numbers in Words */

using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;


namespace ReadNumbers
{
    class Program
    {
        public static string Magnitude(int x)
        {
            if (x == 0) return "";
            else if (x == 1) return "thousand ";
            else if (x == 2) return "million ";
            else if (x == 3) return "billion ";
            else if (x == 4) return "trillion ";
            else if (x == 5) return "quadrillion ";
            else if (x == 6) return "quintillion ";
            else if (x == 7) return "sextillion ";
            else if (x == 8) return "septillion ";
            else if (x == 9) return "octillion ";
            else if (x == 10) return "nonillion ";
            else return "";
        }

        public static string Digit(char x)
        {
            if (x == '0') return "";
            else if (x == '1') return "one ";
            else if (x == '2') return "two ";
            else if (x == '3') return "three ";
            else if (x == '4') return "four ";
            else if (x == '5') return "five ";
            else if (x == '6') return "six ";
            else if (x == '7') return "seven ";
            else if (x == '8') return "eight ";
            else if (x == '9') return "nine ";
            else return "";
        }

        public static string Writer(char x, int r)
        {
            string back = "";
            if (x == '0') return back;

            else if (r == 2)
            {
                back += Digit(x) + "hundred ";
            }

            else if (r == 1)
            {
                if (x == '1') back += "ten ";
                else if (x == '2') back += "twenty ";
                else if (x == '3') back += "thirty ";
                else if (x == '4') back += "fourty ";
                else if (x == '5') back += "fifty ";
                else if (x == '6') back += "sixty ";
                else if (x == '7') back += "seventy ";
                else if (x == '8') back += "eighty ";
                else if (x == '9') back += "ninety ";
            }

            else back += Digit(x);

            return back;
        }


        public static string ReadRoot(string a)
        {
            string written = "";

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == '0')    // Example: For 025.000, You can Eliminate the First 0 and Move on with 25.000
                    continue;

                else if (a.Length - (i + 1) == 1 && a[i] == '1' && a[i + 1] != '0')
                {
                    if (a[i + 1] == '1') written += "eleven ";
                    else if (a[i + 1] == '2') written += "twelve ";
                    else if (a[i + 1] == '3') written += "thirteen ";
                    else if (a[i + 1] == '4') written += "fourteen ";
                    else if (a[i + 1] == '5') written += "fifteen ";
                    else if (a[i + 1] == '6') written += "sixteen ";
                    else if (a[i + 1] == '7') written += "seventeen ";
                    else if (a[i + 1] == '8') written += "eighteen ";
                    else if (a[i + 1] == '9') written += "nineteen ";
                    return written;
                }
                else
                {
                    written += Writer(a[i], a.Length - (i + 1));
                    if (a.Length - (i + 1) == 1 && a[i] != '1' && a[i + 1] != '0')
                    {
                        written = written.Substring(0, written.Length - 1);
                        written += "-";
                    }
                }
            }
            return written;
        }


        static void Main(string[] args)
        {
        Start:
            Console.WriteLine("\nCorrect Examples: 143 || 54.67 || -246 || -23.56:");
            Console.Write("Enter Your Number: ");
            string fnumber = Console.ReadLine();
            if (fnumber == null || fnumber == "")
            {
                Console.WriteLine("Your Input Format is Not Correct!");
                goto Start;
            }

            int comma = fnumber.Count(x => x == '.');

            bool num = true;
            for (int a = 0; a < fnumber.Length; a++)
            {                                       // This Loop Prevents Such Inputs:
                if (((a == 0 || a == fnumber.Length - 1) && fnumber[a] == '.') ||           // ,563 || 45,          (Comma Before or After the Number)
                       (a != 0 && fnumber[a] == '-') ||                                     // 324-7320 || 343,11-  (Minus After Any Figures)
                       (comma > 1) ||                                                       // -53,102,40           (More than One Comma)
                       (fnumber[a] != '-' && fnumber[a] != '0' && fnumber[a] != '1' &&      // Containing Any Characters Other than { '-', '.', Numbers(0-9) }
                        fnumber[a] != '2' && fnumber[a] != '3' && fnumber[a] != '4' &&
                        fnumber[a] != '5' && fnumber[a] != '6' && fnumber[a] != '7' &&
                        fnumber[a] != '8' && fnumber[a] != '9' && fnumber[a] != '.'))
                {
                    num = false;
                    break;
                }
            }

            if (!num)
            {
                Console.WriteLine("Your Input Format is Not Correct!");
                goto Start;
            }
            Console.WriteLine();

            //////////////////////////////////////////////////

            string readnumber = "";

            if (fnumber[0] == '-')
            {
                //Console.Write("minus ");
                readnumber += "minus ";             // Get the "minus" Info If Exists
                fnumber = fnumber.Split('-')[1];    // Now Start Reading the Number
            }

            string[] parts = fnumber.Split('.');    // If It is A Decimal Number, Divide It into 2 Parts

            for (int k = 0; k < parts.Length; k++)  // For Each Part, Extract Reading Words of the Numbers
            {
                string number = parts[k];           // The Current Number Part Before or After the Point
                string new_number = "";             // Forming A New String for Each Number Part to Divide Those Numbers with ',' Char

                for (int i = 0; i < number.Length; i++) // For Each Index of This Number Piece
                {
                    new_number += number[i];            // Copy the Number Figures
                    if ((number.Length - (i + 1)) % 3 == 0 && (i != number.Length - 1))
                    {
                        new_number += ",";  // Divide the Whole Number with Commas when There are Digits at the Right Side as Much as 3x
                    }
                }

                // Example: 1129034 >> 1,129,034    ||   29122 >> 29,122


                string[] split_number = new_number.Split(',');                  // Now Split the Divided Number and Obtain A String Array from Them

                for (int i = 0; i < split_number.Length; i++)                   // Reading Each Number Part. Example: 246,112,000 >> ["246", "112", "000"]
                {
                    //Console.Write(ReadRoot(split_number[i]));
                    readnumber += ReadRoot(split_number[i]);                    // 246 -> "two hundred fourty-six"...
                    //Console.Write(Magnitude(split_number.Length - (i + 1)));
                    readnumber += Magnitude(split_number.Length - (i + 1));     // ...112,000 (2x3 Digits Remained) -> "million"...
                }                                                               // ...112 -> "one hundred twelve"
                                                                                // ...000 (1x3 Digits Remained) -> "thousand"

                if (parts.Length != 1 && k != parts.Length - 1)
                {
                    //Console.Write("point ");
                    readnumber += "point ";                                     // Get the "point" Info If Exists
                }
            }



            //Console.WriteLine();
            Console.WriteLine(readnumber);

            Console.WriteLine("\n\n-------------");
            Console.Write("Press any key to continue . . .");
            Console.ReadKey();
            return;
        }
    }
}
