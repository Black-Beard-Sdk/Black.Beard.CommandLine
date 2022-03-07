using System;

namespace Bb.CommandLines.Outs
{

    public static partial class Output
    {

        public static void PrepareProgressBar(decimal completed, int maxChar = 0)
        {

            if (maxChar == 0)
                maxChar = Console.WindowWidth;

            var l1 = "0".PadRight(maxChar - 4, ' ') + "100";
            Output.WriteLineStandard(l1);

            var o = (int)(((maxChar - 1) / 2));
            var l2 = "|".PadRight(o, ' ') + "|".PadRight(o, ' ') + "|";
            Output.WriteLineStandard(l2);

        }

        public static void ShowProgressBar(decimal value, decimal completed, int maxChar = 0)
        {

            if (maxChar == 0)
                maxChar = Console.WindowWidth;

            int currentValue = (int)Math.Round(((value / completed * 100) / 100) * maxChar);

            var l1 = "".PadRight(currentValue, CharsetProgressBarCompleted);
            var l2 = l1.PadRight(maxChar - 1, CharsetProgressBar);

            Output.WriteStandard(l2 + "\r");

        }

        public static char CharsetProgressBarCompleted { get; set; } = '█';


        public static char CharsetProgressBar { get; set; } = '░';


    }


}
