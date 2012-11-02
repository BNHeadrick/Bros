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

        ActionNode currentGoal; /**< current goal in learnedPlan */

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
        }

        public override void update()
        {
            //if there are no plans to execute (i.e., if the plan execution fails due to lack of associated
            if (!executePlan())
            {
                followPlayer();
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
            //go to first node's location
            if (currentGoal == null)
            { // query the world to find out if there are any interactable objects
                List<PuzzleObject> objs = GameplayManager.ActiveArea.getPuzzleObjects((int)m_collider.Bounds.Center().X / Area.TILE_WIDTH, (int)m_collider.Bounds.Center().Y / Area.TILE_HEIGHT, (int)m_collider.Bounds.Width, (int)m_collider.Bounds.Height);
                int priority = 0;
                // choose highest priority object to interact with
                for (int i = 0; i < objs.Count; i++)
                {
                    int p = learnedPlan.findNodeDepth(objs[i]);
                    if (p != -1 && (currentGoal == null || p < priority))
                    {
                        currentGoal = learnedPlan.findNode(new ActionNode(objs[i]));
                        priority = p;
                    }
                }
            }
            if (currentGoal != null)
            {

            }

            return false;
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
            learnedPlan.merge(newInfo);
        }

    }
}
