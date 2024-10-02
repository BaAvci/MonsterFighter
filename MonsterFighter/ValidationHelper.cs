using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MonsterFighter
{
    public abstract class ValidationHelper
    {
        /// <summary>
        /// Checks the value if it's applyable to a parameter.
        /// </summary>
        /// <param name="displayText">The text that should be displayed if the value is not good</param>
        /// <returns></returns>
        public static float ValueInputCheck(string displayText)
        {
            var inputValue = -1f;
            while (inputValue < 0)
            {
                if (float.TryParse(Console.ReadLine(), out float input) && input > 0)
                {
                    return input;
                }
                Console.WriteLine(displayText);
            }
            return inputValue;
        }

        public static int NumberCheck(int minValue = 0)
        {
            var number = 0;
            var exit = false;
            while (!exit)
            {
                if (int.TryParse(Console.ReadLine(), out int output) && output >= minValue)
                {
                    number = output;
                    exit = !exit;
                }
                else
                {
                    Console.WriteLine($"Bitte geben Sie eine ganze Zahl ein die grösser als {minValue} ist.");
                }
            }

            return number;
        }
        public static bool YesNoCheck()
        {
            var yesNo = false;
            bool exit;
            exit = false;
            while (!exit)
            {
                if (int.TryParse(Console.ReadLine(), out int output) && output == 1)
                {
                    yesNo = true;
                    exit = !exit;
                }
                else if (output == 2)
                {
                    yesNo = false;
                    exit = !exit;
                }
                else
                {
                    Console.WriteLine("Bitte geben Sie entweder 1 oder 2 ein!");
                }
            }
            return yesNo;
        }

        public static char CharInputCheck(List<char> viableInputs)
        {
            var value = '\0';
            var exit = false;
            while (!exit)
            {
                var input = Console.ReadKey().KeyChar;
                var check = viableInputs.Any(v => v == input);
                if (check)
                {
                    value = input;
                    exit = !exit;
                }
                Console.WriteLine("Bitte geben Sie einen validen Buchstaben ein.");
            }
            return value;
        }
        public static int CheckValueBetween(int minValue,int maxValue)
        {
            var number = 0;
            var exit = false;
            while (!exit)
            {
                if (int.TryParse(Console.ReadLine(), out int output) && output >= minValue && output <= maxValue)
                {
                    number = output;
                    exit = !exit;
                }
                else
                {
                    Console.WriteLine($"Bitte geben Sie eine ganze Zahl ein die zwischen {minValue} und {maxValue} liegt.");
                }
            }
            return number;
        }
    }
}
