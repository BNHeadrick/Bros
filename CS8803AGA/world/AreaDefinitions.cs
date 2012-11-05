using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CS8803AGA.puzzle;

namespace CS8803AGA.world
{
    class AreaDefinitions
    {
        /// <summary>
        /// Returns the area (in drunken format) at the specified coordinate
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static int[,] areaAt(Point location)
        {
            int[] tiles;
            int a = 10;
            int b = 11;
            int c = 12;
            //int d = 13;
            //int e = 14;
            if (location == Area.START)
            { // map 0x0
                tiles = new int[]{
                    
                    3,3,3,3,3,3,3,3,3,3,3,0,6,2,3,3,3,3,3,3,3,3,3,3,
                    3,3,3,3,3,3,3,3,3,3,3,5,6,7,3,3,3,3,3,3,3,3,3,3,
                    3,3,3,3,3,3,3,3,3,3,3,5,6,7,3,3,3,3,3,3,3,3,3,3,
                    3,3,3,3,3,3,3,3,1,1,1,0,6,7,3,3,3,3,3,3,3,3,3,3,
                    3,3,3,3,3,3,3,5,6,6,6,6,6,c,3,3,3,3,3,3,3,3,3,3,
                    3,3,3,3,3,3,3,5,6,6,6,6,c,3,3,3,3,3,3,3,3,3,3,3,
                    3,3,3,3,3,3,3,5,6,6,6,6,7,3,3,3,3,3,3,3,3,3,3,3,
                    3,3,3,3,3,3,3,3,a,6,c,b,c,3,3,3,3,3,3,3,3,3,3,3,
                    4,4,4,3,3,3,3,3,5,6,7,3,3,3,3,3,3,3,3,3,3,3,3,3,
                    4,4,4,3,3,3,3,3,5,6,7,3,3,3,3,3,3,3,3,3,3,3,3,3,
                    3,3,3,3,3,3,3,3,5,6,7,3,3,3,3,3,3,3,3,3,3,3,3,3,
                    3,3,3,3,3,3,3,3,5,6,7,3,3,3,3,3,3,3,3,3,3,3,3,3,
                    3,3,3,3,3,3,3,3,5,6,7,3,3,3,3,3,3,3,3,3,3,3,3,3
                    
                    /*
                    4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4
                    */
                };
            }
            else if (location == Area.PARTYHOOD)
            { // map 0x0
                tiles = new int[]{
                    3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,
                    3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,
                    3,3,3,3,3,3,3,3,3,6,3,3,3,3,3,3,3,3,3,3,3,3,3,3,
                    3,3,3,3,3,3,3,3,3,6,3,3,3,3,3,3,3,3,3,3,3,3,3,3,
                    3,3,3,3,3,3,3,3,3,3,3,3,0,1,2,3,3,3,3,3,3,3,3,3,
                    3,3,3,3,3,3,3,3,3,3,3,3,5,6,7,3,3,3,3,3,3,3,3,3,
                    3,3,3,3,3,3,3,3,3,3,3,3,5,6,7,3,3,3,3,3,3,3,3,3,
                    3,3,3,3,3,3,3,3,3,3,3,3,5,6,7,3,3,3,3,3,3,3,3,3,
                    3,3,3,3,3,3,3,3,3,3,3,3,5,6,7,4,4,4,3,3,3,3,3,3,
                    3,3,3,3,3,3,3,3,3,3,3,3,5,6,7,4,4,4,3,3,3,3,3,3,
                    3,3,3,3,4,4,4,3,3,3,3,5,6,6,7,3,3,3,3,3,3,3,3,3,
                    3,3,3,3,3,3,3,3,3,3,3,5,6,7,3,3,3,3,3,3,3,3,3,3,
                    3,3,3,3,3,3,3,3,3,3,3,5,6,7,3,3,3,3,3,3,3,3,3,3
                };
            }
            else if (location == Area.LIQUOR_STORE)
            {
                tiles = new int[]{
                    4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
                    4,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,4,
                    4,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,4,
                    4,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,4,
                    4,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,4,
                    4,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,4,
                    4,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,4,
                    4,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,4,
                    4,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,4,
                    4,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,4,
                    4,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,4,
                    4,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,3,6,6,4,
                    4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,3,4,4,4
                };
            }
            else if (location == Area.PARTY)
            {
                tiles = new int[]{
                    4,4,4,4,4,4,4,4,4,3,4,4,4,4,4,4,4,4,4,4,4,4,4,4,
                    4,6,6,6,6,6,6,6,6,3,6,6,6,6,6,6,6,6,6,6,6,6,6,4,
                    4,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,4,
                    4,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,4,
                    4,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,4,
                    4,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,4,
                    4,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,4,
                    4,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,4,
                    4,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,4,
                    4,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,4,
                    4,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,4,
                    4,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,4,
                    4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4
                };
            }
            else
            { // default oopsie map
                tiles = new int[]{
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4,
                    4,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,4
                };
            }

            int[,] drunkenTiles = new int[Area.WIDTH_IN_TILES,Area.HEIGHT_IN_TILES];
            for (int i = 0; i < Area.WIDTH_IN_TILES; i++)
            {
                for (int j = 0; j < Area.HEIGHT_IN_TILES; j++)
                {
                    drunkenTiles[i, j] = tiles[i+j*Area.WIDTH_IN_TILES];
                }
            }

            return drunkenTiles;
        }

        /// <summary>
        /// Returns the doodad layer (in drunken format) at the specified coordinate
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static int[,] doodadsAt(Point location)
        {
            /* doodads
            // see Area.makeTestArea for what each doodad index is
            */
            int[] doodads;
            int Z = 0;// Constants.COMPANION;

            int a = 10;
            int b = 11;
            int c = 12;

            if (location == Area.START)
            { // map 0x0

                

                doodads = new int[] {
                    
                    1,0,0,0,0,0,0,0,0,0,0,b,0,c,0,0,0,0,0,0,0,0,0,2,
                    3,4,5,6,3,4,5,6,3,4,3,6,Z,3,4,5,6,3,4,5,6,3,4,5,
                    a,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,213,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,b,0,0,0,0,0,0,0,c,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2
                    


                    
                };
            }
            else if (location == Area.LIQUOR_STORE)
            { // map 0x0
                int M = Constants.LIQUERMERCH;
                int C = 35;
                doodads = new int[] {
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,M,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,C,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,7,7,0,0,0,0,
                    0,0,9,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
                };
            }
            else if (location == Area.START_SOUTH)
            {
                int M = 181;
                int C = 242;
                int B = 73;
                int K = 271;
                doodads = new int[] {
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,
                    1,0,0,0,0,K,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,
                    1,0,0,0,0,0,0,0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,6,2,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,
                    1,0,0,0,0,0,0,0,0,6,6,6,6,6,6,6,6,6,5,0,5,0,0,2,
                    1,0,0,0,0,0,0,4,0,0,0,0,0,0,C,0,0,0,0,3,4,0,0,2,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,B,0,0,0,3,0,0,2,
                    1,0,0,0,3,0,0,0,0,0,0,0,M,0,0,0,0,0,0,0,0,0,0,2,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,2,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,
                    1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2,1,2
                };
            }
            else if (location == Area.START_WEST)
            { // default oopsie map
                int D = 252;
                doodads = new int[] {
                    2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,
                    2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,
                    1,0,0,0,D,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,
                    2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,
                    2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,
                    2,0,0,0,0,3,0,3,0,4,0,4,0,6,0,6,0,5,0,5,0,0,0,2,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,
                    1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1
                };
            }
            else if (location == Area.START_EAST)
            {
                int W = 366;
                int T = 321;
                int V = 351;
                doodads = new int[] {
                    1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,T,0,
                    b,0,0,0,0,0,0,0,b,0,T,0,0,0,0,T,0,0,0,T,0,0,0,0,
                    0,0,0,0,0,V,0,0,c,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,c,0,0,0,0,0,T,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,c,0,0,0,0,0,0,0,T,0,0,0,0,0,T,0,
                    0,0,W,0,0,0,0,0,c,0,T,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,c,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,c,0,0,0,0,T,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,b,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,b,0,0,0,0,0,0,0,0,0,0,0,T,0,0,0,
                    0,0,0,0,0,0,0,0,b,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,b,0,0,0,T,0,0,0,T,0,0,0,0,0,0,0,
                    1,1,1,1,1,1,1,1,1,0,T,0,0,0,0,0,0,0,0,0,T,0,0,0
                };
            }
            else if (location == Area.PARTYHOOD)
            { // map 0x-1 inside the house!
                int B = 157;
                int D = 301;
                doodads = new int[] {
                    b,c,c,c,c,c,c,c,c,c,c,b,0,0,0,0,0,0,0,0,0,0,0,b,
                    b,0,0,0,0,0,0,0,0,0,0,b,0,0,0,0,0,0,0,0,0,0,0,b,
                    b,0,0,0,0,0,0,0,0,0,0,b,0,0,0,a,0,0,0,0,0,0,0,b,
                    b,0,0,0,0,0,0,0,0,0,0,b,0,0,0,0,0,0,0,0,0,0,0,b,
                    b,0,0,0,a,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,b,
                    b,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,b,
                    b,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,b,
                    b,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,b,
                    b,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,D,0,0,b,
                    b,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,b,
                    b,0,0,0,0,0,0,0,0,B,0,0,0,0,0,0,0,0,0,0,0,0,0,b,
                    b,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,b,
                    3,3,3,3,3,3,3,3,3,3,3,3,0,3,3,3,3,3,3,3,3,3,3,3
                };
            }
            else if (location == Area.PARTYHOOD_NORTH)
            { // map 0x-1 inside the house!

                /* OLD PARTYHOOD-NORTH
                int M = Constants.BREW_THIEF1;
                int S = Constants.BREW_THIEF2;
                int T = Constants.BREW_THIEF3;
                int H = Constants.STOLEN_BREW;
                doodads = new int[] {
                    7,8,0,9,0,8,0,9,0,8,0,9,0,8,0,9,0,8,0,9,0,8,0,7,
                    7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,
                    7,0,0,0,M,0,0,0,0,0,0,S,0,0,0,0,0,0,T,0,0,0,0,7,
                    7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,
                    7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,
                    7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,
                    7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,
                    7,0,0,0,0,0,0,0,0,0,0,H,0,0,0,0,0,0,0,0,0,0,0,7,
                    7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,
                    7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,
                    7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,
                    7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,
                    b,c,c,c,c,c,c,c,c,c,c,b,0,0,0,0,0,0,0,0,0,0,0,b
                };
                */
                int D = 131; //guard
                int N = 15; //ness
                int M = 16; //monkey
                int I = 242;//nekogirl
                int L = 251;//oldman
                
                int W = 341;//white vehicle
                int B = 342;//blue vehicle
                int R = 344;//red vehicle
                int T = 161;//orange flame
                int G = 162;//green flame
                


                doodads = new int[] {
                    0,4,4,4,4,4,4,4,4,0,4,4,4,4,4,4,4,4,4,4,4,4,4,0,
                    0,0,0,0,0,0,4,0,0,0,0,4,0,0,0,0,0,0,4,0,0,0,0,0,
                    0,0,0,0,0,0,4,0,0,0,0,4,0,0,0,0,0,0,4,0,0,0,T,0,
                    0,0,0,0,0,0,4,0,0,0,0,4,0,0,0,0,B,0,4,0,0,0,0,0,
                    0,0,0,0,0,0,4,L,4,4,I,4,0,0,0,0,0,0,4,4,D,4,4,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,4,4,M,4,0,0,0,0,G,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,4,0,0,0,0,0,0,0,4,4,4,N,4,4,4,4,4,4,4,0,
                    0,0,0,0,4,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,W,0,
                    0,0,R,0,4,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,4,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,
                    0,4,4,4,4,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0
                };
            }
            else if (location == Area.PARTY)
            {
                int A = Constants.BREW_MAIDEN;
                int C = Constants.COOK;

                int Y = Constants.PARTY_PEOPLE1;
                int P = Constants.PARTY_PEOPLE2;
                int D = Constants.PARTY_PEOPLE3;
                int M = Constants.PARTY_PEOPLE4;
                int G = Constants.PARTY_PEOPLE5;
                int F = Constants.PARTY_PEOPLE6;
                int H = Constants.PARTY_PEOPLE7;
                int I = Constants.PARTY_PEOPLE8;
                int B = Constants.PARTY_PEOPLE9;
                int L = Constants.PARTY_PEOPLE10;
                doodads = new int[] {
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,L,0,0,0,0,0,0,H,0,0,I,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,Y,0,P,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,F,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,A,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,M,0,0,0,0,
                    0,0,0,C,0,0,B,0,0,0,0,0,0,0,0,0,0,0,D,0,G,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
                };
            }

            else if (location == Area.PARTYHOOD_NORTH2)
            {
                /*
                int D = 131; //guard
                int N = 15; //ness
                int M = 16; //monkey
                */
                int I = 242;//nekogirl
                int L = 251;//oldman
                int U = 73;//bunny

                int A = 141;//fairy
                
                
                
                int M = 181;//machine
                

                int W = 341;//white vehicle
                int B = 342;//blue vehicle
                int R = 344;//red vehicle
                int T = 161;//orange flame
                int G = 162;//green flame
                int Y = 214;//yellow monster
                int K = 118;//black dragon
                int H = 31; //brown horse
                int X = 217;//purple monster

                
                 
                doodads = new int[] {
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,4,0,0,0,0,
                    0,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,0,4,0,0,0,0,
                    0,0,0,0,4,0,0,0,0,4,0,0,0,4,0,0,0,0,0,0,4,4,4,0,
                    0,4,4,4,4,0,0,0,0,4,0,0,0,4,0,4,4,4,0,4,4,0,0,0,
                    0,0,0,0,0,0,4,4,0,4,4,0,0,4,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,4,0,0,0,4,4,0,4,4,4,4,4,0,4,0,0,0,0,
                    0,0,M,4,4,0,4,4,4,U,4,4,0,0,0,0,0,4,0,4,0,0,0,0,
                    0,0,0,4,0,0,0,0,4,0,0,0,I,4,0,4,4,4,A,4,4,4,4,0,
                    0,4,4,4,4,4,U,4,4,4,4,L,0,4,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,4,0,0,0,4,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,4,4,4,0,4,4,4,4,4,4,4,4,4,4,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,
                    0,4,4,4,4,4,4,4,4,0,4,4,4,4,4,4,4,4,4,4,4,4,4,0
                };
            }

            else if (location == Area.PARTYHOOD_NORTH3)
            {
                /*
                int D = 131; //guard
                int N = 15; //ness
                int M = 16; //monkey
                */
                int I = 242;//nekogirl
                int L = 251;//oldman
                int U = 73;//bunny

                int A = 141;//fairy



                int M = 181;//machine


                int W = 341;//white vehicle
                int B = 342;//blue vehicle
                int R = 344;//red vehicle
                int T = 161;//orange flame
                int G = 162;//green flame
                int Y = 214;//yellow monster
                int K = 118;//black dragon
                int H = 31; //brown horse
                int X = 217;//purple monster



                doodads = new int[] {
                    0,4,4,4,4,4,4,4,4,4,4,4,0,4,4,4,4,4,4,4,4,4,4,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,4,4,4,4,4,4,L,4,4,4,4,4,4,4,4,4,I,4,4,4,4,4,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,Y,G,K,T,H,X,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,0,4,4,4,4,0
                };
            }

            else if (location == Area.PARTYHOOD_NORTH4)
            {
                
                int D = 131; //guard
                int N = 15; //ness
                int M = 16; //monkey
                int I = 242;//nekogirl
                int L = 251;//oldman


                int W = 341;//white vehicle
                int B = 342;//blue vehicle
                int R = 344;//red vehicle
                int T = 161;//orange flame
                int G = 162;//green flame
                int Y = 214;//yellow monster
                int K = 118;//black dragon
                int H = 31; //brown horse
                int X = 217;//purple monster



                doodads = new int[] {
                    0,4,4,4,4,4,4,4,4,4,4,4,0,4,4,4,4,4,4,4,4,4,4,0,
                    0,0,0,0,0,0,0,0,0,0,4,0,0,0,4,0,0,0,0,4,0,B,0,0,
                    0,0,0,0,0,0,0,0,0,0,4,0,0,0,4,0,0,0,0,4,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,4,0,0,0,4,0,0,0,0,4,N,4,4,0,
                    0,0,0,0,0,0,0,0,0,0,4,L,4,I,4,0,0,0,0,0,0,0,0,0,
                    0,M,4,4,4,0,0,G,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,R,4,0,0,0,0,0,0,0,W,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,4,D,4,0,
                    0,4,4,4,4,0,0,0,0,0,0,0,0,0,0,T,0,4,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,X,H,K,Y,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,
                    0,4,4,4,4,4,4,4,4,4,4,4,0,4,4,4,4,4,4,4,4,4,4,0
                };
            }

            else if (location == Area.PARTYHOOD_NORTH5)
            {
                int L = 251;//oldman

                int A = 141;//fairy

                int M = 181;//machine

                
                doodads = new int[] {
                    0,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,4,4,4,0,4,0,0,4,0,4,4,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,4,0,0,0,4,4,0,4,0,4,0,4,0,0,0,0,0,0,0,0,0,0,
                    0,0,4,4,0,0,4,4,4,4,0,4,0,4,0,0,0,0,0,0,0,0,0,0,
                    0,0,4,0,0,0,4,0,4,4,0,4,0,4,0,0,0,0,0,0,0,0,0,0,
                    0,0,4,4,4,0,4,0,0,4,0,4,4,0,4,4,4,4,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,4,4,4,4,4,4,4,4,4,0,4,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,
                    0,4,4,4,4,4,4,4,L,4,4,4,4,4,4,4,A,4,4,4,4,4,4,0,
                    0,0,0,0,0,0,0,M,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,4,4,4,4,4,4,4,4,4,4,4,0,4,4,4,4,4,4,4,4,4,4,0
                };
            }

            else
            { // default oopsie map
                doodads = new int[] {
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
                };
            }

            int[,] drunkenDoodads = new int[Area.WIDTH_IN_TILES, Area.HEIGHT_IN_TILES];
            for (int i = 0; i < Area.WIDTH_IN_TILES; i++)
            {
                for (int j = 0; j < Area.HEIGHT_IN_TILES; j++)
                {
                    drunkenDoodads[i, j] = doodads[i + j * Area.WIDTH_IN_TILES];
                }
            }

            return drunkenDoodads;
        }
        
        public static Dictionary<int, PuzzleObject> puzzleAt(Point location)
        {
            Dictionary<int, PuzzleObject> puzzle = new Dictionary<int, PuzzleObject>();

            if (location == Area.START_SOUTH)
            {
                int M = 181;
                int C = 242;
                int B = 73;
                int K = 271;

                
                List<int> k_path = new List<int>();
                k_path.Add(Bouncer.PATH_LEFT);
                k_path.Add(Bouncer.PATH_LEFT);
                k_path.Add(Bouncer.PATH_LEFT);
                k_path.Add(Bouncer.PATH_DOWN);
                k_path.Add(Bouncer.PATH_RIGHT);
                k_path.Add(Bouncer.PATH_UP);
                k_path.Add(Bouncer.PATH_RIGHT);
                puzzle.Add(K, new Bouncer(Brew.COLOR_RED|Brew.COLOR_BLUE|Brew.COLOR_WHITE, k_path));
                
                puzzle.Add(M, new Brew(Brew.COLOR_RED | Brew.COLOR_YELLOW, 0));
                puzzle.Add(C, new Brew(Brew.COLOR_BLUE, 0));
                puzzle.Add(B, new Brew(Brew.COLOR_WHITE, Brew.COLOR_YELLOW));
            }
            else if (location == Area.START)
            {
                puzzle.Add(213, new Brew(Brew.COLOR_RED, 0));
            }


            else if (location == Area.PARTYHOOD_NORTH)
            {
                int D = 131; //guard
                int N = 15; //ness
                int M = 16; //monkey
                int I = 242;//nekogirl
                int L = 251;//oldman

                int W = 341;//white vehicle
                int B = 342;//blue vehicle
                int R = 344;//red vehicle
                int T = 161;//orange flame
                int G = 162;//green flame

                
                List<int> k_path = new List<int>();
                k_path.Add(Bouncer.PATH_UP);
                k_path.Add(Bouncer.PATH_LEFT);

                puzzle.Add(N, new Bouncer(Brew.COLOR_WHITE, k_path));
                puzzle.Add(W, new Brew(Brew.COLOR_WHITE, 0));

                k_path = new List<int>();
                k_path.Add(Bouncer.PATH_UP);
                k_path.Add(Bouncer.PATH_UP);

                puzzle.Add(D, new Bouncer(Brew.COLOR_GREEN, k_path));
                puzzle.Add(G, new Brew(Brew.COLOR_GREEN, 0));
                puzzle.Add(R, new Brew(Brew.COLOR_RED, 0));

                k_path = new List<int>();
                k_path.Add(Bouncer.PATH_DOWN);
                k_path.Add(Bouncer.PATH_DOWN);

                puzzle.Add(M, new Bouncer(Brew.COLOR_BLUE, k_path));
                puzzle.Add(B, new Brew(Brew.COLOR_BLUE, 0));
                puzzle.Add(T, new Brew(Brew.COLOR_ORANGE, 0));

                k_path = new List<int>();
                k_path.Add(Bouncer.PATH_UP);
                k_path.Add(Bouncer.PATH_UP);

                puzzle.Add(I, new Bouncer(Brew.COLOR_BLUE|Brew.COLOR_ORANGE, k_path));

                k_path = new List<int>();
                k_path.Add(Bouncer.PATH_UP);
                k_path.Add(Bouncer.PATH_UP);

                puzzle.Add(L, new Bouncer(Brew.COLOR_RED | Brew.COLOR_GREEN, k_path));

            }

            else if (location == Area.PARTYHOOD_NORTH2)
            {

                int I = 242;//nekogirl
                int L = 251;//oldman
                int U = 73;//bunny
                int A = 141;//fairy

                int M = 181;//machine

                List<int> i_path = new List<int>();
                i_path.Add(Bouncer.PATH_DOWN);
                i_path.Add(Bouncer.PATH_DOWN);
                puzzle.Add(I, new Bouncer(Brew.COLOR_BLUE|Brew.COLOR_ORANGE, i_path));

                List<int> l_path = new List<int>();
                l_path.Add(Bouncer.PATH_RIGHT);
                l_path.Add(Bouncer.PATH_DOWN);
                l_path.Add(Bouncer.PATH_DOWN);
                puzzle.Add(L, new Bouncer(Brew.COLOR_RED | Brew.COLOR_GREEN, l_path));

                List<int> u_path = new List<int>();
                u_path.Add(Bouncer.PATH_UP);
                u_path.Add(Bouncer.PATH_LEFT);
                u_path.Add(Bouncer.PATH_LEFT);
                puzzle.Add(U, new Bouncer(Brew.COLOR_WHITE | Brew.COLOR_GREEN, u_path));

                puzzle.Add(M, new Brew(Brew.COLOR_YELLOW, Brew.COLOR_RED));

                List<int> a_path = new List<int>();
                a_path.Add(Bouncer.PATH_UP);
                a_path.Add(Bouncer.PATH_UP);
                a_path.Add(Bouncer.PATH_UP);
                a_path.Add(Bouncer.PATH_UP);
                puzzle.Add(A, new Bouncer(Brew.COLOR_WHITE | Brew.COLOR_GREEN | Brew.COLOR_YELLOW, a_path));


                int W = 341;//white vehicle
                int B = 342;//blue vehicle
                int R = 344;//red vehicle
                int T = 161;//orange flame
                int Y = 214;//yellow monster
                int G = 162;//green flame
                puzzle.Add(W, new Brew(Brew.COLOR_WHITE, 0));
                puzzle.Add(Y, new Brew(Brew.COLOR_YELLOW, 0));
                puzzle.Add(R, new Brew(Brew.COLOR_RED, 0));
                puzzle.Add(T, new Brew(Brew.COLOR_ORANGE, 0));
                puzzle.Add(G, new Brew(Brew.COLOR_GREEN, 0));
                puzzle.Add(B, new Brew(Brew.COLOR_BLUE, 0));
            }

            else if (location == Area.PARTYHOOD_NORTH3)
            {

                int I = 242;//nekogirl
                int L = 251;//oldman

                int Y = 214;//yellow monster
                int K = 118;//black dragon
                int H = 31; //brown horse
                int X = 217;//purple monster
                //these last two are red herrings.
                int T = 161;//orange flame
                int G = 162;//green flame


                List<int> k_path = new List<int>();
                k_path = new List<int>();
                k_path.Add(Bouncer.PATH_UP);
                k_path.Add(Bouncer.PATH_UP);
                k_path.Add(Bouncer.PATH_UP);

                puzzle.Add(I, new Bouncer(Brew.COLOR_BROWN | Brew.COLOR_BLACK, k_path));

                puzzle.Add(K, new Brew(Brew.COLOR_BLACK, 0));
                puzzle.Add(H, new Brew(Brew.COLOR_BROWN, 0));

                k_path = new List<int>();
                k_path.Add(Bouncer.PATH_UP);
                k_path.Add(Bouncer.PATH_UP);
                k_path.Add(Bouncer.PATH_UP);

                puzzle.Add(L, new Bouncer(Brew.COLOR_VIOLET | Brew.COLOR_YELLOW, k_path));

                puzzle.Add(X, new Brew(Brew.COLOR_VIOLET, 0));
                puzzle.Add(Y, new Brew(Brew.COLOR_YELLOW, 0));

                //red herrings
                puzzle.Add(T, new Brew(Brew.COLOR_ORANGE, 0));
                puzzle.Add(G, new Brew(Brew.COLOR_GREEN, 0));
            }

            else if (location == Area.PARTYHOOD_NORTH4)
            {
                int D = 131; //guard
                int N = 15; //ness
                int M = 16; //monkey

                int I = 242;//nekogirl
                int L = 251;//oldman

                int W = 341;//white vehicle
                int B = 342;//blue vehicle
                int R = 344;//red vehicle
                int Y = 214;//yellow monster
                int K = 118;//black dragon
                int H = 31; //brown horse
                int X = 217;//purple monster
                //these last two are red herrings.
                int T = 161;//orange flame
                int G = 162;//green flame


                List<int> k_path = new List<int>();
                k_path = new List<int>();
                k_path.Add(Bouncer.PATH_UP);
                k_path.Add(Bouncer.PATH_UP);
                k_path.Add(Bouncer.PATH_UP);

                puzzle.Add(I, new Bouncer(Brew.COLOR_BLUE, k_path));
                puzzle.Add(B, new Brew(Brew.COLOR_BLUE, 0));
                
                k_path = new List<int>();
                k_path.Add(Bouncer.PATH_UP);
                k_path.Add(Bouncer.PATH_UP);
                k_path.Add(Bouncer.PATH_UP);

                puzzle.Add(L, new Bouncer(Brew.COLOR_RED | Brew.COLOR_GREEN, k_path));
                puzzle.Add(R, new Brew(Brew.COLOR_RED, 0));

                k_path = new List<int>();
                k_path.Add(Bouncer.PATH_UP);
                k_path.Add(Bouncer.PATH_RIGHT);
                k_path.Add(Bouncer.PATH_RIGHT);

                puzzle.Add(N, new Bouncer(Brew.COLOR_YELLOW | Brew.COLOR_VIOLET, k_path));
                puzzle.Add(Y, new Brew(Brew.COLOR_YELLOW, 0));
                puzzle.Add(X, new Brew(Brew.COLOR_VIOLET, 0));

                k_path = new List<int>();
                k_path.Add(Bouncer.PATH_DOWN);
                k_path.Add(Bouncer.PATH_DOWN);
                k_path.Add(Bouncer.PATH_DOWN);

                puzzle.Add(M, new Bouncer(Brew.COLOR_BLACK | Brew.COLOR_BROWN, k_path));
                puzzle.Add(K, new Brew(Brew.COLOR_BLACK, 0));
                puzzle.Add(H, new Brew(Brew.COLOR_BROWN, 0));

                k_path = new List<int>();
                k_path.Add(Bouncer.PATH_DOWN);
                k_path.Add(Bouncer.PATH_LEFT);
                k_path.Add(Bouncer.PATH_LEFT);
                k_path.Add(Bouncer.PATH_LEFT);

                puzzle.Add(D, new Bouncer(Brew.COLOR_WHITE, k_path));
                puzzle.Add(W, new Brew(Brew.COLOR_WHITE, 0));
                

                //red herrings
                puzzle.Add(T, new Brew(Brew.COLOR_ORANGE, 0));
                puzzle.Add(G, new Brew(Brew.COLOR_GREEN, 0));
            }

            else if (location == Area.PARTYHOOD_NORTH5)
            {
                int L = 251;//oldman
                int A = 141;//fairy

                int M = 181;//machine

                List<int> l_path = new List<int>();
                l_path.Add(Bouncer.PATH_UP);
                l_path.Add(Bouncer.PATH_RIGHT);
                l_path.Add(Bouncer.PATH_RIGHT);
                l_path.Add(Bouncer.PATH_RIGHT);
                l_path.Add(Bouncer.PATH_RIGHT);
                l_path.Add(Bouncer.PATH_RIGHT);
                l_path.Add(Bouncer.PATH_RIGHT);
                l_path.Add(Bouncer.PATH_RIGHT);
                l_path.Add(Bouncer.PATH_RIGHT);
                l_path.Add(Bouncer.PATH_UP);
                puzzle.Add(L, new Bouncer(Brew.COLOR_RED | Brew.COLOR_GREEN, l_path));

                puzzle.Add(M, new Brew(Brew.COLOR_YELLOW, Brew.COLOR_RED));

                List<int> a_path = new List<int>();
                a_path.Add(Bouncer.PATH_UP);
                a_path.Add(Bouncer.PATH_UP);
                a_path.Add(Bouncer.PATH_UP);
                a_path.Add(Bouncer.PATH_LEFT);
                puzzle.Add(A, new Bouncer(Brew.COLOR_WHITE | Brew.COLOR_GREEN | Brew.COLOR_YELLOW, a_path));
            }

            return puzzle;
        }

        /// <summary>
        /// Returns the transition list for special (in-map) transitions
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static int[] transitionsAt(Point location)
        {
            int[] transitions = null;
            // x, y, globalTargetX, globalTargetY, destTileX, destTileY

            if (location == Area.START)
            { // map 0x0
                
                transitions = new int[]{
                        //liquer door
                        5, 7, Area.LIQUOR_STORE.X, Area.LIQUOR_STORE.Y, 20, 11
                };
                
            }
            else if (location == Area.LIQUOR_STORE)
            {
                transitions = new int[]{
                        /*liquer door */20, 12, Area.START.X, Area.START.Y, 5, 8
                };
            }
            else if (location == Area.PARTYHOOD && questcontent.Quest.hasPartyKey)
            {
                transitions = new int[]{
                        /*back door */9, 4, Area.PARTY.X, Area.PARTY.Y, 9, 1
                };
            }
            else if (location == Area.PARTY)
            {
                transitions = new int[]{
                        /*back door */9, 0, Area.PARTYHOOD.X, Area.PARTYHOOD.Y, 9, 3
                };
            }

            /*else if (location == Area.PARTYHOOD_NORTH)
            {
                transitions = new int[]{
                        /*back door * /0, 9, Area.PARTYHOOD_NORTH2.X, Area.PARTYHOOD_NORTH2.Y, 9, 3
                };
            }*/

            //Console.WriteLine("This function was called");

            return transitions;
        }
    }
}
