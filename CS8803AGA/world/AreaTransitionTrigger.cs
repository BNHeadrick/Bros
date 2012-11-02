using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CSharpQuadTree;
using CS8803AGA.collision;
using CS8803AGA.controllers;
using CS8803AGA.engine;
using CS8803AGA.dialog;

namespace CS8803AGA
{
    public enum AreaSideEnum
    {
        Top, Bottom, Left, Right, Other
    }

    /// <summary>
    /// Special type of collision box which transfers a playable character from
    /// one Area to another
    /// </summary>
    public class AreaTransitionTrigger : ATrigger
    {
        protected Area m_owner;           // area this trigger is in
        protected Area m_target;          // area in which the PC will end up when triggered

        protected Point m_tilePos;        // coordinate of the owner Area where this trigger lies

        private AreaSideEnum m_side;      // used to determine where to place the PC in the target Area

        private bool m_hasGlobalTarget;   // whether or not we have the global target point
        private Point m_globalTarget;     // the specified target location to go to
        private Point m_destTile;         // coordinate of the destination tile

        public AreaTransitionTrigger(Area owner, Area target, Point tilePos, AreaSideEnum side)
            : base(calculateBounds(owner, tilePos, side))
        {
            this.m_owner = owner;
            this.m_target = target;

            this.m_tilePos = tilePos;

            this.m_side = side;

            this.m_hasGlobalTarget = false;
        }

        public AreaTransitionTrigger(Area owner, Area target, Point tilePos, AreaSideEnum side, Point globalTarget, Point destTile)
            : this(owner, target, tilePos, side)
        {
            m_hasGlobalTarget = true;
            m_globalTarget = globalTarget;
            m_destTile = destTile;
        }

        /// <summary>
        /// Used during construction to get the shape of the collision box
        /// </summary>
        /// <returns></returns>
        private static Rectangle calculateBounds(Area owner, Point tilePos, AreaSideEnum side)
        {
            const int attSize = 10;
            int tileWidth = owner.TileSet.tileWidth;
            int tileHeight = owner.TileSet.tileHeight;
            switch (side)
            {
                case AreaSideEnum.Top:
                    return new Rectangle(
                        tileWidth * tilePos.X,
                        -attSize,
                        tileWidth,
                        attSize);
                case AreaSideEnum.Bottom:
                    return new Rectangle(
                        tileWidth * tilePos.X,
                        tileHeight * tilePos.Y + tileHeight,
                        tileWidth,
                        attSize);
                case AreaSideEnum.Left:
                    return new Rectangle(
                        -attSize,
                        tileHeight * tilePos.Y,
                        attSize,
                        tileHeight);
                case AreaSideEnum.Right:
                    return new Rectangle(
                        tileWidth * tilePos.X + tileWidth,
                        tileHeight * tilePos.Y,
                        attSize,
                        tileHeight);
                case AreaSideEnum.Other:
                    return new Rectangle(
                        tileWidth * tilePos.X,
                        tileHeight * tilePos.Y,
                        tileWidth,
                        tileHeight);
                default:
                    throw new Exception("Unknown value for AreaSideEnum");
            }
        }

        #region ITrigger Members

        /// <summary>
        /// Code to actually transfer PC to a new Area when touched; called by collision handler
        /// </summary>
        /// <param name="other">Collider which has impacted the trigger</param>
        public override void handleImpact(Collider other)
        {
            if (other.m_owner == GameplayManager.Player)
            {
                GameplayManager.Player.clearPlan();
                // TODO
                // remove this code once maps are pre-generated and area transition triggers already contain references
                //  to their targets -- or, we might decide its just easier this way
                Point targetGlobalLocation;
                if (m_hasGlobalTarget)
                {
                    targetGlobalLocation = m_globalTarget;
                }
                else
                {
                    switch (m_side)
                    {
                        case AreaSideEnum.Top:
                            targetGlobalLocation = new Point(this.m_owner.GlobalLocation.X, this.m_owner.GlobalLocation.Y - 1);
                            break;
                        case AreaSideEnum.Bottom:
                            targetGlobalLocation = new Point(this.m_owner.GlobalLocation.X, this.m_owner.GlobalLocation.Y + 1);
                            break;
                        case AreaSideEnum.Left:
                            targetGlobalLocation = new Point(this.m_owner.GlobalLocation.X - 1, this.m_owner.GlobalLocation.Y);
                            break;
                        case AreaSideEnum.Right:
                            targetGlobalLocation = new Point(this.m_owner.GlobalLocation.X + 1, this.m_owner.GlobalLocation.Y);
                            break;
                        case AreaSideEnum.Other:
                            throw new Exception("AreaTransitions on non-edges not fully impled");
                        default:
                            throw new Exception("Unknown value for AreaSideEnum");
                    }
                }

                if (targetGlobalLocation == Area.PARTYHOOD)
                {
                    m_target = Area.makeTestArea(targetGlobalLocation);
                }
                else if (m_target == null)
                {
                    // check if it was created but we just don't have it
                    this.m_target = WorldManager.GetArea(targetGlobalLocation);

                    // still not created, so make it
                    if (m_target == null)
                    {
                        m_target = Area.makeTestArea(targetGlobalLocation);
                    }
                }
                

                // This code stays
                Point targetTile; // tile on other map on which player should arrive
                if (m_hasGlobalTarget)
                {
                    targetTile = m_destTile;
                }
                else
                {
                    switch (m_side)
                    {
                        case AreaSideEnum.Top:
                            targetTile = new Point(this.m_tilePos.X, Area.HEIGHT_IN_TILES - 1);
                            break;
                        case AreaSideEnum.Bottom:
                            targetTile = new Point(this.m_tilePos.X, 0);
                            break;
                        case AreaSideEnum.Left:
                            targetTile = new Point(Area.WIDTH_IN_TILES - 1, this.m_tilePos.Y);
                            break;
                        case AreaSideEnum.Right:
                            targetTile = new Point(0, this.m_tilePos.Y);
                            break;
                        case AreaSideEnum.Other:
                            throw new Exception("AreaTransitions on non-edges not fully impled");
                        default:
                            throw new Exception("Unknown value for AreaSideEnum");
                    }
                }
                // load the dialogs
                DialogManager.load(targetGlobalLocation);
                GameplayManager.changeActiveArea(this.m_target, targetTile);
            }
        }

        #endregion

        #region IGameObject Members

        public override bool isAlive()
        {
            return true;
        }

        public override void update()
        {
            // nch
        }

        public override void draw()
        {
            // nch
        }

        #endregion
    }
}
