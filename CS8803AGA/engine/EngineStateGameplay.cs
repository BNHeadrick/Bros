using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CS8803AGAGameLibrary;
using System.Collections.Generic;
using CSharpQuadTree;
using System;
using CS8803AGA.collision;
using CS8803AGA.devices;
using CS8803AGA.controllers;
using CS8803AGA.questcontent;

namespace CS8803AGA.engine
{
    /// <summary>
    /// Engine state for the main gameplay processes of the game.
    /// </summary>
    public class EngineStateGameplay : AEngineState
    {
        public int[] party_order;
        public int current_partier;

        /// <summary>
        /// Creates an EngineStateGameplay and registers it with the GameplayManager.
        /// Also creates and initializes a PlayerController and starting Area.
        /// </summary>
        /// <param name="engine">Engine instance for the game.</param>
        public EngineStateGameplay(Engine engine) : base(engine)
        {
            if (GameplayManager.GameplayState != null)
                throw new Exception("Only one EngineStateGameplay allowed at once!");
             

            CharacterInfo ci = GlobalHelper.loadContent<CharacterInfo>(@"Characters/"+Constants.doodadIntToString(Constants.PLAYER));

            PlayerController player =
                (PlayerController)CharacterController.construct(ci, new Vector2(600, 400), Constants.CharType.PLAYERCHAR, null);

            CharacterInfo compCi = GlobalHelper.loadContent<CharacterInfo>(@"Characters/" + Constants.doodadIntToString(Constants.COMPANION));

            CompanionController compC =
                (CompanionController)CharacterController.construct(compCi, new Vector2(500, 50), Constants.CharType.COMPANIONCHAR, player);

            Point startPoint = new Point(0, 0);
            Area.makeTestArea(startPoint);
            GameplayManager.initialize(this, player, compC, WorldManager.GetArea(startPoint));

            Quest.initPartyQuest();

            party_order = new int[]{
                Constants.PARTY_PEOPLE1,
                Constants.PARTY_PEOPLE2,
                Constants.PLAYER,
                Constants.PARTY_PEOPLE3,
                Constants.PARTY_PEOPLE4,
                Constants.PLAYER,
                Constants.PARTY_PEOPLE5,
                Constants.PARTY_PEOPLE6,
                Constants.PLAYER,
                Constants.PARTY_PEOPLE7,
                Constants.PARTY_PEOPLE8,
                Constants.PLAYER,
                Constants.PARTY_PEOPLE9,
                Constants.PARTY_PEOPLE10,
                Constants.PLAYER,
                Constants.COMPANION,
                Constants.COOK,
                Constants.PLAYER,
                Constants.BREW_MAIDEN
            };
            current_partier = 0;
        }

        /// <summary>
        /// Main game loop, checks for UI-related inputs and tells game objects to update.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {           
            if (InputSet.getInstance().getButton(InputsEnum.BUTTON_3))
            {
                EngineManager.pushState(new EngineStateMap());
                return;
            }

            GameplayManager.Player.checkNPCCollision();

            Area area = GameplayManager.ActiveArea;
            if (area.GlobalLocation == Area.PARTY)
            { // make party turn based
                for (int i = 0; i < area.GameObjects.Count; i++)
                {
                    if (area.GameObjects[i].getDoodadIndex() == party_order[current_partier])
                    {
                        if (area.GameObjects[i].update() && ++current_partier >= party_order.Length)
                        {
                            current_partier = 0;
                        }
                        break;
                    }
                }
            }
            else
            {
                area.GameObjects.ForEach(i => i.update());
            }
            area.GameObjects.ForEach(i => { if (!i.isAlive() && i is ICollidable) ((ICollidable)i).getCollider().unregister(); });
            area.GameObjects.RemoveAll(i => !i.isAlive());

            if (InputSet.getInstance().getButton(InputsEnum.LEFT_TRIGGER))
            {
                InputSet.getInstance().setToggle(InputsEnum.LEFT_TRIGGER);

                Vector2 rclickspot = new Vector2(InputSet.getInstance().getRightDirectionalX(), InputSet.getInstance().getRightDirectionalY());
                DecorationSet ds = DecorationSet.construct("World/town");
                Decoration d = ds.makeDecoration("house1", rclickspot);

                GameplayManager.ActiveArea.add(d);
            }
        }

        public override void draw()
        {
            GameplayManager.ActiveArea.draw();
            GameplayManager.drawHUD();

            drawCollisionDetector(false);
        }

        /// <summary>
        /// Draws all of the ActiveArea's colliders.
        /// </summary>
        /// <param name="drawQuadTree">True if QuadTree partitions should also be drawn.</param>
        protected void drawCollisionDetector(bool drawQuadTree)
        {
            Area area = GameplayManager.ActiveArea;

            List<QuadTree<Collider>.QuadNode> nodes = area.CollisionDetector.getAllNodes();
            foreach (QuadTree<Collider>.QuadNode node in nodes)
            {
                if (drawQuadTree)
                {
                    DoubleRect dr = node.Bounds;
                    LineDrawer.drawLine(new Vector2((float)dr.X, (float)dr.Y),
                                        new Vector2((float)dr.X + (float)dr.Width, (float)dr.Y),
                                        Color.AliceBlue);
                    LineDrawer.drawLine(new Vector2((float)dr.X, (float)dr.Y),
                                        new Vector2((float)dr.X, (float)dr.Y + (float)dr.Height),
                                        Color.AliceBlue);
                    LineDrawer.drawLine(new Vector2((float)dr.X + (float)dr.Width, (float)dr.Y),
                                        new Vector2((float)dr.X + (float)dr.Width, (float)dr.Y + (float)dr.Height),
                                        Color.AliceBlue);
                    LineDrawer.drawLine(new Vector2((float)dr.X, (float)dr.Y + (float)dr.Height),
                                        new Vector2((float)dr.X + (float)dr.Width, (float)dr.Y + (float)dr.Height),
                                        Color.AliceBlue);
                }
                
                foreach (Collider collider in node.quadObjects)
                {
                    DoubleRect dr2 = collider.Bounds;
                    LineDrawer.drawLine(new Vector2((float)dr2.X, (float)dr2.Y),
                                    new Vector2((float)dr2.X + (float)dr2.Width, (float)dr2.Y),
                                    Color.LimeGreen);
                    LineDrawer.drawLine(new Vector2((float)dr2.X, (float)dr2.Y),
                                        new Vector2((float)dr2.X, (float)dr2.Y + (float)dr2.Height),
                                        Color.LimeGreen);
                    LineDrawer.drawLine(new Vector2((float)dr2.X + (float)dr2.Width, (float)dr2.Y),
                                        new Vector2((float)dr2.X + (float)dr2.Width, (float)dr2.Y + (float)dr2.Height),
                                        Color.LimeGreen);
                    LineDrawer.drawLine(new Vector2((float)dr2.X, (float)dr2.Y + (float)dr2.Height),
                                        new Vector2((float)dr2.X + (float)dr2.Width, (float)dr2.Y + (float)dr2.Height),
                                        Color.LimeGreen);
                }
            }
        }

        public void cleanup()
        {
            GameplayManager.GameplayState = null;
            WorldManager.reset();
        }
    }
}