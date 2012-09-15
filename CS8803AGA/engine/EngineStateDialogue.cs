using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CS8803AGA.devices;
using CS8803AGA.dialog;

namespace CS8803AGA.engine
{
    public class EngineStateDialogue : AEngineState
    {

        #region Graphics

        private GameTexture m_baseImage;

        private bool m_button4_released;

        private Dialog m_dialog;

        #endregion

        public EngineStateDialogue(int character)
            : base(EngineManager.Engine)
        {
            m_baseImage = new GameTexture(@"Sprites/RPG/PopupScreen");

            m_dialog = DialogManager.get(character);
            m_button4_released = false;
        }

        public override void update(GameTime gameTime)
        {
            if (InputSet.getInstance().getButton(InputsEnum.BUTTON_1) ||
                InputSet.getInstance().getButton(InputsEnum.BUTTON_2) ||
                InputSet.getInstance().getButton(InputsEnum.CONFIRM_BUTTON) ||
                InputSet.getInstance().getButton(InputsEnum.CANCEL_BUTTON) ||
                (m_button4_released && InputSet.getInstance().getButton(InputsEnum.BUTTON_4)) ||
                m_dialog == null)
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

            if (m_dialog != null)
            {
                FontMap.getInstance().getFont(FontEnum.Kootenay48).drawString(m_dialog.getText(), new Vector2(50, 300), m_dialog.getColor(), 0, Vector2.Zero, 0.5f, m_dialog.getDrunk() ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 1.0f);
            }
        }
    }
}
