using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Feng.Drawing
{ 
    public class CharRangHelper
    {
        public CharacterRange[] GetCharRange(string text)
        {
            if (text.Length < 33)
            {
                CharacterRange[] ranges = new CharacterRange[text.Length];
                for (int i = 0; i < text.Length; i++)
                {
                    ranges[i] = characterRanges[i];
                }
                return ranges;
            }
            return null;
        }
        public readonly CharacterRange[] characterRanges = { 
                                                   new CharacterRange(0, 1)
                                                   , new CharacterRange(1, 1)
                                                   , new CharacterRange(2, 1)
                                                   , new CharacterRange(3, 1)
                                                   , new CharacterRange(4, 1)
                                                   , new CharacterRange(5, 1)
                                                   , new CharacterRange(6, 1)
                                                   , new CharacterRange(7, 1)
                                                   , new CharacterRange(8, 1)
                                                   , new CharacterRange(9, 1)
                                                   , new CharacterRange(10, 1)
                                                   , new CharacterRange(11, 1)
                                                   , new CharacterRange(12, 1)
                                                   , new CharacterRange(13, 1)
                                                   , new CharacterRange(14, 1)
                                                   , new CharacterRange(15, 1) 
                                                   , new CharacterRange(16, 1)
                                                   , new CharacterRange(17, 1)
                                                   , new CharacterRange(18, 1)
                                                   , new CharacterRange(19, 1) 
                                                   , new CharacterRange(20, 1)
                                                   , new CharacterRange(21, 1)
                                                   , new CharacterRange(22, 1)
                                                   , new CharacterRange(23, 1)
                                                   , new CharacterRange(24, 1)
                                                   , new CharacterRange(25, 1) 
                                                   , new CharacterRange(26, 1)
                                                   , new CharacterRange(27, 1)
                                                   , new CharacterRange(28, 1)
                                                   , new CharacterRange(29, 1) 
                                                   , new CharacterRange(30, 1)
                                                   , new CharacterRange(31, 1) 
                                               };

    }
}
