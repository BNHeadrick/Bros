using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CSharpQuadTree;
using CS8803AGA.engine;
using CS8803AGA.devices;
using CS8803AGA.collision;
using CS8803AGA.puzzle;

namespace CS8803AGA.controllers
{
    /// <summary>
    /// CharacterController for a player-controlled Character.
    /// Reads from inputs to update movement and animations.
    /// </summary>
    public class PlayerController : CharacterController
    {
        private static Brew ALL_BREW = new Brew(0, -1);

        public float dx { get; set; }
        public float dy { get; set; }

        public Vector2 getAbsPosVec()
        {
            return getMPos();
        }

        public PlayerController()
        {
            // nch, should only be called by CharacterController.construct
        }

        public override void update()
        {
            AnimationController.update();

            if (InputSet.getInstance().getButton(InputsEnum.CONFIRM_BUTTON))
            {
                handleInteract();
            }

            if (InputSet.getInstance().getButton(InputsEnum.BUTTON_1))
            {
                string dir = angleTo4WayAnimation(m_previousAngle);
                dir = "attack" + dir;
                AnimationController.requestAnimation(dir, AnimationController.AnimationCommand.Play);
            }

            dx = InputSet.getInstance().getLeftDirectionalX() * m_speed;
            dy = InputSet.getInstance().getLeftDirectionalY() * m_speed;

            if (dx == 0 && dy == 0)
            {
                return;
            }
            else
            {
                //testing for obtaining absolute x/y position
                //Console.WriteLine("dx " + dx + " dy" + dy + " pos x/y is " + getAbsPosVec().X + " " + getAbsPosVec().Y);
            }

            float angle =
                CommonFunctions.getAngle(new Vector2(dx, dy));

            string animName = angleTo4WayAnimation(angle);
            AnimationController.requestAnimation(animName, AnimationController.AnimationCommand.Play);

            if (true /* TODO - checks for paralysis, etc here */)
            {
                m_collider.handleMovement(new Vector2(dx, -dy));
            }

            m_previousAngle = angle;
        }

        /// <summary>
        /// Checks if the player is colliding with NPC
        /// </summary>
        public void checkNPCCollision()
        {
            // check for collision persistance
            if (m_collider.m_other != null)
            {
                if (!m_collider.Surroundings.IntersectsWith(m_collider.m_other.Surroundings))
                {
                    m_collider.m_other = null;
                }
                // we are still colliding so check for dialog
                else if (InputSet.getInstance().getButton(InputsEnum.BUTTON_4))
                {
                    if (m_collider.m_other.m_type == ColliderType.PC)
                    {
                        EngineManager.pushState(new EngineStateDialogue(Constants.COMPANION, (CharacterController)m_collider.m_other.m_owner, this, false));
                        ((CompanionController)m_collider.m_other.m_owner).learnNewInfo(CharacterController.currPlan);
                        ((CharacterController)m_collider.m_owner).brew.extract(ALL_BREW);
                        CharacterController.currPlan = null;
                    }
                    else
                    {
                        EngineManager.pushState(new EngineStateDialogue(((CharacterController)(m_collider.m_other.m_owner)).getDoodadIndex(), (CharacterController)m_collider.m_other.m_owner, this, false));
                    }
                }
            }
        }

        /// <summary>
        /// Sample of interaction - initiates dialogue.
        /// Perty hackish.
        /// </summary>
        private void handleInteract()
        {
            string facing = angleTo4WayAnimation(m_previousAngle);
            DoubleRect queryRectangle;
            if (facing == "up")
            {
                queryRectangle = new DoubleRect(
                    m_collider.Bounds.X, m_collider.Bounds.Y - 50, m_collider.Bounds.Width, 50);
            }
            else if (facing == "down")
            {
                queryRectangle = new DoubleRect(
                    m_collider.Bounds.X, m_collider.Bounds.Y + m_collider.Bounds.Height, m_collider.Bounds.Width, 50);
            }
            else if (facing == "left")
            {
                queryRectangle = new DoubleRect(
                    m_collider.Bounds.X - 50, m_collider.Bounds.Y + m_collider.Bounds.Height / 2 - 25, 50, 50);
            }
            else if (facing == "right")
            {
                queryRectangle = new DoubleRect(
                    m_collider.Bounds.X + m_collider.Bounds.Width, m_collider.Bounds.Y + m_collider.Bounds.Height / 2 - 25, 50, 50);
            }
            else
            {
                throw new Exception("Something aint right");
            }

            List<Collider> queries = m_collider.queryDetector(queryRectangle);
            foreach (Collider c in queries)
            {
                if (c != this.m_collider && c.m_type == ColliderType.NPC)
                {
                    EngineManager.pushState(new EngineStateDialogue(0, null, null, false));
                    InputSet.getInstance().setAllToggles();
                    return;
                }
            }
        }



    }
}
