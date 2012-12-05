using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CS8803AGA.controllers;
using CS8803AGA.collision;
using CS8803AGA.questcontent;
using Microsoft.Xna.Framework.Graphics;
using CS8803AGA.puzzle;

namespace CS8803AGA.engine
{
    /// <summary>
    /// Static class for quickly accessing active gameplay information.
    /// Probably pure evil, but very handy.
    /// </summary>
    public class GameplayManager
    {
        public static EngineStateGameplay GameplayState { get; set; }

        public static PlayerController Player { get { return playerController; } }
        public static CompanionController Companion { get { return companionController; } }

        private static PlayerController playerController = null;
        private static CompanionController companionController = null;

        public static Area ActiveArea { get { return activeArea; } }
        private static Area activeArea = null;

        public static void initialize(EngineStateGameplay esg, PlayerController pc, CompanionController cc, Area startArea)
        {
            GameplayState = esg;
            playerController = pc;
            companionController = cc;
            activeArea = startArea;

            activeArea.add(cc);
            activeArea.add(pc);
        }

        /// <summary>
        /// Moves the player from one area to another.
        /// </summary>
        /// <param name="arrivingArea">New area.</param>
        /// <param name="targetTile">Tile in that area the player should appear on.</param>
        public static void changeActiveArea(Area arrivingArea, Point targetTile)
        {
            Area departingArea = GameplayManager.ActiveArea;

            if (arrivingArea == departingArea)
                return;

            if (departingArea.exists(GameplayManager.Companion))
            {
                //GameplayManager.Companion.getCollider().setCollider(ColliderType.PC);
                departingArea.remove(GameplayManager.Companion);
            }

            

            departingArea.remove(GameplayManager.Player);
            

            Rectangle targetRectangle = arrivingArea.getTileRectangle(targetTile.X, targetTile.Y);
            Vector2 newPos = new Vector2(
                targetRectangle.X + arrivingArea.TileSet.tileWidth / 2,
                targetRectangle.Y + arrivingArea.TileSet.tileHeight / 2);

            Player.getCollider().move(newPos - Player.getCollider().Bounds.Center());
            if (Quest.talkedToCompanion)
            //if(true)
            {
                Companion.getCollider().move(newPos - Companion.getCollider().Bounds.Center());
                ((CompanionController)Companion).learnNewInfo(CharacterController.currPlan);
                CharacterController.currPlan = null;
                //Player.brew.extract(CharacterController.ALL_BREW);
                //Companion.brew.extract(CharacterController.ALL_BREW);
            }
            arrivingArea.add(GameplayManager.Player);

            if (!Quest.talkedToCompanion && arrivingArea.Equals(WorldManager.GetArea(Area.START)))
            {
                arrivingArea.add(GameplayManager.Companion);
                //ADD AND ALSO IF FIRST QUEST IS UNDONE!
                GameplayManager.Companion.setAbsPos(new Vector2(500, 50));
            }
            else if(Quest.talkedToCompanion)
            {
                arrivingArea.add(GameplayManager.Companion);
            }

            activeArea = arrivingArea;

            if (activeArea.GlobalLocation == Area.PARTY)
            {
                // place the companion and player into new default start locales
                double xdiff = GameplayManager.Player.m_position.X - GameplayManager.Player.m_collider.m_bounds.X;
                double ydiff = GameplayManager.Player.m_position.Y - GameplayManager.Player.m_collider.m_bounds.Y;
                GameplayManager.Player.m_position = new Vector2(Area.TILE_WIDTH * 3 + 20, Area.TILE_HEIGHT * 3);
                GameplayManager.Player.m_collider.move(new Vector2((float)(-GameplayManager.Player.m_collider.m_bounds.X + GameplayManager.Player.m_position.X - xdiff), (float)(-GameplayManager.Player.m_collider.m_bounds.Y + GameplayManager.Player.m_position.Y - ydiff)));
                GameplayManager.Player.m_position = new Vector2(Area.TILE_WIDTH * 3 + 20, Area.TILE_HEIGHT * 3);

                xdiff = GameplayManager.Companion.m_position.X - GameplayManager.Companion.m_collider.m_bounds.X;
                ydiff = GameplayManager.Companion.m_position.Y - GameplayManager.Companion.m_collider.m_bounds.Y;
                GameplayManager.Companion.m_position = new Vector2(Area.TILE_WIDTH * 3 + 20, Area.TILE_HEIGHT * 9);
                GameplayManager.Companion.m_collider.move(new Vector2((float)(-GameplayManager.Companion.m_collider.m_bounds.X + GameplayManager.Companion.m_position.X - xdiff), (float)(-GameplayManager.Companion.m_collider.m_bounds.Y + GameplayManager.Companion.m_position.Y - ydiff)));
                GameplayManager.Companion.m_position = new Vector2(Area.TILE_WIDTH * 3 + 20, Area.TILE_HEIGHT * 9);
                GameplayManager.Companion.m_collider.m_type = ColliderType.NPC;
            }
            else
            {
                GameplayManager.Companion.m_collider.m_type = ColliderType.PC;
            }
        }

        public static void drawHUD()
        {
            // brews
            int j = 0;
            for (int i = 1; i <= playerController.brew.getColor() || i <= companionController.brew.getColor(); i <<= 1)
            {
                if ((playerController.brew.getColor() & i) != 0)
                {
                    FontMap.getInstance().getFont(FontEnum.Kootenay48).drawString("[#]", new Vector2(0, j * 36), Brew.getTextColor(i), 0, Vector2.Zero, 0.5f, SpriteEffects.None, 1.0f);
                }
                if ((companionController.brew.getColor() & i) != 0)
                {
                    FontMap.getInstance().getFont(FontEnum.Kootenay48).drawString("[#]", new Vector2(912, j * 36), Brew.getTextColor(i), 0, Vector2.Zero, 0.5f, SpriteEffects.None, 1.0f);
                }
                j++;
            }
        }
    }
}
