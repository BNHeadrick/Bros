using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CS8803AGA.devices;
using CS8803AGA.dialog;
using CS8803AGA.questcontent;
using CS8803AGA.puzzle;
using CS8803AGA.controllers;
using CS8803AGA.collision;

namespace CS8803AGA.engine
{
    public class EngineStateDialogue : AEngineState
    {
        private CharacterController npc;
        private bool bouncerMode;
        private bool bouncerPass;
        private int bouncerDist;
        private int bouncerI;

        private bool brewMode;

        private bool drawOnCompanionSide;

        #region Graphics

        private GameTexture m_baseImage;

        private bool m_button4_released;

        private Dialog m_dialog;

        #endregion

        public EngineStateDialogue(int character, CharacterController _npc, CharacterController player, bool is_companion)
            : base(EngineManager.Engine)
        {
            m_baseImage = new GameTexture(@"Sprites/RPG/PopupScreen");

            m_dialog = DialogManager.get(character);
            m_button4_released = false;

            drawOnCompanionSide = is_companion;

            brewMode = false;
            bouncerMode = false;
            bouncerPass = false;
            bouncerDist = 0;
            bouncerI = 0;
            npc = _npc;

            if (npc != null && npc.getCollider().m_type == ColliderType.NPC && player != null && player.brew != null)
            {
                if (npc.bouncer != null)
                {
                    m_dialog = null;
                    bouncerMode = true;
                    bouncerPass = npc.bouncer.canPass(player.brew, (int)npc.getCollider().Bounds.Center().X, (int)npc.getCollider().Bounds.Center().Y, (int)npc.getCollider().Bounds.Width, (int)npc.getCollider().Bounds.Height);
                    
                    //if what was passed in is the player, then add what task he's currently doing to the plan.
                    if (bouncerPass && !is_companion)
                    {
                        player.addTaskToPlan(npc.bouncer);
                    }
                }
                else if (npc.brew != null)
                {
                    //if what was passed in is the player, then add what task he's currently doing to the plan.

                    brewMode = true;
                    if (player.brew.mix(npc.brew))
                    {
                        m_dialog = null;
                        if (!is_companion)
                        {
                            player.addTaskToPlan(npc.brew);
                        }
                    }
                    else
                    {
                        m_dialog = new Dialog("I don't have the right brew for this mixture");
                    }
                }
            }
        }

        public override void update(GameTime gameTime)
        {
            if (bouncerMode)
            {
                if (!bouncerPass)
                {
                    m_dialog = new Dialog("Bouncer: Only cool people allowed past here!", false, Color.PapayaWhip);
                    bouncerMode = false;
                }
                else
                {
                    // do_path
                    if (bouncerI < npc.bouncer.getPathSize())
                    {
                        int dir = npc.bouncer.getPath(bouncerI);
                        int distance = npc.m_speed;
                        string animName = npc.angleTo4WayAnimation((float)Math.PI / 2);
                        if (dir == Bouncer.PATH_UP || dir == Bouncer.PATH_DOWN)
                        {
                            if (dir == Bouncer.PATH_DOWN)
                            {
                                animName = npc.angleTo4WayAnimation(3 * (float)Math.PI / 2);
                            }
                            if (bouncerDist + distance > Area.TILE_HEIGHT)
                            {
                                distance = Area.TILE_HEIGHT - bouncerDist;
                                bouncerI++;
                                bouncerDist = 0;
                            }
                            else
                            {
                                bouncerDist += distance;
                            }
                            npc.getCollider().handleMovement(new Vector2(0, dir > 0 ? distance : -distance));
                        }
                        else
                        {
                            if (dir == Bouncer.PATH_LEFT)
                            {
                                animName = npc.angleTo4WayAnimation((float)Math.PI);
                            }
                            else
                            {
                                animName = npc.angleTo4WayAnimation(0);
                            }
                            if (bouncerDist + distance > Area.TILE_WIDTH)
                            {
                                distance = Area.TILE_WIDTH - bouncerDist;
                                bouncerI++;
                                bouncerDist = 0;
                            }
                            else
                            {
                                bouncerDist += distance;
                            }
                            npc.getCollider().handleMovement(new Vector2(dir > 0 ? distance : -distance, 0));
                        }
                        npc.AnimationController.requestAnimation(animName, AnimationController.AnimationCommand.Play);
                        npc.AnimationController.update();
                    }
                    else
                    {
                        bouncerMode = false;
                        npc.bouncer.switchDirection();
                    }
                }
            }
            else if (!Quest.gameOver && (InputSet.getInstance().getButton(InputsEnum.BUTTON_1) ||
                InputSet.getInstance().getButton(InputsEnum.BUTTON_2) ||
                InputSet.getInstance().getButton(InputsEnum.CONFIRM_BUTTON) ||
                InputSet.getInstance().getButton(InputsEnum.CANCEL_BUTTON) ||
                (m_button4_released && InputSet.getInstance().getButton(InputsEnum.BUTTON_4)) ||
                m_dialog == null))
            {
                EngineManager.popState();
                InputSet.getInstance().setAllToggles();
                return;
            }
            if (!InputSet.getInstance().getButton(InputsEnum.BUTTON_4))
            {
                m_button4_released = true;
            }
        }

        public override void draw()
        {
            EngineManager.peekBelowState(this).draw();

            /*DrawCommand td = DrawBuffer.getInstance().DrawCommands.pushGet();
            Point p = m_engine.GraphicsDevice.Viewport.TitleSafeArea.Center;
            Vector2 v = new Vector2(p.X, p.Y);
            td.set(m_baseImage, 0, v, CoordinateTypeEnum.ABSOLUTE, Constants.DepthDialoguePage,
                true, Color.White, 0f, 1f);

            WorldManager.DrawMap(new Vector2(300, 100), 600, 500, Constants.DepthDialogueText);*/
            if (npc != null && npc.getCollider().m_type == ColliderType.NPC)
            {
                if (npc.bouncer != null)
                {
                    int j = 0;
                    for (int i = 1; i <= npc.bouncer.getColor(); i <<= 1)
                    {
                        if ((npc.bouncer.getColor() & i) != 0)
                        {
                            FontMap.getInstance().getFont(FontEnum.Kootenay48).drawString("[#]", new Vector2(drawOnCompanionSide ? 912 - 48 : 48, j * 36), Brew.getTextColor(i), 0, Vector2.Zero, 0.5f, SpriteEffects.None, 1.0f);
                        }
                        j++;
                    }
                }
                else if (npc.brew != null)
                {
                    int j = 0;
                    for (int i = 1; i <= npc.brew.getColor() || i <= npc.brew.getUncolor(); i <<= 1)
                    {
                        if ((npc.brew.getColor() & i) != 0)
                        {
                            FontMap.getInstance().getFont(FontEnum.Kootenay48).drawString("+[#]", new Vector2(drawOnCompanionSide ? 912 - 48*2 : 48, j * 36), Brew.getTextColor(i), 0, Vector2.Zero, 0.5f, SpriteEffects.None, 1.0f);
                        }
                        else if ((npc.brew.getUncolor() & i) != 0)
                        {
                            FontMap.getInstance().getFont(FontEnum.Kootenay48).drawString("-[#]", new Vector2(drawOnCompanionSide ? 912 - 48*2 : 48, j * 36), Brew.getTextColor(i), 0, Vector2.Zero, 0.5f, SpriteEffects.None, 1.0f);
                        }
                        j++;
                    }
                }
            }
            if (!bouncerMode && m_dialog != null)
            {
                FontMap.getInstance().getFont(FontEnum.Kootenay48).drawString(m_dialog.getText(), new Vector2(50, 300), m_dialog.getColor(), 0, Vector2.Zero, 0.5f, m_dialog.getDrunk() ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 1.0f);
            }
        }
    }
}
