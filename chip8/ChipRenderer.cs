using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8
{
    public class ChipRenderer
    {
        private const uint C1 = 0xFF000000;
        private const uint C2 = 0xFFFFFF;

        private readonly int chipWidth;
        private readonly int chipHeight;
        private readonly int renderWidth;
        private readonly int renderHeight;
        private readonly Texture2D chipRenderTarget;

        private readonly uint[] pixelData;

        public ChipRenderer(int chipWidth, int chipHeight, int renderWidth, int renderHeight, GraphicsDevice graphicsDevice)
        {
            this.chipWidth = chipWidth;
            this.chipHeight = chipHeight;
            this.renderWidth = renderWidth;
            this.renderHeight = renderHeight;

            chipRenderTarget = new Texture2D(graphicsDevice, chipWidth, chipHeight);
            pixelData = new uint[chipWidth * chipHeight];
        }

        public void Draw(SpriteBatch spriteBatch, bool[] screen)
        {
            var pixelCount = screen.Length;
            var pixelds = new uint[pixelCount];
            for (var i = 0; i < pixelCount; i++)
            {
                pixelds[i] = screen[i] ? C1 : C2;
            }

            chipRenderTarget.SetData(pixelds, 0, pixelCount);

            spriteBatch.Draw(chipRenderTarget, new Rectangle(0, 0, renderWidth, renderHeight), new Rectangle(0, 0, chipWidth, chipHeight), Color.White);
        }
    }
}
