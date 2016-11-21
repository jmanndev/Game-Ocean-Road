using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace OceanRoadWinMono
{
    class Road : Sprite
    {

        public Road(SpriteBatch sp, Texture2D tex, Color color, Vector2 pos, int width, int height)
            : base(sp, tex, color, pos, height, width)
        {

        }

    }
}
