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
        }

        public override void update()
        {
            AnimationController.update();

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
            
            if((Math.Abs(player.getAbsPosVec().X - getAbsPosVec().X) < 50) &&
                (Math.Abs(player.getAbsPosVec().Y - getAbsPosVec().Y) < 50)){
                    return;
            }

            float angle = (float)chaseAngle;

            string animName = angleTo4WayAnimation(-angle);
            AnimationController.requestAnimation(animName, AnimationController.AnimationCommand.Play);

            if ( Quest.talkedToCompanion )
            //if (true) 
            {
                //m_collider.handleMovement(new Vector2(dx, -dy));
                m_collider.handleMovement(AngleToVector((float)chaseAngle));
            }

            m_previousAngle = angle;
            
        }

        Vector2 AngleToVector(float angle)
        {
            return new Vector2((float)Math.Cos(angle)*m_speed, (float)Math.Sin(angle)*m_speed);
        }

    }
}
