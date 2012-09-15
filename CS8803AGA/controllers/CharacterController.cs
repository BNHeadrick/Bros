﻿using Microsoft.Xna.Framework;
using System;
using CS8803AGAGameLibrary;
using CS8803AGA.collision;
using CS8803AGA.engine;
using CS8803AGA.actions;
using CS8803AGA;

namespace CS8803AGA.controllers
{
    public class CharacterController : ICollidable
    {
        public int Health { get; set; }
        public AnimationController AnimationController { get; protected set; }

        protected Vector2 m_position;
        protected Collider m_collider;
        protected int m_speed;

        protected float m_previousAngle;

        

        /// <summary>
        /// Factory method to create CharacterControllers
        /// </summary>
        /// <param name="ci">Information about character apperance and stats</param>
        /// <param name="startpos">Where in the Area the character should be placed</param>
        /// <param name="playerControlled">True if the character should be a PC, false if NPC</param>
        /// <returns>Constructed CharacterController</returns>
        public static CharacterController construct(CharacterInfo ci, Vector2 startpos, Constants.CharType typeOfChar)
        {

            CharacterController cc;
            ColliderType type;
            if (typeOfChar == Constants.CharType.PLAYERCHAR)
            {
                cc = new PlayerController();
                type = ColliderType.PC;
            }
            else if (typeOfChar == Constants.CharType.NPCHAR)
            {
                cc = new CharacterController();
                type = ColliderType.NPC;
            }
            else
            {
                cc = new CompanionController();
                type = ColliderType.PC;
            }


            cc.m_position = startpos;

            cc.AnimationController = new AnimationController(ci.animationDataPath, ci.animationTexturePath);
            cc.AnimationController.ActionTriggered += new ActionTriggeredEventHandler(cc.handleAction);
            cc.AnimationController.Scale = ci.scale;

            Rectangle bounds = ci.collisionBox;
            bounds.Offset((int)cc.m_position.X, (int)cc.m_position.Y);
            cc.m_collider = new Collider(cc, bounds, type);

            cc.m_speed = ci.speed;

            cc.m_previousAngle = (float)Math.PI / 2;

            return cc;
        }

        /// <summary>
        /// Factory method to construct non-player character controllers
        /// See other construct() method for more details
        /// </summary>
        public static CharacterController construct(CharacterInfo ci, Vector2 startpos)
        {
            return construct(ci, startpos, Constants.CharType.NPCHAR);
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

        public virtual void update()
        {
            // TODO
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
        protected virtual string angleTo4WayAnimation(float angle)
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
    }
}