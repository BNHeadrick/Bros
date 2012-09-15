using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CS8803AGA.controllers;
using CS8803AGA.questcontent;

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
        public static QuestManager QuestManager { get { return questManager; } }

        private static PlayerController playerController = null;
        private static CompanionController companionController = null;
        private static QuestManager questManager = null;

        public static Area ActiveArea { get { return activeArea; } }
        private static Area activeArea = null;

        public static void initialize(EngineStateGameplay esg, PlayerController pc, CompanionController cc, Area startArea, QuestManager qm)
        {
            GameplayState = esg;
            playerController = pc;
            companionController = cc;
            activeArea = startArea;
            questManager = qm;

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

            departingArea.remove(GameplayManager.Player);
            departingArea.remove(GameplayManager.Companion);

            Rectangle targetRectangle = arrivingArea.getTileRectangle(targetTile.X, targetTile.Y);
            Vector2 newPos = new Vector2(
                targetRectangle.X + arrivingArea.TileSet.tileWidth / 2,
                targetRectangle.Y + arrivingArea.TileSet.tileHeight / 2);

            Player.getCollider().move(newPos - Player.getCollider().Bounds.Center());
            Companion.getCollider().move(newPos - Companion.getCollider().Bounds.Center());

            arrivingArea.add(GameplayManager.Player);
            arrivingArea.add(GameplayManager.Companion);

            activeArea = arrivingArea;
        }

        public static void drawHUD()
        {
            // TODO
        }
    }
}
