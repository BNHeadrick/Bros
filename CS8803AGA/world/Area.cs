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

namespace CS8803AGA
{
    /// <summary>
    /// A portion of the game world; exactly one is visible on the monitor at a time
    /// </summary>
    public class Area
    {
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
            Console.WriteLine("making location: " + location);

            Area a = makeEmptyArea(tileSetpath, location);

            DecorationSet ds1 = DecorationSet.construct(@"World/graveyard");
            DecorationSet ds2 = DecorationSet.construct(@"World/town");
            DecorationSet ds3 = DecorationSet.construct(@"World/trees");
            string[] doodadIndex = {// graveyard
                                    /*0 => */ "NONE",      /*1 => */"tree1",      /*2 => */"tree2", 
                                    /*3 => */"tombstone1", /*4 => */"tombstone2", /*5 => */"tombstone3", 
                                    /*6 => */"tombstone4", /*7 => */"obelisk",    /*8 => */ "bigstone1", 
                                    /*9 => */"bigstone2",
                                    // town
                                    /*10=> */"house1",
                                    // trees
                                    /*11=> */"tree1",     /*12=> */"tree2",
                                    // characters
                                    /*13=> */"DarkKnight",/*14=> */"Jason",       /*15=> */"Ness",
                                    /*16=> */"Salsa",
                                    "NONE", "NONE", "NONE", "NONE", 
                                    //ADULT
                                    /*21=> */"Adult1", /*22=> */"Adult2", /*23=> */"Adult3", /*24=> */"Adult4", 
                                    /*25=> */"Adult5", /*26=> */"Adult6", /*27=> */"Adult7", /*28=> */"Adult8", 
                                    "NONE", "NONE",
                                    //ANIMAL
                                    /*31=> */"Animal1", /*32=> */"Animal2", /*33=> */"Animal3", /*34=> */"Animal4", 
                                    /*35=> */"Animal5", /*36=> */"Animal6", /*37=> */"Animal7", /*38=> */"Animal8", 
                                    "NONE", "NONE", 
                                    //Animal2
                                    /*41=> */"Animal21", /*42=> */"Animal22", /*43=> */"Animal23", /*44=> */"Animal24", 
                                    "NONE", "NONE", "NONE", "NONE", 
                                    "NONE", "NONE",
                                    //Boss
                                    /*51=> */"Boss1", /*52=> */"Boss2", /*53=> */"Boss3", /*54=> */"Boss4", 
                                    /*55=> */"Boss5", /*56=> */"Boss6", /*57=> */"Boss7", /*58=> */"Boss8", 
                                    "NONE", "NONE", 
                                    //Boss2
                                    /*61=> */"Boss21", /*62=> */"Boss22", /*63=> */"Boss23", /*64=> */"Boss24", 
                                    "NONE", "NONE", "NONE", "NONE", 
                                    "NONE", "NONE",
                                    //BunnyGirl2
                                    /*71=> */"BunnyGirl1", /*72=> */"BunnyGirl2", /*73=> */"BunnyGirl3", /*74=> */"BunnyGirl24", 
                                    "NONE", "NONE", "NONE", "NONE", 
                                    "NONE", "NONE",
                                    //Child
                                    /*81=> */"Child1", /*82=> */"Child2", /*83=> */"Child3", /*84=> */"Child4", 
                                    /*85=> */"Child5", /*86=> */"Child6", /*87=> */"Child7", /*88=> */"Child8", 
                                    "NONE", "NONE", 
                                    //Church
                                    /*91=> */"Church1", /*92=> */"Church2", /*93=> */"Church3", /*94=> */"Church4", 
                                    /*95=> */"Church5", /*96=> */"Church6", /*97=> */"Church7", /*98=> */"Church8", 
                                    "NONE", "NONE", 
                                    //Cook2
                                    /*101=> */"Cook1", /*102=> */"Cook2", /*103=> */"Cook3", /*104=> */"Cook4", 
                                    "NONE", "NONE", "NONE", "NONE", 
                                    "NONE", "NONE",
                                    //Dragon
                                    /*111=> */"Dragon1", /*112=> */"Dragon2", /*113=> */"Dragon3", /*114=> */"Dragon4", 
                                    /*115=> */"Dragon5", /*116=> */"Dragon6", /*117=> */"Dragon7", /*118=> */"Dragon8", 
                                    "NONE", "NONE", 
                                    //Elve
                                    /*121=> */"Elve1", /*122=> */"Elve2", /*123=> */"Elve3", /*124=> */"Elve4", 
                                    /*125=> */"Elve5", /*126=> */"Elve6", /*127=> */"Elve7", /*128=> */"Elve8", 
                                    "NONE", "NONE", 
                                    //Employee
                                    /*131=> */"Employee1", /*132=> */"Employee2", /*133=> */"Employee3", /*134=> */"Employee4", 
                                    /*135=> */"Employee5", /*136=> */"Employee6", /*137=> */"Employee7", /*138=> */"Employee8", 
                                    "NONE", "NONE", 
                                    //Fairy2
                                    /*141=> */"Fairy1", /*142=> */"Fairy2", /*143=> */"Fairy3", /*144=> */"Fairy4", 
                                    "NONE", "NONE", "NONE", "NONE", 
                                    "NONE", "NONE",
                                    //Fighter
                                    /*151=> */"Fighter1", /*152=> */"Fighter2", /*153=> */"Fighter3", /*154=> */"Fighter4", 
                                    /*155=> */"Fighter5", /*156=> */"Fighter6", /*157=> */"Fighter7", /*158=> */"Fighter8", 
                                    "NONE", "NONE", 
                                    //Flame2
                                    /*161=> */"Flame1", /*162=> */"Flame2", /*163=> */"Flame3", /*164=> */"Flame4", 
                                    "NONE", "NONE", "NONE", "NONE", 
                                    "NONE", "NONE",
                                    //Hero
                                    /*171=> */"Hero1", /*172=> */"Hero2", /*173=> */"Hero3", /*174=> */"Hero4", 
                                    /*175=> */"Hero5", /*176=> */"Hero6", /*177=> */"Hero7", /*178=> */"Hero8", 
                                    "NONE", "NONE",
                                    //Machine
                                    /*181=> */"Machine1", /*182=> */"Machine2", /*183=> */"Machine3", /*184=> */"Machine4", 
                                    /*185=> */"Machine5", /*186=> */"Machine6", /*187=> */"Machine7", /*188=> */"Machine8", 
                                    "NONE", "NONE",
                                    //Mage
                                    /*191=> */"Mage1", /*192=> */"Mage2", /*193=> */"Mage3", /*194=> */"Mage4", 
                                    /*195=> */"Mage5", /*196=> */"Mage6", /*197=> */"Mage7", /*198=> */"Mage8", 
                                    "NONE", "NONE",
                                    //Merchant2
                                    /*201=> */"Merchant1", /*202=> */"Merchant2", /*203=> */"Merchant3", /*204=> */"Merchant4", 
                                    "NONE", "NONE", "NONE", "NONE", 
                                    "NONE", "NONE",
                                    //Monster
                                    /*211=> */"Monster1", /*212=> */"Monster2", /*213=> */"Monster3", /*214=> */"Monster4", 
                                    /*215=> */"Monster5", /*216=> */"Monster6", /*217=> */"Monster7", /*218=> */"Monster8", 
                                    "NONE", "NONE",
                                    //Monster2
                                    /*221=> */"Monster21", /*222=> */"Monster22", /*223=> */"Monster23", /*224=> */"Monster24", 
                                    /*225=> */"Monster25", /*226=> */"Monster26", /*227=> */"Monster27", /*228=> */"Monster28", 
                                    "NONE", "NONE",
                                    //Monster3
                                    /*231=> */"Monster31", /*232=> */"Monster32", /*233=> */"Monster33", /*234=> */"Monster34", 
                                    /*235=> */"Monster35", /*236=> */"Monster36", /*237=> */"Monster37", /*238=> */"Monster38", 
                                    "NONE", "NONE",
                                    //NekoGirl2
                                    /*241=> */"NekoGirl1", /*242=> */"NekoGirl2", /*243=> */"NekoGirl3", /*244=> */"NekoGirl4", 
                                    "NONE", "NONE", "NONE", "NONE", 
                                    "NONE", "NONE",
                                    //Old
                                    /*251=> */"Old1", /*252=> */"Old2", /*253=> */"Old3", /*254=> */"Old4", 
                                    /*255=> */"Old5", /*256=> */"Old6", /*257=> */"Old7", /*258=> */"Old8", 
                                    "NONE", "NONE",
                                    //Pirate
                                    /*261=> */"Pirate1", /*262=> */"Pirate2", /*263=> */"Pirate3", /*264=> */"Pirate4", 
                                    /*265=> */"Pirate5", /*266=> */"Pirate6", /*267=> */"Pirate7", /*268=> */"Pirate8", 
                                    "NONE", "NONE",
                                    //Royal
                                    /*271=> */"Royal1", /*272=> */"Royal2", /*273=> */"Royal3", /*274=> */"Royal4", 
                                    /*275=> */"Royal5", /*276=> */"Royal6", /*277=> */"Royal7", /*278=> */"Royal8", 
                                    "NONE", "NONE",
                                    //Royal2
                                    /*281=> */"Royal21", /*282=> */"Royal22", /*283=> */"Royal23", /*284=> */"Royal24", 
                                    /*285=> */"Royal25", /*286=> */"Royal26", /*287=> */"Royal27", /*288=> */"Royal28", 
                                    "NONE", "NONE",
                                    //Seer2
                                    /*291=> */"Seer1", /*292=> */"Seer2", /*293=> */"Seer3", /*294=> */"Seer4", 
                                    "NONE", "NONE", "NONE", "NONE", 
                                    "NONE", "NONE",
                                    //Soldier
                                    /*301=> */"Soldier1", /*302=> */"Soldier2", /*303=> */"Soldier3", /*304=> */"Soldier4", 
                                    /*305=> */"Soldier5", /*306=> */"Soldier6", /*307=> */"Soldier7", /*308=> */"Soldier8", 
                                    "NONE", "NONE",
                                    //Student
                                    /*311=> */"Student1", /*312=> */"Student2", /*313=> */"Student3", /*314=> */"Student4", 
                                    /*315=> */"Student5", /*316=> */"Student6", /*317=> */"Student7", /*318=> */"Student8", 
                                    "NONE", "NONE",
                                    //Template
                                    /*321=> */"Template1", "NONE", "NONE", "NONE", 
                                    "NONE", "NONE", "NONE", "NONE",  
                                    "NONE", "NONE",
                                    //Thief
                                    /*331=> */"Thief1", /*332=> */"Thief2", /*333=> */"Thief3", /*334=> */"Thief4", 
                                    /*335=> */"Thief5", /*336=> */"Thief6", /*337=> */"Thief7", /*338=> */"Thief8", 
                                    "NONE", "NONE",
                                    //Vehicle
                                    /*341=> */"Vehicle1", /*342=> */"Vehicle2", /*343=> */"Vehicle3", /*344=> */"Vehicle4", 
                                    /*345=> */"Vehicle5", /*346=> */"Vehicle6", /*347=> */"Vehicle7", /*348=> */"Vehicle8", 
                                    "NONE", "NONE",
                                    //Vehicle2
                                    /*351=> */"Vehicle21", /*352=> */"Vehicle22", /*353=> */"Vehicle23", /*354=> */"Vehicle24", 
                                    "NONE", "NONE", "NONE", "NONE", 
                                    "NONE", "NONE",
                                    //Warrior
                                    /*361=> */"Warrior1", /*362=> */"Warrior2", /*363=> */"Warrior3", /*364=> */"Warrior4", 
                                    /*365=> */"Warrior5", /*366=> */"Warrior6", /*367=> */"Warrior7", /*368=> */"Warrior8", 
                                    "NONE", "NONE",
                                    //Young
                                    /*371=> */"Young1", /*372=> */"Young2", /*373=> */"Young3", /*374=> */"Young4", 
                                    /*375=> */"Young5", /*376=> */"Young6", /*377=> */"Young7", /*378=> */"Young8", 
                                    "NONE", "NONE",
                                   };
            int[,] doodads = AreaDefinitions.doodadsAt(location);
            for (int i = 0; i < WIDTH_IN_TILES; i++)
            {
                for (int j = 0; j < HEIGHT_IN_TILES; j++)
                {
                    if (doodads[i, j] >= 13)
                    {
                        Console.WriteLine("doodad: " + doodads[i, j] + "x"+doodadIndex[doodads[i, j]]);
                        CharacterInfo ci = GlobalHelper.loadContent<CharacterInfo>(@"Characters/"+doodadIndex[doodads[i, j]]);
                        Vector2 pos = new Vector2(a.getTileRectangle(i, j).X, a.getTileRectangle(i, j).Y);
                        CharacterController cc = CharacterController.construct(ci, pos);
                        a.add(cc);
                    }
                    else if (doodads[i, j] != 0)
                    {
                        Vector2 pos = new Vector2(a.getTileRectangle(i, j).X, a.getTileRectangle(i, j).Y);
                        Decoration d;
                        if (doodads[i, j] < 10)
                        {
                            d = ds1.makeDecoration(doodadIndex[doodads[i, j]], pos);
                        }
                        else if (doodads[i, j] < 11)
                        {
                            d = ds2.makeDecoration(doodadIndex[doodads[i, j]], pos);
                        }
                        else
                        {
                            d = ds3.makeDecoration(doodadIndex[doodads[i, j]], pos);
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

    }
}
