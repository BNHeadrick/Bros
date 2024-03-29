﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS8803AGA.puzzle
{
    public abstract class PuzzleObject
    {
        public const int TYPE_NONE = 0; /**< NaN */
        public const int TYPE_BREW = 1; /**< brew type */
        public const int TYPE_BOUNCER = 2; /**< guard type */

        public static int next_id = 0;

        public int type; /**< maybe there's multiple types of puzzle objects */
        public int id;

        /**
         * Determiens if two objects are the same
         *@param other the other object
         */
        public abstract bool equals(PuzzleObject other);

        /**
         * Makes a copy of this
         * @return this
         */
        public abstract PuzzleObject copy();

        public bool isSameAs(PuzzleObject other)
        {
            return (id == other.id);
        }
    }
}
