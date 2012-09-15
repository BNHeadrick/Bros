using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace CS8803AGA.dialog
{
    class Dialog
    {
        private static Color DEFAULT_COLOR = Color.MistyRose;

        private List<string> m_text;
        private List<bool> m_drunk;
        private List<Color> m_color;
        private int m_current;

        public Dialog(string text, bool drunk, Color color)
        {
            m_text = new List<string>();
            m_drunk = new List<bool>();
            m_color = new List<Color>();

            add(text, drunk, color);
            m_current = 0;
        }

        public Dialog(string text, bool drunk)
            : this(text, drunk, DEFAULT_COLOR)
        {

        }

        public Dialog(string text)
            : this(text, false, DEFAULT_COLOR)
        {
        }

        public Dialog add(string text, bool drunk, Color color)
        {
            for (int i = 60; i < text.Length; i += 60)
            {
                while (text[i] != ' ' && text[i] != '\n')
                {
                    i--;
                }
                StringBuilder sb = new StringBuilder(text);
                sb[i] = '\n';
                text = sb.ToString();
            }
            m_text.Add(text);
            m_drunk.Add(drunk);
            m_color.Add(color);
            return this;
        }

        public Dialog add(string text, bool drunk)
        {
            return add(text, drunk, DEFAULT_COLOR);
        }

        public Dialog add(string text)
        {
            return add(text, false, DEFAULT_COLOR);
        }

        public string getText()
        {
            return m_text[m_current];
        }

        public bool getDrunk()
        {
            return m_drunk[m_current];
        }

        public Color getColor()
        {
            return m_color[m_current];
        }

        public Dialog getCurrent()
        {
            return new Dialog(m_text[m_current], m_drunk[m_current], m_color[m_current]);
        }

        public void next()
        {
            m_current++;
            if (m_current >= m_text.Count)
            {
                m_current = 0;
            }
        }
    }
}
