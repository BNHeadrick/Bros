using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CS8803AGAGameLibrary;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using CSharpQuadTree;
using CS8803AGA.controllers;
using CS8803AGA.collision;
using CS8803AGA.world;
using CS8803AGA.dialog;
using CS8803AGA.puzzle;

namespace CS8803AGA
{
    /// <summary>
    /// A portion of the game world; exactly one is visible on the monitor at a time
    /// </summary>
    public class Area
    {
        public static Point START = new Point(0, 0);
        public static Point START_SOUTH = new Point(0, 1);
        public static Point START_WEST = new Point(-1, 0);
        public static Point START_EAST = new Point(1, 0);
        public static Point PARTYHOOD = new Point(0, -1);
        public static Point PARTYHOOD_NORTH = new Point(0, -2);
        public static Point PARTY = new Point(-1, -1);
        public static Point LIQUOR_STORE = new Point(-1, 1);

        #region Constants and Members

        public const int TILE_WIDTH = 40;           // width of a tile, in pixels
        public const int TILE_HEIGHT = 40;          // heigh of a tile, in pixels

        public const int WIDTH_IN_TILES = 24;       // number of tiles in the width of an area
        public const int HEIGHT_IN_TILES = 13;      // number of tiles-2 in height of an area

        public Point GlobalLocation                 // x,y coordinate of the area in the world (can be negative)
        { get; protected set; }

        public TileSet TileSet                      // information about a set of matching tiles to be used in the area
        { get; protected set; }

        public int[,] Tiles                         //  grid of tile #s, where each # is a specific graphic from the tileset
        { get; protected set; }                     // the grid represents the x,y coordinates of the tiles in the area

        public GameTexture TilesetTexture          // graphic to which the TileSet references
        { get; protected set; }

        public CollisionDetector CollisionDetector      // container for all collision boxes in the Area
        { get; protected set; }

        public List<IGameObject> GameObjects            // container for all GameObjects in the Area
        { get; protected set; }

        protected HashSet<Point> m_fixedTiles;          // used for construction, each Point is a coordinate of a tile
                                                        //  which has had an obstructing decoration placed up on it

        #endregion

        #region Construction & Initialization

        /// <summary>
        /// Load a TileSetInfo from content and create an Area using it at the specified global coordinate
        /// </summary>
        /// <param name="tileSetPath"></param>
        /// <param name="location"></param>
        public Area(string tileSetPath, Point location)
        {
            WorldManager.RegisterArea(location, this);

            this.GlobalLocation = location;

            this.TileSet = GlobalHelper.loadContent<TileSet>(tileSetPath);
            this.TilesetTexture = new GameTexture(TileSet.assetPath, TileSet.getSpriteSheetSourceRectangles());

            this.Tiles = new int[WIDTH_IN_TILES, HEIGHT_IN_TILES];
            this.m_fixedTiles = new HashSet<Point>();

            this.CollisionDetector = new CollisionDetector();
            this.GameObjects = new List<IGameObject>();
        }

        /// <summary>
        /// Creates an empty area using a default tileset at the specified global coordinate
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Area makeEmptyArea(Point location)
        {
            return makeEmptyArea(@"Sprites/TileSet1", location);
        }

        /// <summary>
        /// Creates an empty area using a specified tileset at the specified global coordinate
        /// </summary>
        /// <param name="tileSetpath"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Area makeEmptyArea(string tileSetpath, Point location)
        {
            Area a = new Area(tileSetpath, location);

            // default area
            /*for (int i = 0; i < WIDTH_IN_TILES; ++i)
                for (int j = 0; j < HEIGHT_IN_TILES; ++j)
                {
                    a.Tiles[i, j] = 3;
                }*/

            // whoever came up with this map format was drunk, 
            // it's stored rotated counter-clockwise 90 degrees AND THEN horizontally flipped
            // what the shit, right?
            a.Tiles = AreaDefinitions.areaAt(location);

            a.initializeTileColliders();
            a.initializeAreaTransitions(null, null, null, null);

            return a;
        }

        /// <summary>
        /// Creates an area using a default tileset at the specified global coordinate,
        /// contains some decorations
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Area makeTestArea(Point location)
        {
            return makeTestArea(@"Sprites/TileSet1", location);
        }

        /// <summary>
        /// Creates an area using a specified tileset at the specified global coordinate,
        /// contains some decorations
        /// </summary>
        /// <param name="tileSetpath"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Area makeTestArea(string tileSetpath, Point location)
        {
            DialogManager.load(location);
            Console.WriteLine("making location: " + location);

            Area a = makeEmptyArea(tileSetpath, location);

            DecorationSet ds1 = DecorationSet.construct(@"World/graveyard");
            DecorationSet ds2 = DecorationSet.construct(@"World/town");
            DecorationSet ds3 = DecorationSet.construct(@"World/trees");
            
            int[,] doodads = AreaDefinitions.doodadsAt(location);
            for (int i = 0; i < WIDTH_IN_TILES; i++)
            {
                for (int j = 0; j < HEIGHT_IN_TILES; j++)
                {
                    if (doodads[i, j] >= 13)
                    {
                        Console.WriteLine("doodad: " + doodads[i, j] + "x"+Constants.doodadIntToString(doodads[i, j]));
                        CharacterInfo ci = GlobalHelper.loadContent<CharacterInfo>(@"Characters/"+Constants.doodadIntToString(doodads[i, j]));
                        Vector2 pos = new Vector2(a.getTileRectangle(i, j).X+20, a.getTileRectangle(i, j).Y+8);
                        CharacterController cc = CharacterController.construct(ci, pos);
                        if (doodads[i, j] == 35)
                        {
                            List<int> path = new List<int>();
                            path.Add(Bouncer.PATH_LEFT);
                            path.Add(Bouncer.PATH_UP);
                            path.Add(Bouncer.PATH_LEFT);
                            path.Add(Bouncer.PATH_LEFT);
                            //cc.bouncer = new Bouncer(0, path);
                            cc.brew = new Brew(Brew.COLOR_BLACK, 0);
                        }
                        cc.setDoodadIndex(doodads[i, j]);
                        a.add(cc);
                    }
                    else if (doodads[i, j] != 0)
                    {
                        Vector2 pos = new Vector2(a.getTileRectangle(i, j).X, a.getTileRectangle(i, j).Y);
                        Decoration d;
                        if (doodads[i, j] < 10)
                        {
                            d = ds1.makeDecoration(Constants.doodadIntToString(doodads[i, j]), pos);
                        }
                        else if (doodads[i, j] < 11)
                        {
                            d = ds2.makeDecoration(Constants.doodadIntToString(doodads[i, j]), pos);
                        }
                        else
                        {
                            d = ds3.makeDecoration(Constants.doodadIntToString(doodads[i, j]), pos);
                        }
                        a.CollisionDetector.register(d.getCollider());
                        a.GameObjects.Add(d);
                    }
                }
            }
            
            a.initializeTileColliders();
            a.initializeAreaTransitions(null, null, null, null);

            // add special transitions for this map
            int[] transitions = AreaDefinitions.transitionsAt(location);
            if (transitions != null)
            {
                for (int i = 0; i < transitions.Length; i += 6)
                {
                    a.addAreaTransitionTrigger(transitions[i], transitions[i + 1], null, AreaSideEnum.Other, new Point(transitions[i + 2], transitions[i + 3]), new Point(transitions[i + 4], transitions[i + 5]));
                }
            }

            return a;
        }

        /// <summary>
        /// Certain tile #s in a TileSet may be marked as impassable.  This function looks up those
        /// numbers and makes instances of those tiles in the Area impassable by registering the
        /// appropriate collision box.
        /// </summary>
        public void initializeTileColliders()
        {
            for (int i = 0; i < WIDTH_IN_TILES; ++i)
            {
                for (int j = 0; j < HEIGHT_IN_TILES; ++j)
                {
                    if (!TileSet.tileInfos[Tiles[i, j]].passable)
                    {
                        Rectangle bounds = getTileRectangle(i, j);
                        Collider ci = new Collider(null, bounds, ColliderType.Scenery);
                        CollisionDetector.register(ci);
                    }
                }
            }
        }
        
        /// <summary>
        /// Puts area transition triggers around the edges of the Area, so that moving to the edges
        /// will transfer the player to the adjacent Area.
        /// </summary>
        /// <param name="north">Area to the north; i.e., to which Area the triggers on the top edge should lead</param>
        /// <param name="south"></param>
        /// <param name="east"></param>
        /// <param name="west"></param>
        public void initializeAreaTransitions(Area north, Area south, Area east, Area west)
        {
            // top
            for (int i = 0; i < WIDTH_IN_TILES; ++i)
            {
                this.addAreaTransitionTrigger(i, 0, north, AreaSideEnum.Top);
            }

            // bottom
            for (int i = 0; i < WIDTH_IN_TILES; ++i)
            {
                this.addAreaTransitionTrigger(i, HEIGHT_IN_TILES - 1, south, AreaSideEnum.Bottom);
            }

            // left
            for (int i = 0; i < HEIGHT_IN_TILES; ++i)
            {
                this.addAreaTransitionTrigger(0, i, west, AreaSideEnum.Left);
            }

            // right
            for (int i = 0; i < HEIGHT_IN_TILES; ++i)
            {
                this.addAreaTransitionTrigger(WIDTH_IN_TILES - 1, i, east, AreaSideEnum.Right);
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Determines the draw depth of a sprite based on its collision bounds.
        /// The bottom edge of the bounds is used, so elements in the world with lower bottom edges
        /// are drawn in front of those with higher bottom edges.
        /// </summary>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public float getDrawDepth(DoubleRect bounds)
        {
            // map coordinates to a 3x3 cluster of Areas centered around this one
            //  (done so that we eventually can draw neighboring areas' items)
            double mappedX = bounds.X + bounds.Width + getWidthInPixels();
            double mappedY = bounds.Y + bounds.Height + getHeightInPixels();

            return
                Constants.DepthBaseGameplay + Constants.DepthRangeGameplay *
                    ((float)(mappedY) / (getHeightInPixels() * 3) +
                    (float)(mappedX) / (getWidthInPixels() * 3) / 10000.0f);
        }

        /// <summary>
        /// Width of the area in pixels (WidthOfTilesInPixels * WidthOfAreaInTiles)
        /// </summary>
        /// <returns></returns>
        public int getWidthInPixels()
        {
            return WIDTH_IN_TILES * TileSet.tileWidth + TileSet.tileWidth;
        }

        /// <summary>
        /// Height of the area in pixels (HeightOfTilesInPixels *HeightOfAreaInTiles)
        /// </summary>
        /// <returns></returns>
        public int getHeightInPixels()
        {
            return HEIGHT_IN_TILES * TileSet.tileHeight + TileSet.tileHeight;
        }

        /// <summary>
        /// Converts an x,y coordinate of a Tile into the pixel-based Rectangle of that tile
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public Rectangle getTileRectangle(int i, int j)
        {
            return new Rectangle(i * TileSet.tileWidth, j * TileSet.tileHeight, TileSet.tileWidth, TileSet.tileHeight);
        }

        #endregion

        #region World Construction

        /// <summary>
        /// Returns true if a particular rectangle of tiles is all "unfixed", or no decorations
        /// impede the placement of another decoration in that section.
        /// </summary>
        /// <param name="tileTopLeft"></param>
        /// <param name="widthInPixels"></param>
        /// <param name="heightInPixels"></param>
        /// <returns></returns>
        public bool isSectionClear(Point tileTopLeft, int widthInPixels, int heightInPixels)
        {
            // TODO
            // make sure to handle rounding up if there is a partial tile consumption
            int rightTile = (int)Math.Ceiling((float)widthInPixels / Area.TILE_WIDTH) + tileTopLeft.X;
            int bottomTile = (int)Math.Ceiling((float)heightInPixels / Area.TILE_HEIGHT) + tileTopLeft.Y;
            Point tileBottomRight = new Point(rightTile, bottomTile);
            return isSectionClear(tileTopLeft, tileBottomRight);
        }

        /// <summary>
        /// Returns true if a particular rectangle of tiles is all "unfixed", or no decorations
        /// impede the placement of another decoration in that section.
        /// </summary>
        /// <param name="tileTopLeft"></param>
        /// <param name="tileBottomRight"></param>
        /// <returns></returns>
        public bool isSectionClear(Point tileTopLeft, Point tileBottomRight)
        {
            for (int i = tileTopLeft.X; i <= tileBottomRight.X; ++i)
            {
                for (int j = tileTopLeft.Y; j <= tileBottomRight.Y; ++j)
                {
                    if (m_fixedTiles.Contains(new Point(i, j)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Used when placing a decoration, marks a rectangle of space is occupied by a decoration so that
        /// nothing else can be placed there.
        /// </summary>
        /// <param name="tileTopLeft"></param>
        /// <param name="widthInPixels"></param>
        /// <param name="heightInPixels"></param>
        public void setSectionFixed(Point tileTopLeft, int widthInPixels, int heightInPixels)
        {
            int rightTile = (int)Math.Ceiling((float)widthInPixels / Area.TILE_WIDTH) + tileTopLeft.X;
            int bottomTile = (int)Math.Ceiling((float)heightInPixels / Area.TILE_HEIGHT) + tileTopLeft.Y;
            Point tileBottomRight = new Point(rightTile, bottomTile);
            setSectionFixed(tileTopLeft, tileBottomRight);
        }

        /// <summary>
        /// Used when placing a decoration, marks a rectangle of space is occupied by a decoration so that
        /// nothing else can be placed there.
        /// </summary>
        /// <param name="tileTopLeft"></param>
        /// <param name="tileBottomRight"></param>
        public void setSectionFixed(Point tileTopLeft, Point tileBottomRight)
        {
            for (int i = tileTopLeft.X; i <= tileBottomRight.X; ++i)
            {
                for (int j = tileTopLeft.Y; j <= tileBottomRight.Y; ++j)
                {
                    m_fixedTiles.Add(new Point(i, j));
                }
            }
        }

        #endregion

        #region Add Items

        /// <summary>
        /// Place a new area transition trigger in the Area; handles registering collision box
        /// </summary>
        /// <param name="tileX"></param>
        /// <param name="tileY"></param>
        /// <param name="target"></param>
        /// <param name="side"></param>
        public void addAreaTransitionTrigger(int tileX, int tileY, Area target, AreaSideEnum side)
        {
            AreaTransitionTrigger att =
                new AreaTransitionTrigger(this, target, new Point(tileX, tileY), side);
            add(att);
        }
        /// <summary>
        /// Place a new area transition trigger in the Area; handles registering collision box
        /// </summary>
        /// <param name="tileX"></param>
        /// <param name="tileY"></param>
        /// <param name="target"></param>
        /// <param name="side"></param>
        public void addAreaTransitionTrigger(int tileX, int tileY, Area target, AreaSideEnum side, Point globalTarget, Point destTile)
        {
            AreaTransitionTrigger att =
                new AreaTransitionTrigger(this, target, new Point(tileX, tileY), side, globalTarget, destTile);
            add(att);
        }

        /// <summary>
        /// Place a GameObject into the area
        /// </summary>
        /// <param name="gameObject"></param>
        public void add(IGameObject gameObject)
        {
            this.GameObjects.Add(gameObject);
        }

        /// <summary>
        /// Place a Collidable game object into the area; handles registering collision box
        /// </summary>
        /// <param name="collidable"></param>
        public void add(ICollidable collidable)
        {
            add((IGameObject)collidable);
            this.CollisionDetector.register(collidable.getCollider());
        }

        /// <summary>
        /// Remove a GameObject from the area
        /// </summary>
        /// <param name="gameObject"></param>
        public void remove(IGameObject gameObject)
        {
            this.GameObjects.Remove(gameObject);
        }

        //check if a GameObject exists in the area
        public bool exists(IGameObject gameObject)
        {
            foreach (IGameObject gameObj in GameObjects)
            {
                if(gameObject.Equals(gameObj)){
                    
                    return true;

                }
            }
            return false;
                   
        }

        /// <summary>
        /// Place a Collidable game object into the area; handles registering collision box
        /// </summary>
        /// <param name="collidable"></param>
        public void remove(ICollidable collidable)
        {
            remove((IGameObject)collidable);
            this.CollisionDetector.remove(collidable.getCollider());
        }

        #endregion

        #region Draw Methods

        /// <summary>
        /// Normal draw method to be called during gameplay
        /// </summary>
        public void draw()
        {
            for (int i = 0; i < WIDTH_IN_TILES; ++i)
            {
                for (int j = 0; j < HEIGHT_IN_TILES; ++j)
                {
                    Vector2 pos = new Vector2(i * TileSet.tileWidth, j * TileSet.tileHeight);
                    float depth = Constants.DepthGameplayTiles;
                    // if we use equal depth for all tiles, they will all be rendered at once, meaning the graphics device
                    //  doesn't have to switch between sprites... much better performance

                    DrawCommand td = DrawBuffer.getInstance().DrawCommands.pushGet();
                    td.set(TilesetTexture, Tiles[i,j], pos, CoordinateTypeEnum.ABSOLUTE, depth, false, Color.White, 0, 1.0f);
                }
            }

            foreach (IGameObject gameObject in GameObjects)
            {
                gameObject.draw();
            }
        }

        /// <summary>
        /// Scaled draw method for use in maps, minimaps, etc.
        /// </summary>
        /// <param name="offset">Pixel-position where the top left corner of the area should be drawn</param>
        /// <param name="scale">Amount the size of each graphic will be multiplied by when drawing</param>
        /// <param name="depth">z-Depth of the image</param>
        public void drawMap(Vector2 offset, float scale, float depth)
        {
            for (int i = 0; i < WIDTH_IN_TILES; ++i)
            {
                for (int j = 0; j < HEIGHT_IN_TILES; ++j)
                {
                    Vector2 pos = new Vector2(i * TileSet.tileWidth, j * TileSet.tileHeight);

                    DrawCommand td = DrawBuffer.getInstance().DrawCommands.pushGet();
                    td.set(TilesetTexture, Tiles[i, j], pos*scale + offset, CoordinateTypeEnum.ABSOLUTE, depth, false, Color.White, 0, scale);
                }
            }

            foreach (IGameObject gameObject in GameObjects)
            {
                if (gameObject is Decoration)
                {
                    ((Decoration)gameObject).drawMap(offset, scale, depth);
                }
            }
        }

        #endregion

        /**
         * Returns whether or not there is an object here
         * @param x x
         * @param y y
         * @return a boolean
         */
        public bool objectAt(int x, int y, int w, int h)
        {
            if ((!TileSet.tileInfos[Tiles[x/TILE_WIDTH, y/TILE_HEIGHT]].passable))
            {
                return true;
            }
            // check if doodad at location
            for (int i = 0; i < GameObjects.Count; i++)
            {
                if (((ICollidable)GameObjects[i]).getCollider().Bounds.IntersectsWith(new DoubleRect(x-w/2, y-h/2, w, h)))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
