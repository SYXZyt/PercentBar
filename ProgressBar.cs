﻿using System;
using System.Collections.Generic;

namespace PercentBar
{
    struct ConsolePos
    {
        public int x;
        public int y;

        /// <summary>
        /// Create an object to store screen coordinates
        /// </summary>
        /// <param name="x">X Position (Horizontal)</param>
        /// <param name="y">Y Position (Vertical)</param>
        public ConsolePos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    struct Style
    {
        public char BackgroundEmpty { get; private set; }
        public char BackgroundFilled { get; private set; }
        public bool ShowPercent { get; private set; }
        public ConsoleColor ForegroundColourFilled { get; private set; }
        public ConsoleColor ForegroundColourEmpty { get; private set; }
        public ConsoleColor BackgroundColourFilled { get; private set; }
        public ConsoleColor BackgroundColourEmpty { get; private set; }

        /// <summary>
        /// Create a style that can be used by a progress bar
        /// </summary>
        /// <param name="backgroundEmpty">What character is used for the empty spaces</param>
        /// <param name="backgroundFilled">What character is used for the filled spaces</param>
        /// <param name="showPercent">Should the percentage as a number be drawn?</param>
        /// <param name="foregroundColourFilled">Colour is the filled tile</param>
        /// <param name="foregroundColourEmpty">Colour of the empty tile</param>
        /// <param name="backgroundColourFilled">Background colour of filled tile</param>
        /// <param name="backgroundColourEmpty">Background colour of empty tile</param>
        public Style(char backgroundEmpty, char backgroundFilled, bool showPercent, ConsoleColor foregroundColourFilled, ConsoleColor foregroundColourEmpty, ConsoleColor backgroundColourFilled, ConsoleColor backgroundColourEmpty)
        {
            BackgroundEmpty = backgroundEmpty;
            BackgroundFilled = backgroundFilled;
            ShowPercent = showPercent;
            ForegroundColourFilled = foregroundColourFilled;
            ForegroundColourEmpty = foregroundColourEmpty;
            BackgroundColourFilled = backgroundColourFilled;
            BackgroundColourEmpty = backgroundColourEmpty;
        }
    }

    class ProgressBar
    {
        private readonly float maxValue;
        private readonly ConsolePos position;
        private readonly byte length;

        /// <summary>
        /// What style this progress bar uses
        /// </summary>
        public Style Style { get; protected set; }
        /// <summary>
        /// Current percentage of the progress bar
        /// </summary>
        public float Percentage { get; protected set; }

        /// <summary>
        /// Set the colours of the console
        /// </summary>
        /// <param name="colour1">Foreground colour</param>
        /// <param name="colour2">Background colour</param>
        private void SetColour(ConsoleColor colour1, ConsoleColor colour2)
        {
            Console.ForegroundColor = colour1;
            Console.BackgroundColor = colour2;
        }

        /// <summary>
        /// Forces the percentage to a specific value
        /// </summary>
        /// <param name="percentage">The value to set as the percentage. Note that this value will be rounded</param>
        public void SetPercentage(float percentage)
        {
            Percentage = percentage;
        }

        /// <summary>
        /// Updates the percentage of the progress bar
        /// </summary>
        /// <param name="value">The new value to be calculated into the percentage. This is not setting the percentage. Use SetPercentage for that</param>
        public void Update(float value)
        {
            Percentage = (value / maxValue) * 100f;
        }

        /// <summary>
        /// Draws the progress bar to the screen
        /// </summary>
        public void Draw()
        {
            ConsoleColor frg = Console.ForegroundColor;
            ConsoleColor bkg = Console.BackgroundColor;

            Console.SetCursorPosition(position.x, position.y);
            Console.Write('[');
            int progress = (int)(Percentage / length);

            //Set the colour and draw the progress chars
            SetColour(Style.ForegroundColourFilled, Style.BackgroundColourFilled);
            for (int u = 0; u < progress; u++)
            {
                Console.Write(Style.BackgroundFilled);
            }

            //Draw unfilled chars
            SetColour(Style.ForegroundColourEmpty, Style.BackgroundColourEmpty);
            for (int f = progress; f < length; f++)
            {
                Console.Write(Style.BackgroundEmpty);
            }

            //Set back to original colour
            SetColour(frg, bkg);

            Console.Write(']');

            if (Style.ShowPercent)
            {
                Console.WriteLine($" {(int)Percentage}%");
            }
        }

       /// <summary>
        /// Creates a new progress bar object
        /// </summary>
        /// <param name="maxValue">The maximum that the input value can reach. Note that this is NOT the max percentage. E.g. If counting every pixel horizontally on the screen, this could be 1080</param>
        /// <param name="x">X position on the screen</param>
        /// <param name="y">Y position on the screen</param>
        /// <param name="style">What style is this progress bar using</param>
        /// <param name="length">How wide is the progress bar. How many characters is it made up of. Note that the larger this is, the longer it will take to draw, and therefore slow down your application if too wide</param>
        public ProgressBar(float maxValue, int x, int y, Style style)
        {
            this.maxValue = maxValue;
            position = new ConsolePos(x, y);
            Style = style;
            length = 10; //This is kinda broken, currently anything other than 10 breaks the bar. Idk how to fix this, I really want a variable size bar
        }
    }
}

namespace PercentBar.Styles
{
    static class BuiltInStyles
    {
        public static Style Style1 { get; } = new Style('\u2591', '\u2588', true, ConsoleColor.White, ConsoleColor.Gray, ConsoleColor.Black, ConsoleColor.Black);
        public static Style Style2 { get; } = new Style(' ', '\u2588', true, ConsoleColor.White, ConsoleColor.Gray, ConsoleColor.Black, ConsoleColor.Black);
        public static Style Style3 { get; } = new Style('.', '#', true, ConsoleColor.White, ConsoleColor.Gray, ConsoleColor.Black, ConsoleColor.Black);
    }
}