﻿using Microsoft.Xna.Framework;
using CSharpQuadTree;
using System;
using System.Collections.Generic;
using CS8803AGA.controllers;

namespace CS8803AGA.collision
{
    public enum ColliderType
    {
        PC, NPC, Scenery, Movable, Effect, Trigger
    }

    /// <summary>
    /// Encapsulation of information about something which can be collided into.
    /// </summary>
    public class Collider : IQuadObject
    {
        /// <summary>
        /// CollisionDetector which is managing this collider.
        /// </summary>
        private CollisionDetector m_detector;

        /// <summary>
        /// Area the collider takes up.
        /// </summary>
        public DoubleRect m_bounds;
        private DoubleRect m_bounds2;

        /// <summary>
        /// Object whose area is represented by this collider.
        /// </summary>
        public ICollidable m_owner;

        /// <summary>
        /// Type of owning object.
        /// </summary>
        public ColliderType m_type;

        /// <summary>
        /// The collider that we are currently colliding against
        /// </summary>
        public Collider m_other;

        public Collider(ICollidable owner, Rectangle bounds, ColliderType type)
        {
            this.m_owner = owner;
            this.m_bounds = new DoubleRect(bounds.X, bounds.Y, bounds.Width, bounds.Height);
            this.m_bounds2 = new DoubleRect(bounds.X - 5, bounds.Y - 5, bounds.Width + 10, bounds.Height + 10);
            this.m_type = type;
            this.m_other = null;
        }

        public void setCollider(ColliderType t)
        {
            this.m_type = t;
        }

        /// <summary>
        /// Find all Colliders which intersect the queried Area and are
        /// managed by this Collider's managing CollisionDetector.
        /// </summary>
        /// <param name="queryRect">Area in which to find Colliders.</param>
        /// <returns>All Colliders which intersect the queryRect.</returns>
        public List<Collider> queryDetector(Rectangle queryRect)
        {
            return queryDetector(new DoubleRect(m_bounds.X, m_bounds.Y, m_bounds.Width, m_bounds.Height));
        }

        /// <summary>
        /// Find all Colliders which intersect the queried Area and are
        /// managed by this Collider's managing CollisionDetector.
        /// </summary>
        /// <param name="queryRect">Area in which to find Colliders.</param>
        /// <returns>All Colliders which intersect the queryRect.</returns>
        public List<Collider> queryDetector(DoubleRect queryRect)
        {
            return m_detector.query(queryRect);
        }

        /// <summary>
        /// Forcible - ignores collisions.
        /// Move the Collider; informs its CollisionDetector via an event.
        /// Also moves the owning object's DrawPosition.
        /// </summary>
        /// <param name="dp">Delta of position</param>
        public void move(Vector2 dp)
        {
            m_bounds += dp;
            m_bounds2 += dp;
            m_owner.DrawPosition += dp;

            RaiseBoundsChanged();
        }

        public DoubleRect Bounds
        {
            get { return m_bounds; }
        }

        public DoubleRect Surroundings
        {
            get { return m_bounds2; }
        }

        /// <summary>
        /// Move the Collider according to what the CollisionHandler allows.
        /// </summary>
        /// <param name="dp">Delta position.</param>
        public void handleMovement(Vector2 dp)
        {
            m_detector.handleMovement(this, dp);
        }

        public void forCollisionDetectorUseOnly(CollisionDetector cd)
        {
            this.m_detector = cd;
        }

        public void unregister()
        {
            this.m_detector.remove(this);
        }

        private void RaiseBoundsChanged()
        {
            EventHandler handler = BoundsChanged;
            if (handler != null)
                handler(this, new EventArgs());
        }

        public event System.EventHandler BoundsChanged;
    }
}