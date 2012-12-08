﻿using System;
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
        public bool results_screen;

        /**
         * Creates a new social game results screen (no choice because not player initing
         * @param game_player the game starter
         * @param game_victim the other player
         * @param social_game the game to be played
         */
        public EngineStateSocialGame(int game_player, int game_victim, int social_game): base(EngineManager.Engine)
        {
            game_played = false;
            player = game_player;
            victim = game_victim;

            key_pressed = true;
            results_screen = false;
            quit = false;
            cursor = 0;

            if (player == -1 || victim == -1 || social_game == -1)
            {
                quit = true;
                key_pressed = false;
                possible_games = new List<SocialGame>();
                game = null;
            }
            else
            { // this is a real game
                //possible_games = SocialGames.get(player, victim);
                //game = possible_games[social_game];

                //debug:
                possible_games = new List<SocialGame>();
                game = null;
            }
        }

        /**
         * Creates a new social game menu (player init)
         * @param game_victim the other player
         */
        public EngineStateSocialGame(int game_victim)
            : base(EngineManager.Engine)
        {
            game_played = false;

            player = Constants.PLAYER;
            victim = game_victim;

            game = null;
            key_pressed = true;
            results_screen = false;
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
                if (game == null && cursor < possible_games.Count-1)
                {
                    game = possible_games[cursor];
                    game_played = true;
                }
                else if (results_screen)
                {
                    quit = true;
                }
                else
                {
                    results_screen = true;
                }
                key_pressed = true;
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
            if (quit)
            {
                EngineManager.peekBelowState(this).draw();
            }
            else
            {
                CharacterController character = GameplayManager.ActiveArea.getCharacter(player);
                if (character != null) {
                    Vector2 pos = character.m_position;
                    character.m_position = new Vector2(SCREEN_W / 3, SCREEN_H / 8);
                    character.AnimationController.requestAnimation("right", AnimationController.AnimationCommand.Play);
                    character.draw();
                    character.m_position = pos;
                }
                character = GameplayManager.ActiveArea.getCharacter(victim);
                if (character != null) {
                    Vector2 pos = character.m_position;
                    character.m_position = new Vector2(2 * SCREEN_W / 3, SCREEN_H / 8);
                    character.AnimationController.requestAnimation("left", AnimationController.AnimationCommand.Play);
                    character.draw();
                    character.m_position = pos;
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
                else if (results_screen)
                {
                    // display results of social game
                    draw_string("Results of: " + possible_games[cursor].name, SCREEN_W / 8, SCREEN_H / 8 + 50, Color.AliceBlue);
                }
                else
                {
                    // display some cool stock text
                    draw_string(possible_games[cursor].name, SCREEN_W / 8, SCREEN_H / 8 + 50, Color.AliceBlue);
                    draw_string(possible_games[cursor].text(), SCREEN_W / 8, SCREEN_H / 8 + 100, Color.Thistle);
                }
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
            CharacterController character = GameplayManager.ActiveArea.getCharacter(obj);
            if (character != null) {
                Vector2 pos = character.m_position;
                character.m_position = new Vector2(x + option.Length * 16, y + 16);
                character.AnimationController.requestAnimation("down", AnimationController.AnimationCommand.Play);
                character.AnimationController.Scale /= 1.5f;
                character.draw();
                character.AnimationController.Scale *= 1.5f;
                character.m_position = pos;
            }
        }
    }
}