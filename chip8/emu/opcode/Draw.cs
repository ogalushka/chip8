using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8.emu.opcode
{
    class Draw : Executor
    {
        public override ushort Mask => 0xF000;

        public override ushort Command => 0xD000;

        protected override void ExecuteInternal(ushort code, Memory mem)
        {
            var vx = (code & 0x0F00) >> 8;
            var vy = (code & 0x00F0) >> 4;
            var height = (code & 0x000F);

            var x = mem.V[vx];
            var y = mem.V[vy];

            mem.V[0xF] = 0;

            for (var i = 0; i < height; i++)
            {
                DrawLine(i, mem, x, y);
            }
        }

        private void DrawLine(int lineIdex, Memory mem, int x, int y)
        {
            var lineToDraw = mem.memory[mem.I + lineIdex];
            var targetY = (y + lineIdex);
            if (targetY >= Chip8.ScreenHeight)
                return;

            for (var pixelIndex = 0; pixelIndex < 8; pixelIndex++)
            {
                var targetX = x + pixelIndex;
                if (targetX >= Chip8.ScreenWidth)
                    return;

                var targetIndex = targetY * Chip8.ScreenWidth + targetX;
                var pixelToDraw = PixelAt(lineToDraw, pixelIndex);
                if (pixelToDraw && mem.gfx[targetIndex]) mem.V[0xF] = 1;

                mem.gfx[targetIndex] = pixelToDraw != mem.gfx[targetIndex];
            }
        }

        private bool HasPixelAt(byte line, int pixelIndex)
        {
            return (line & (0x80 >> pixelIndex)) != 0;
        }

        private bool PixelAt(byte line, int index)
        {
            return (line & (0b1000_0000 >> index)) != 0;
        }
    }
}
