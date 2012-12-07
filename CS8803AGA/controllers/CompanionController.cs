using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CSharpQuadTree;
using CS8803AGA.engine;
using CS8803AGA.devices;
using CS8803AGA.collision;
using CS8803AGA.questcontent;
using CS8803AGA.learning;
using CS8803AGA.puzzle;

namespace CS8803AGA.controllers
{
    /// <summary>
    /// CharacterController for a player-controlled Character.
    /// Reads from inputs to update movement and animations.
    /// </summary>
    public class CompanionController : CharacterController
    {
        ActionNode learnedPlan;

        private ActionNode currentGoal; /**< current goal in learnedPlan */
        private int currentGoalID;
        public List<int> invalids;
        private int walk_target; /**< where to twlk to */
        private bool walking; /**< where to walk to */
        private bool interacting; /**< whether interact*/
        private int walk_dir; /**< what dir to walk */
        private int distance; /**< how far traveled */

        //private int last_walk_target;

        private bool is_init;
        private int width;
        private int height;
        private int boundsX;
        private int boundsY;

        public const int WALK_LEFT = 0;
        public const int WALK_RIGHT = 1;
        public const int WALK_UP = 2;
        public const int WALK_DOWN = 3;

        int compSpeed;
        bool isLearning;
        public float dx { get; set; }
        public float dy { get; set; }

        private PlayerController player;

        public Vector2 getAbsPosVec()
        {
            return getMPos();
        }

        public void setAbsPos(Vector2 pos)
        {
            m_position = pos;
        }

        public CompanionController(PlayerController p)
        {
            // nch, should only be called by CharacterController.construct
            player = p;
            isLearning = false;
            learnedPlan = new ActionNode(ActionNode.EMPTY);
            currentGoal = null;
            currentGoalID = -1;
            walking = false;
            distance = 0;
            walk_dir = 0;
            walk_target = -1;
            interacting = false;
            invalids = new List<int>();

            is_init = false;

            //last_walk_target = -1;

            ///debug
            //learnedPlan.addLeaf(new ActionNode(new Brew(Brew.COLOR_RED, 0)));
        }

        public override bool update()
        {
            if (GameplayManager.ActiveArea.GlobalLocation == Area.PARTY)
            {
                return base.update();
            }
            else
            {
                //if there are no plans to execute (i.e., if the plan execution fails due to lack of associated
                if (!executePlan())
                {
                    followPlayer();
                }
                return true;
            }
        }

        private void followPlayer()
        {
            compSpeed = 0;
            if (Quest.talkedToCompanion)
            {
                AnimationController.update();
            }

            Vector2 normPlayPos = player.getAbsPosVec();

            normPlayPos.Normalize();
            double chaseAngle = 0.0;

            if (((Math.Abs(player.getAbsPosVec().X - getAbsPosVec().X) > 50) ||
                (Math.Abs(player.getAbsPosVec().Y - getAbsPosVec().Y) > 50)))
            {
                dx = (player.getAbsPosVec().X - this.getAbsPosVec().X);
                dy = (player.getAbsPosVec().Y - this.getAbsPosVec().Y);

                chaseAngle = Math.Atan2(player.getAbsPosVec().Y - this.getAbsPosVec().Y,
                    player.getAbsPosVec().X - this.getAbsPosVec().X);
            }

            if ((Math.Abs(player.getAbsPosVec().X - getAbsPosVec().X) < 50) &&
                (Math.Abs(player.getAbsPosVec().Y - getAbsPosVec().Y) < 50))
            {
                return;
            }

            float angle = (float)chaseAngle;

            string animName = angleTo4WayAnimation(-angle);
            AnimationController.requestAnimation(animName, AnimationController.AnimationCommand.Play);

            if (Quest.talkedToCompanion)
            {
                compSpeed = m_speed;
            }
            m_collider.handleMovement(AngleToVector((float)chaseAngle));


            m_previousAngle = angle;
        }

        private bool executePlan()
        {
            if (!is_init)
            {
                width = (int)m_collider.Bounds.Width;
                height = (int)m_collider.Bounds.Height;
                boundsX = (int)m_collider.Bounds.X;
                boundsY = (int)m_collider.Bounds.Y;
                is_init = true;
            }

            if (Quest.talkedToCompanion)
            {
                //go to first node's location
                if (currentGoal == null)
                { // query the world to find out if there are any interactable objects
                    List<PuzzleObject> objs = GameplayManager.ActiveArea.getPuzzleObjects((int)m_collider.Bounds.Center().X / Area.TILE_WIDTH, (int)m_collider.Bounds.Center().Y / Area.TILE_HEIGHT, (int)m_collider.Bounds.Width, (int)m_collider.Bounds.Height, brew);
                    int priority = 0;

                    // choose highest priority object to interact with
                    for (int i = 0; i < objs.Count; i++)
                    {
                        if (!invalids.Contains(objs[i].id))
                        {
                            int p = learnedPlan.findNodeDepth(objs[i]);
                            if (p != -1 && (currentGoal == null || p < priority))
                            {
                                currentGoal = learnedPlan.findNode(new ActionNode(objs[i]));
                                currentGoalID = objs[i].id;
                                priority = p;
                            }
                        }
                    }
                    if (currentGoal != null)
                    {
                        currentGoal.value++;
                        //invalids.Clear();
                        invalids.Add(currentGoalID);
                        Console.WriteLine("doing: " + currentGoal.id);
                        interacting = true;
                        walk_target = -1;
                    }
                }
                if (currentGoal != null)
                {
                    if (interacting)
                    {
                        if (!walking)
                        {
                            // check if adjacent to goal
                            int x = (int)m_collider.Bounds.Center().X / Area.TILE_WIDTH;
                            int y = (int)m_collider.Bounds.Center().Y / Area.TILE_HEIGHT;
                            if (walk_target == -1)
                            {
                                walk_target = GameplayManager.ActiveArea.getObjectLocation(currentGoalID);
                                Console.WriteLine("walk to: " + (walk_target) % Area.WIDTH_IN_TILES + "x" + (walk_target / Area.WIDTH_IN_TILES));
                            }
                            int gx = walk_target % Area.WIDTH_IN_TILES;
                            int gy = walk_target / Area.WIDTH_IN_TILES;
                            if (x+y*Area.WIDTH_IN_TILES == walk_target)//(gx - x) * (gx - x) + (gy - y) * (gy - y) <= 1)
                            { // adjacent
                                // interact!
                                GameplayManager.ActiveArea.interact(this, currentGoalID);
                                interacting = false;
                                walk_target = -1;
                            }
                            else
                            {
                                // need to walk to
                                walk_dir = GameplayManager.ActiveArea.startPath(x + y * Area.WIDTH_IN_TILES, walk_target, currentGoal.getData().type, currentGoalID, width, height);
                                //Console.WriteLine("path: " + walk_dir);
                                if (walk_dir == -2)
                                { // bad, but, at location
                                    GameplayManager.ActiveArea.interact(this, currentGoalID);
                                    interacting = false;
                                    walk_target = -1;
                                }
                                else if (walk_dir != -1)
                                {
                                    walking = true;
                                    // reset to center of square
                                    //m_collider.m_bounds.X = x * Area.TILE_WIDTH + (Area.TILE_WIDTH - width) / 2;
                                    //m_collider.m_bounds.Y = y * Area.TILE_HEIGHT + (Area.TILE_HEIGHT - height) / 2;

                                    //Console.WriteLine(m_collider.m_bounds.X + "x" + m_collider.m_bounds.Y);
                                    //Console.WriteLine((x * Area.TILE_WIDTH + (Area.TILE_WIDTH - width) / 2) + "x" + (3+y * Area.TILE_HEIGHT + (Area.TILE_HEIGHT - height) / 2));

                                    m_collider.handleMovement(new Vector2(-(float)m_collider.m_bounds.X + x * Area.TILE_WIDTH + (Area.TILE_WIDTH - width) / 2, 11 - (float)m_collider.m_bounds.Y + y * Area.TILE_HEIGHT + (Area.TILE_HEIGHT - height) / 2));
                                }
                                else
                                { // need to pick another action / can't reach this one
                                    Console.WriteLine(walk_target + " is impossible to reach");
                                    currentGoal = null;
                                    return false;
                                }
                            }
                        }
                        else
                        { // walk torwards this thingy
                            int travel = m_speed;
                            int size = walk_dir < WALK_UP ? Area.TILE_WIDTH : Area.TILE_HEIGHT;
                            int reservation = size;
                            if (distance + travel > size)
                            {
                                travel = (walk_dir < WALK_UP ? Area.TILE_WIDTH : Area.TILE_HEIGHT) - distance;
                                distance = 0;
                                walking = false;
                                reservation = 0;

                                boundsX = (int)m_collider.Bounds.X;
                                boundsY = (int)m_collider.Bounds.Y;
                            }
                            else
                            {
                                distance += travel;
                                reservation -= distance;
                            }
                            // reserve space
                            /*switch (walk_dir)
                            {
                                case WALK_LEFT:
                                    if (reservation != 0)
                                    {
                                        m_collider.m_bounds.X = boundsX - Area.TILE_WIDTH - travel;
                                    }
                                    m_collider.m_bounds.Width = width + reservation;
                                    break;
                                case WALK_RIGHT:
                                    m_collider.m_bounds.Width = width + reservation;
                                    break;
                                case WALK_UP:
                                    if (reservation != 0)
                                    {
                                        m_collider.m_bounds.Y = boundsY - Area.TILE_HEIGHT - travel;
                                    }
                                    m_collider.m_bounds.Height = height + reservation;
                                    break;
                                case WALK_DOWN:
                                    m_collider.m_bounds.Height = height + reservation;
                                    break;
                            }*/

                            m_collider.handleMovement(new Vector2(walk_dir == WALK_LEFT ? -travel : walk_dir == WALK_RIGHT ? travel : 0, walk_dir == WALK_UP ? -travel : walk_dir == WALK_DOWN ? travel : 0));

                            AnimationController.requestAnimation(walk_dir == WALK_LEFT ? "left" : walk_dir == WALK_RIGHT ? "right" : walk_dir == WALK_UP ? "up" : "down", AnimationController.AnimationCommand.Play);
                            AnimationController.update();
                        }
                    }
                    if (!interacting)
                    {
                        // find new goal
                        ActionNode next = null;
                        List<PuzzleObject> objs = GameplayManager.ActiveArea.getPuzzleObjects((int)m_collider.Bounds.Center().X / Area.TILE_WIDTH, (int)m_collider.Bounds.Center().Y / Area.TILE_HEIGHT, (int)m_collider.Bounds.Width, (int)m_collider.Bounds.Height, brew);
                        int priority = 0;
                        int nextID = -1;
                        // choose highest priority object to interact with
                        for (int i = 0; i < objs.Count; i++)
                        {
                            if (!invalids.Contains(objs[i].id))
                            {
                                int p = currentGoal.findNodeDepth(objs[i]);
                                if (p != -1 && (next == null || p < priority))
                                {
                                    next = currentGoal.findNode(new ActionNode(objs[i]));
                                    nextID = objs[i].id;
                                    priority = p;
                                }
                            }
                        }
                        /*int next_walk_target = -1;
                        if (nextID != -1)
                        {
                            next_walk_target = GameplayManager.ActiveArea.getObjectLocation(nextID);
                        }*/
                        if (next != null)// && last_walk_target != next_walk_target)
                        {
                            currentGoal = next;
                            currentGoal.value++;
                            Console.WriteLine("doing: " + currentGoal.id);
                            currentGoalID = nextID;
                            walk_target = -1;
                            interacting = true;
                            invalids.Add(currentGoalID);

                            //last_walk_target = next_walk_target;
                        }
                        else
                        {
                            // check if this is another step series we can do
                            /*for (int i = 0; i < objs.Count; i++)
                            {
                                int p = learnedPlan.findNodeDepth(objs[i], doneGoals);
                                if (p != -1 && (next == null || p < priority))
                                {
                                    currentGoal = learnedPlan.findNode(new ActionNode(objs[i]));
                                    nextID = objs[i].id;
                                    priority = p;
                                }
                            }
                            if (next != null)
                            {
                                currentGoal = next;
                                Console.WriteLine("re-doing: " + currentGoal.id);
                                doneGoals.Add(currentGoal.id);
                                currentGoalID = nextID;
                                walk_target = -1;
                                interacting = true;
                            }*/
                            currentGoal = null;
                            currentGoalID = -1;
                        }
                    }
                }
            }
            return (currentGoal != null);
        }

        Vector2 AngleToVector(float angle)
        {
            return new Vector2((float)Math.Cos(angle) * compSpeed, (float)Math.Sin(angle) * compSpeed);
        }

        public void setLearning(bool learning){
            isLearning = learning;
        }

        /*
        public void toggleLearning()
        {
            isLearning = !isLearning;
        }
        */

        public void learnNewInfo(ActionNode newInfo){
            if (newInfo != null)
            {
                learnedPlan.merge(newInfo);
            }
            currentGoal = null;
            invalids.Clear();
            walk_target = -1;
        }

    }
}
