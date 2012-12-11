using Microsoft.Xna.Framework;
using System;
using CS8803AGAGameLibrary;
using CS8803AGA.collision;
using CS8803AGA.engine;
using CS8803AGA.actions;
using CS8803AGA;
using CS8803AGA.puzzle;
using CS8803AGA.learning;
using CS8803AGA.devices;
using System.Collections.Generic;
using CS8803AGA.Knowledge;

namespace CS8803AGA.controllers
{
    public class CharacterController : ICollidable
    {
        public int Health { get; set; }
        public AnimationController AnimationController { get; protected set; }

        public Vector2 m_position;
        public Collider m_collider;
        public int m_speed;

        protected float m_previousAngle;

        private int m_doodadIndex;
        public Bouncer bouncer;
        public Brew brew;

        private int victim;
        private SGame social_game;

        public static Brew ALL_BREW = new Brew(0, -1);

        static public ActionNode currPlan;

        // taken from companion controller, pathing variables
        private bool walking;
        private int walk_target;
        private int walk_dir;
        private int distance;
        ////////////////////////

        public int getDoodadIndex()
        {
            return m_doodadIndex;
        }

        public void setDoodadIndex(int doodadIndex)
        {
            m_doodadIndex = doodadIndex;
        }

        /// <summary>
        /// Factory method to create CharacterControllers
        /// </summary>
        /// <param name="ci">Information about character apperance and stats</param>
        /// <param name="startpos">Where in the Area the character should be placed</param>
        /// <param name="playerControlled">True if the character should be a PC, false if NPC</param>
        /// <returns>Constructed CharacterController</returns>
        public static CharacterController construct(CharacterInfo ci, Vector2 startpos, Constants.CharType typeOfChar, PlayerController p)
        {
            CharacterController cc;
            cc = new CharacterController();
            cc.m_doodadIndex = 0;
            ColliderType type;
            if (typeOfChar == Constants.CharType.PLAYERCHAR)
            {
                cc = new PlayerController();
                type = ColliderType.PC;

                cc.m_doodadIndex = Constants.PLAYER;
                cc.bouncer = null;
                cc.brew = new Brew(0, 0);

                currPlan = null;// new ActionNode(ActionNode.EMPTY);
            }
            else if (typeOfChar == Constants.CharType.NPCHAR)
            {
                type = ColliderType.NPC;

                cc.bouncer = null;
                
                cc.brew = null;
            }
            else
            {
                cc = new CompanionController(p);
                type = ColliderType.PC;
                cc.m_doodadIndex = Constants.COMPANION;

                cc.bouncer = null;
                cc.brew = new Brew(0, 0);
            }

            
            cc.m_position = startpos;

            cc.AnimationController = new AnimationController(ci.animationDataPath, ci.animationTexturePath);
            cc.AnimationController.ActionTriggered += new ActionTriggeredEventHandler(cc.handleAction);
            cc.AnimationController.Scale = ci.scale;

            Rectangle bounds = ci.collisionBox;
            bounds.Offset((int)cc.m_position.X, (int)cc.m_position.Y);
            cc.m_collider = new Collider(cc, bounds, type);

            cc.m_speed =  ci.speed;

            //if(PCControllerInput.

            cc.m_previousAngle = (float)Math.PI / 2;

            cc.victim = -1;
            cc.social_game = null;

            cc.walking = false;
            cc.walk_target = -1;
            cc.walk_dir = -1;
            cc.distance = 0;

            return cc;
        }

        /// <summary>
        /// Factory method to construct non-player character controllers
        /// See other construct() method for more details
        /// </summary>
        public static CharacterController construct(CharacterInfo ci, Vector2 startpos)
        {
            return construct(ci, startpos, Constants.CharType.NPCHAR, null);
        }

        /// <summary>
        /// Protected ctor - use the construct() method
        /// </summary>
        protected CharacterController()
        {
            // protected so we have to use the factory
            // we want the factory so that later we can store subclass info in CharacterInfo
            //  and then have the factory instantiate the subclass we want
            Health = 2;
        }

        public virtual bool update()
        {
            if (GameplayManager.ActiveArea.GlobalLocation == Area.PARTY)
            {
                if (victim != -1 && social_game != null)
                {
                    // path to the victim
                    if (!walking)
                    {
                        // check if adjacent to goal
                        int x = (int)m_collider.Bounds.Center().X / Area.TILE_WIDTH;
                        int y = (int)m_collider.Bounds.Center().Y / Area.TILE_HEIGHT;
                        if (walk_target == -1)
                        {
                            //Console.WriteLine("victim= " + victim);
                            walk_target = GameplayManager.ActiveArea.getObjectLocation(victim);
                            //Console.WriteLine("walk_target= " + walk_target);
                        }
                        int gx = walk_target % Area.WIDTH_IN_TILES;
                        int gy = walk_target / Area.WIDTH_IN_TILES;
                        if (x + y * Area.WIDTH_IN_TILES == walk_target)//(gx - x) * (gx - x) + (gy - y) * (gy - y) <= 1)
                        { // adjacent
                            // interact!
                            EngineManager.pushState(new EngineStateSocialGame(getDoodadIndex(), victim, social_game));
                            walk_target = -1;
                            victim = -1;
                            social_game = null;
                            return true;
                        }
                        else
                        {
                            // need to walk to
                            walk_dir = GameplayManager.ActiveArea.startPath(x + y * Area.WIDTH_IN_TILES, walk_target, PuzzleObject.TYPE_NONE, victim, Area.TILE_WIDTH-2, Area.TILE_HEIGHT-2);
                            //Console.WriteLine("path: " + walk_dir);
                            if (walk_dir == -2)
                            { // bad, but, at location
                                EngineManager.pushState(new EngineStateSocialGame(getDoodadIndex(), victim, social_game));
                                walk_target = -1;
                                victim = -1;
                                social_game = null;
                                return true;
                            }
                            else if (walk_dir != -1)
                            {
                                walking = true;
                                m_collider.handleMovement(new Vector2(-(float)m_collider.m_bounds.X + x * Area.TILE_WIDTH + (Area.TILE_WIDTH - (int)m_collider.Bounds.Width) / 2,  - (float)m_collider.m_bounds.Y + y * Area.TILE_HEIGHT + (Area.TILE_HEIGHT - (int)m_collider.Bounds.Height) / 2));
                            }
                            else
                            { // need to pick another action / can't reach this one
                                victim = -1;
                                social_game = null;
                                return true;
                            }
                        }
                    }
                    else
                    { // walk torwards this thingy
                        int travel = m_speed*2;
                        int size = walk_dir < CompanionController.WALK_UP ? Area.TILE_WIDTH : Area.TILE_HEIGHT;
                        if (distance + travel > size)
                        {
                            travel = (walk_dir < CompanionController.WALK_UP ? Area.TILE_WIDTH : Area.TILE_HEIGHT) - distance;
                            distance = 0;
                            walking = false;
                        }
                        else
                        {
                            distance += travel;
                        }

                        m_collider.handleMovement(new Vector2(walk_dir == CompanionController.WALK_LEFT ? -travel : walk_dir == CompanionController.WALK_RIGHT ? travel : 0, walk_dir == CompanionController.WALK_UP ? -travel : walk_dir == CompanionController.WALK_DOWN ? travel : 0));

                        AnimationController.requestAnimation(walk_dir == CompanionController.WALK_LEFT ? "left" : walk_dir == CompanionController.WALK_RIGHT ? "right" : walk_dir == CompanionController.WALK_UP ? "up" : "down", AnimationController.AnimationCommand.Play);
                        AnimationController.update();
                    }
                }
                else
                {
                    // pick a social game to play
                    List<int> potential_players = GameplayManager.ActiveArea.getPartiers((int)m_collider.m_bounds.Center().X, (int)m_collider.m_bounds.Center().Y, (int)m_collider.m_bounds.Width, (int)m_collider.m_bounds.Height);
                    int total = 0;
                    List<List<SGame>> games = new List<List<SGame>>();
                    for (int i = 0; i < potential_players.Count; i++)
                    {
                        List<SGame> sgames = SocialGames.getAllGames(getDoodadIndex(), potential_players[i]);
                        for (int j = 0; j < sgames.Count; j++)
                        {
                            total += sgames[j].ssR.Count + 1;
                        }
                        games.Add(sgames);
                    }
                    int game = (new Random()).Next(total);


                    for (int i = 0; i < games.Count; i++)
                    {
                        for (int j = 0; j < games[i].Count; j++)
                        {
                            game -= games[i][j].ssR.Count + 1;
                            if (game < 0)
                            {
                                victim = potential_players[i];
                                social_game = games[i][j];
                                break;
                            }
                        }
                        if (game < 0)
                        {
                            break;
                        }
                    }

                    if (victim == -1 && social_game == null)
                    {
                        return true;
                    }
                    //EngineManager.pushState(new EngineStateSocialGame(getDoodadIndex(), victim, social_game));
                }
                return false;
            }
            return true;
        }

        public virtual void draw()
        {
            float depth = GameplayManager.ActiveArea.getDrawDepth(this.m_collider.Bounds);
            AnimationController.draw(m_position, depth);
        }

        /// <summary>
        /// Converts an angle in radians to a direction left,right,up,down
        /// </summary>
        /// <param name="angle">Angle in radians, where 0 = right</param>
        /// <returns>Left, right, up, or down</returns>
        public virtual string angleTo4WayAnimation(float angle)
        {
            angle += MathHelper.PiOver4;
            angle += MathHelper.Pi;
            if (angle > MathHelper.TwoPi) angle -= MathHelper.TwoPi;
            angle *= 4.00f / MathHelper.TwoPi;
            angle -= 0.001f;
            int angleInt = (int)angle;
            switch (angleInt)
            {
                case 0: return "left";
                case 1: return "down";
                case 2: return "right";
                case 3: return "up";
                default: throw new Exception("Math is wrong");
            }
        }

        /// <summary>
        /// Converts an angle in radians to an 8-way direction
        /// </summary>
        /// <param name="angle">Angle in radians, where 0 = right</param>
        /// <returns>Left, right, up, down, upleft, upright, downleft, or downright</returns>
        protected virtual string angleTo8WayAnimation(float angle)
        {
            // complicated.. essentially takes angle and maps to 8 directions
            angle += MathHelper.PiOver4 / 2; // adjust so ranges don't wrap around -pi
            angle += MathHelper.Pi; // shift ranges to 0-TwoPi
            if (angle > MathHelper.TwoPi) angle -= MathHelper.TwoPi; // fix edge case
            angle *= 8.00f / MathHelper.TwoPi;
            angle -= 0.001f;
            int angleInt = (int)angle;
            switch (angleInt)
            {
                case 0: return "left";
                case 1: return "downleft";
                case 2: return "down";
                case 3: return "downright";
                case 4: return "right";
                case 5: return "upright";
                case 6: return "up";
                case 7: return "upleft";
                default: throw new Exception("Math is wrong");
            }
        }

        /// <summary>
        /// Handles actions passed to it from its Animation Controller
        /// </summary>
        /// <param name="sender">Object sending the action, probably AnimationController</param>
        /// <param name="action">The action itself</param>
        protected void handleAction(object sender, IAction action)
        {
            action.execute(this);
        }

        #region Collider Members

        public Collider getCollider()
        {
            return m_collider;
        }

        public Vector2 getMPos()
        {
            return m_position;
        }

        public Vector2 DrawPosition
        {
            get
            {
                return m_position;
            }
            set
            {
                m_position = value;
            }
        }

        #endregion

        #region IGameObject Members

        public bool isAlive()
        {
            return Health > 0;
        }

        #endregion

        public void addTaskToPlan(PuzzleObject po)
        {
            if (currPlan == null)
            {
                currPlan = new ActionNode(po);
            }
            else
            {
                currPlan.addLeaf(new ActionNode(po));
            }
            currPlan.debugPrint();
        }

        public void clearPlan()
        {
            currPlan = null;
            //currPlan = new ActionNode(ActionNode.EMPTY);
        }

    }
}