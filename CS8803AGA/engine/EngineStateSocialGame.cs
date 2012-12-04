using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CS8803AGA.controllers;
using Microsoft.Xna.Framework;
using CS8803AGA.Knowledge;
using Microsoft.Xna.Framework.Graphics;
using CS8803AGA.devices;

namespace CS8803AGA.engine
{
    class EngineStateSocialGame : AEngineState
    {
        public const int SCREEN_W = 960;
        public const int SCREEN_H = 600;
        public const int PAGE_SIZE = 7;

        public static bool game_played = false;

        public int victim;
        public int player;
        public SocialGame game;

        public List<SocialGame> possible_games;
        public int cursor;
        public bool key_pressed;
        public bool quit;

        public EngineStateSocialGame(int game_player, int game_victim)
            : base(EngineManager.Engine)
        {
            game_played = false;

            player = game_player;
            victim = game_victim;
            //game = social_game;
            game = null;
            key_pressed = true;
            quit = false;
            cursor = 0;
            //possible_games = ????
            /// debug:
            /// 
            possible_games = new List<SocialGame>();
            possible_games.Add(new SocialGame("Praise Brewtopia!"));
            possible_games.Add(new SocialGame("Grab a brew."));
            possible_games.Add(new SocialGame("Gossip about #55"));
            possible_games.Add(new SocialGame("Play prank."));
            possible_games.Add(new SocialGame("Vomit on."));
            possible_games.Add(new SocialGame("Knock brew out of hand."));
            possible_games.Add(new SocialGame("[CANCEL]"));
        }

        public override void update(GameTime gameTime)
        {
            if (!key_pressed && InputSet.getInstance().getButton(InputsEnum.BUTTON_4))
            {
                if (game == null && cursor != possible_games.Count-1)
                {
                    game = possible_games[cursor];
                    game_played = true;
                    key_pressed = true;
                }
                else
                {
                    quit = true;
                    key_pressed = true;
                }
            }
            else if (key_pressed && !InputSet.getInstance().getButton(InputsEnum.BUTTON_4) && InputSet.getInstance().getLeftDirectionalY() == 0)
            {
                key_pressed = false;
            }
            if (game == null)
            {
                if (!key_pressed && InputSet.getInstance().getLeftDirectionalY() > 0 && cursor > 0)
                {
                    cursor--;
                    key_pressed = true;
                }
                else if (!key_pressed && InputSet.getInstance().getLeftDirectionalY() < 0 && cursor < possible_games.Count - 1)
                {
                    cursor++;
                    key_pressed = true;
                }
            }
            if (quit && !key_pressed)
            {
                EngineManager.popState();
            }
        }

        public override void draw()
        {
            //EngineManager.peekBelowState(this).draw();

            bool had_player = false;
            bool had_victim = false;
            for (int i = 0; i < GameplayManager.ActiveArea.GameObjects.Count; i++)
            {
                if (GameplayManager.ActiveArea.GameObjects[i].getDoodadIndex() == player)
                {
                    CharacterController character = (CharacterController)GameplayManager.ActiveArea.GameObjects[i];
                    Vector2 pos = character.m_position;
                    character.m_position = new Vector2(SCREEN_W / 3, SCREEN_H / 8);
                    character.AnimationController.requestAnimation("right", AnimationController.AnimationCommand.Play);
                    character.draw();
                    character.m_position = pos;
                    had_player = true;
                }
                else if (GameplayManager.ActiveArea.GameObjects[i].getDoodadIndex() == victim)
                {
                    CharacterController character = (CharacterController)GameplayManager.ActiveArea.GameObjects[i];
                    Vector2 pos = character.m_position;
                    character.m_position = new Vector2(2*SCREEN_W / 3, SCREEN_H / 8);
                    character.AnimationController.requestAnimation("left", AnimationController.AnimationCommand.Play);
                    character.draw();
                    character.m_position = pos;
                    had_victim = true;
                }
                if (had_player && had_victim)
                {
                    break;
                }
            }

            if (game == null)
            { // yet to choose a social game
                draw_string("Choose social game:", SCREEN_W / 8, SCREEN_H / 8 + 50, Color.AliceBlue);
                int page_start = PAGE_SIZE * (cursor / PAGE_SIZE);
                int i = page_start;
                if (i != 0)
                {
                    draw_string("^ More ^", SCREEN_W / 6, SCREEN_H / 8 + 128 - 36, Color.AliceBlue);
                }
                for (; i < page_start + PAGE_SIZE && i < possible_games.Count; i++)
                {
                    draw_string((i == cursor ? "> " : "") + possible_games[i].name, SCREEN_W / 6, SCREEN_H / 8 + 128 + 36 * (i - page_start), Color.MistyRose);
                }
                if (i < possible_games.Count)
                {
                    draw_string("v More v", SCREEN_W / 6, SCREEN_H / 8 + 128 + 36 * i, Color.AliceBlue);
                }
            }
            else
            {
                // display results of social game
                draw_string("Results of: " + possible_games[cursor].name, SCREEN_W / 8, SCREEN_H / 8+50, Color.AliceBlue);
            }
        }

        /**
         * Draws a string, which may have a character image in it
         * @param str
         * @param x
         * @param y
         * @param color
         */
        public void draw_string(string str, int x, int y, Color color) {
            string option = str;
            int obj = -1;
            if (option.Contains("#"))
            {
                obj = Convert.ToInt32(option.Substring(option.IndexOf('#') + 1));
                option = option.Substring(0, option.IndexOf('#'));
            }
            FontMap.getInstance().getFont(FontEnum.Kootenay48).drawString(option, new Vector2(x, y), color, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 1.0f);
            if (obj != -1)
            {
                for (int j = 0; j < GameplayManager.ActiveArea.GameObjects.Count; j++)
                {
                    if (GameplayManager.ActiveArea.GameObjects[j].getDoodadIndex() == obj)
                    {
                        CharacterController character = (CharacterController)GameplayManager.ActiveArea.GameObjects[j];
                        Vector2 pos = character.m_position;
                        character.m_position = new Vector2(x + option.Length * 16, y + 16);
                        character.AnimationController.requestAnimation("down", AnimationController.AnimationCommand.Play);
                        character.AnimationController.Scale /= 1.5f;
                        character.draw();
                        character.AnimationController.Scale *= 1.5f;
                        character.m_position = pos;
                        break;
                    }
                }
            }
        }
    }
}