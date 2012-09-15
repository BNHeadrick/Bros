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

        private PlayerController player;

        public Vector2 getAbsPosVec()
        {
            return getMPos();
        }

        public CompanionController(PlayerController p)
        {
            // nch, should only be called by CharacterController.construct
            player = p;
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

            
            //dx = InputSet.getInstance().getLeftDirectionalX() * m_speed;
            //dy = InputSet.getInstance().getLeftDirectionalY() * m_speed;

            Vector2 normPlayPos = player.getAbsPosVec();

            normPlayPos.Normalize();
            double chaseAngle = 0.0;

            if (
                //(InputSet.getInstance().getLeftDirectionalX() ==0 && InputSet.getInstance().getLeftDirectionalY() ==0 ) &&
                ((Math.Abs(player.getAbsPosVec().X - getAbsPosVec().X) > 90) ||
                (Math.Abs(player.getAbsPosVec().Y - getAbsPosVec().Y) > 90)))
            {
                dx = (player.getAbsPosVec().X - this.getAbsPosVec().X);
                dy = (player.getAbsPosVec().Y - this.getAbsPosVec().Y);

                //Console.WriteLine(player.getAbsPosVec().X + " " + player.getAbsPosVec().Y + " " +
                    //getAbsPosVec().X + " " + getAbsPosVec().X + " ");

                chaseAngle = Math.Atan2(player.getAbsPosVec().Y - this.getAbsPosVec().Y, 
                    player.getAbsPosVec().X - this.getAbsPosVec().X);

                //Console.WriteLine(chaseAngle);


            }
            
            //Console.WriteLine(normPlayPos.X + " " + normPlayPos.Y);

            //int playerPosX = (int)player.getAbsPosVec().X;
            //int playerPosY = (int)player.getAbsPosVec().Y;

            //Console.Write(playerPosX + " " + playerPosY);

            if (dx == 0 && dy == 0)
            {
                Console.WriteLine("stopped!");
                return;
                
            }
            else
            {
                Console.WriteLine("MOVING!");
                //testing for obtaining absolute x/y position
                //Console.WriteLine("COMPANION!");
            }

            /*float angle =
                CommonFunctions.getAngle(new Vector2(dx, dy));*/
            float angle = (float)chaseAngle;

            string animName = angleTo4WayAnimation(-angle);
            AnimationController.requestAnimation(animName, AnimationController.AnimationCommand.Play);

            if (true )
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
