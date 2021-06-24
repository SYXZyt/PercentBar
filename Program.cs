using System;
using PercentBar;
using PercentBar.Styles;

namespace Demo
{
    class Program
    {
        static void Main()
        {
            Console.CursorVisible = false;

            float target = 9000f;
            Console.WriteLine("Progress Bar:\n");
            ProgressBar progressBar = new ProgressBar(target, Console.CursorLeft, Console.CursorTop, BuiltInStyles.Style1);

            for (int i = 0; i < target + 1; i++)
            {
                progressBar.Update(i);
                progressBar.Draw();
            }
        }
    }
}
