using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CS8803AGA.engine;

namespace CS8803AGA.puzzle
{
    public class Bouncer: PuzzleObject
    {
        public const int PATH_UP = -1;
        public const int PATH_DOWN = 1;
        public const int PATH_LEFT = -2;
        public const int PATH_RIGHT = 2;

        private int color; /**< the color of this bouncer */
        private List<int> path; /**< the path this bouncer alternates between */
        private bool atPathStart; /**< whether or ont at the start of the path */

        /**
         * Makes a new bouncer, beware
         */
        public Bouncer(int _color, List<int> _path)
        {
            type = PuzzleObject.TYPE_BOUNCER;
            color = _color;
            path = new List<int>();
            path.AddRange(_path);
            atPathStart = true;

            id = next_id++;
        }

        /**
         * getColor()
         */
        public int getColor()
        {
            return color;
        }

        /**
         * get path length
         * @return path.size()
         */
        public int getPathSize()
        {
            return path.Count;
        }

        /**
         * switchDirection()
         */
        public void switchDirection()
        {
            atPathStart = !atPathStart;
        }

        /**
         * Gets path index
         *@param index
         */
        public int getPath(int index)
        {
            if (atPathStart)
            {
                return path[index];
            }
            return -path[path.Count - 1 - index];
        }

        /**
         * Determines if we have the color
         *@param
         *@return
         */
        public bool hasColor(Brew brew)
        {
            for (int i = 1; i <= color; i <<= 1)
            {
                if ((i & color) != 0 && (i & brew.getColor()) == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /**
         * Determines whether or not this bouncer is passable
         *@param brew the brew we have
         *@return true or false
         */
        public bool canPass(Brew brew, int x, int y, int w, int h)
        {
            // ensure color is correct
            Console.WriteLine(color + " // " + brew.getColor());
            if (hasColor(brew))
            {
                // ensure path not blocked
                Area area = GameplayManager.ActiveArea;
                int dir = atPathStart ? 1 : -1;
                for (int i = atPathStart ? 0 : path.Count - 1; atPathStart ? (i < path.Count) : (i >= 0); i += dir)
                {
                    if (path[i] == PATH_UP * dir)
                    {
                        y -= Area.TILE_HEIGHT;
                    }
                    else if (path[i] == PATH_DOWN * dir)
                    {
                        y += Area.TILE_HEIGHT;
                    }
                    else if (path[i] == PATH_LEFT * dir)
                    {
                        x -= Area.TILE_WIDTH;
                    }
                    else if (path[i] == PATH_RIGHT * dir)
                    {
                        x += Area.TILE_WIDTH;
                    }

                    // check obj at x,y
                    if (area.objectAt(x, y, w, h))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /**
         * Determiens if two objects are the same
         *@param other the other object
         */
        public override bool equals(PuzzleObject other)
        {
            return (other.type == type && ((Bouncer)other).color == color);
        }

        /**
         * Makes a copy of this
         * @return this
         */
        public override PuzzleObject copy()
        {
            return new Bouncer(color, path);
        }
    }
}
