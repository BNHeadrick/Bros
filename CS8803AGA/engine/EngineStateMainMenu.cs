using Microsoft.Xna.Framework;
using System.Collections.Generic;
using CS8803AGA.devices;
using CS8803AGA.ui;
using Microsoft.Xna.Framework.Graphics;

namespace CS8803AGA.engine
{
    public class EngineStateMainMenu : AEngineState
    {
        private const string c_StartGame = "Start Game";

        private MenuList m_menuList;

        private MenuList m_title;

        public EngineStateMainMenu(Engine engine) : base(engine)
        {
            List<string> menuOptions = new List<string>();
            menuOptions.Add(c_StartGame);

            Point temp = m_engine.GraphicsDevice.Viewport.TitleSafeArea.Center;
            m_menuList = new MenuList(menuOptions, new Vector2(temp.X, temp.Y));
            m_menuList.Font = FontEnum.Kootenay48;
            m_menuList.ItemSpacing = 100;
            m_menuList.SpaceAvailable = 400;

            List<string> titleOptions = new List<string>();
            titleOptions.Add("Bros:");
            m_title = new MenuList(titleOptions, new Vector2(temp.X, temp.Y - 150));
            m_title.Font = FontEnum.Lindsey;
            m_title.ItemSpacing = 100;
            m_title.SpaceAvailable = 400;
            m_title.Scale = 8.0f;
            m_title.BaseColor = Color.AliceBlue;
            m_title.SelectedColor = Color.AliceBlue;
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (InputSet.getInstance().getButton(InputsEnum.CONFIRM_BUTTON))
            {
                switch (m_menuList.SelectedString)
                {
                    case c_StartGame:
                        EngineManager.replaceCurrentState(new EngineStateGameplay(m_engine));
                        return;
                    default:
                        break;
                }
            }

            if (InputSet.getInstance().getLeftDirectionalY() < 0)
            {
                m_menuList.selectNextItem();
                InputSet.getInstance().setStick(InputsEnum.LEFT_DIRECTIONAL, 5);
            }

            if (InputSet.getInstance().getLeftDirectionalY() > 0)
            {
                m_menuList.selectPreviousItem();
                InputSet.getInstance().setStick(InputsEnum.LEFT_DIRECTIONAL, 5);
            }
        }

        public override void draw()
        {
            m_menuList.draw();
            m_title.draw();
        }
    }
}
