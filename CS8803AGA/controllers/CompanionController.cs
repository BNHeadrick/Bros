using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CSharpQuadTree;
using CS8803AGA.engine;
using CS8803AGA.devices;
using CS8803AGA.collision;

namespace CS8803AGA.controllers
{
    /// <summary>
    /// CharacterController for a player-controlled Character.
    /// Reads from inputs to update movement and animations.
    /// </summary>
    public class CompanionController : CharacterController
    {
        public float dx { get; set; }
        public float dy { get; set; }

        public Vector2 getAbsPosVec()
        {
            return getMPos();
        }

        public CompanionController()
        {
            // nch, should only be called by CharacterController.construct
        }

        public override void update()
        {
            AnimationController.update();

            /*
            if (InputSet.getInstance().getButton(InputsEnum.BUTTON_1))
            {
                string dir = angleTo4WayAnimation(m_previousAngle);
                dir = "attack" + dir;
                AnimationController.requestAnimation(dir, AnimationController.AnimationCommand.Play);
            }
            */

            
            dx = InputSet.getInstance().getLeftDirectionalX() * m_speed;
            dy = InputSet.getInstance().getLeftDirectionalY() * m_speed;

            if (dx == 0 && dy == 0)
            {
                return;
            }
            else
            {
                //testing for obtaining absolute x/y position
                //Console.WriteLine("COMPANION!");
            }

            float angle =
                CommonFunctions.getAngle(new Vector2(dx, dy));

            string animName = angleTo4WayAnimation(angle);
            AnimationController.requestAnimation(animName, AnimationController.AnimationCommand.Play);

            if (true )
            {
                //m_collider.handleMovement(new Vector2(dx, -dy));
            }

            m_previousAngle = angle;
            
        }

    }
}
