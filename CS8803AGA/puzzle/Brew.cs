using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace CS8803AGA.puzzle
{
    /// <summary>
    /// Brews. It's what's for dinner.
    /// </summary>
    public class Brew: PuzzleObject
    {
        public const int COLOR_RED = 1;
        public const int COLOR_BLUE = 2;
        public const int COLOR_GREEN = 4;
        public const int COLOR_YELLOW = 8;
        public const int COLOR_ORANGE = 16;
        public const int COLOR_VIOLET = 32;
        public const int COLOR_WHITE = 64;
        public const int COLOR_BLACK = 128;
        public const int COLOR_BROWN = 256;

        private int color; /**< the color of the brew */
        private int uncolor; /**< the color from the keg, to remove */

        public static Color getTextColor(int c)
        {
            switch (c)
            {
                case COLOR_RED:     return Color.Red;
                case COLOR_BLUE:    return Color.Blue;
                case COLOR_GREEN:   return Color.Green;
                case COLOR_YELLOW:  return Color.Yellow;
                case COLOR_ORANGE:  return Color.Orange;
                case COLOR_VIOLET:  return Color.Violet;
                case COLOR_WHITE:   return Color.White;
                case COLOR_BLACK:   return Color.Black;
                case COLOR_BROWN:   return Color.Brown;
            }
            return Color.Black;
        }

        /**
         * Gets the color
         *@return color
         */
        public int getColor()
        {
            return color;
        }

        /**
         * Gets the uncoloring
         *@return uncolor
         */
        public int getUncolor()
        {
            return uncolor;
        }

        /**
         * Makes a new brew
         * @param _color color
         * @param _uncolor uncolor
         */
        public Brew(int _color, int _uncolor)
        {
            type = PuzzleObject.TYPE_BREW;
            color = _color;
            uncolor = _uncolor;
        }

        /**
         * Mixes this brew with another (yummy!)
         *@param other the one to combine with
         *@return false if other was already in this brew, otherwise true
         */
        public bool mix(Brew other)
        {
            // first must make sure to unmix the uncolor
            if (extract(other))
            {
                for (int i = 1; i <= other.color; i <<= 1)
                {
                    if ((i & other.color) != 0 && (i & color) == 0)
                    {
                        color |= other.color;
                        return true;
                    }
                }
            }
            return false;
        }

        /**
         * Extracts a brew from this one
         *@param other the one to extract
         *@return false if other was not in this
         */
        public bool extract(Brew other)
        {
            for (int i = 1; i <= other.uncolor; i <<= 1)
            {
                if ((i & other.uncolor) != 0 && (i & color) == 0)
                {
                    return false;
                }
            }
            color &= ~other.uncolor;
            return true;
        }

        /**
         * Determiens if two objects are the same
         *@param other the other object
         */
        public override bool equals(PuzzleObject other)
        {
            return (other.type == type && ((Brew)other).color == color && ((Brew)other).uncolor == uncolor);
        }

        /**
         * Debug print
         */
        public void debugPrint()
        {
            Console.Write("Brew: ");

            if ((color & COLOR_RED) != 0)
            {
                Console.Write("RED + ");
            }
            if ((color & COLOR_BLUE) != 0)
            {
                Console.Write("BLUE + ");
            }
            if ((color & COLOR_GREEN) != 0)
            {
                Console.Write("GREEN + ");
            }
            if ((color & COLOR_YELLOW) != 0)
            {
                Console.Write("YELLOW + ");
            }
            if ((color & COLOR_ORANGE) != 0)
            {
                Console.Write("ORANGE + ");
            }
            if ((color & COLOR_VIOLET) != 0)
            {
                Console.Write("VIOLET + ");
            }
            if ((color & COLOR_WHITE) != 0)
            {
                Console.Write("WHITE + ");
            }
            if ((color & COLOR_BLACK) != 0)
            {
                Console.Write("BLACK + ");
            }
            if ((color & COLOR_BROWN) != 0)
            {
                Console.Write("BROWN + ");
            }
            Console.WriteLine();
        }

        /**
         * Makes a copy of this
         * @return this
         */
        public override PuzzleObject copy()
        {
            return new Brew(color, uncolor);
        }
    }
}
