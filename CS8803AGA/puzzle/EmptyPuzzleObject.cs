using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS8803AGA.puzzle
{
    public class EmptyPuzzleObject: PuzzleObject
    {
        public EmptyPuzzleObject()
        {
            type = PuzzleObject.TYPE_NONE;
        }

        /**
         * Determiens if two objects are the same
         *@param other the other object
         */
        public override bool equals(PuzzleObject other)
        {
            return (other.type == type);
        }

        /**
         * Makes a copy of this
         * @return this
         */
        public override PuzzleObject copy()
        {
            return new EmptyPuzzleObject();
        }
    }
}
