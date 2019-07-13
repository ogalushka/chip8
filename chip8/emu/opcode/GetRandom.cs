using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8.emu.opcode
{
    class GetRandom : Executor
    {
        public override ushort Mask => 0xF000;

        public override ushort Command => 0xC000;

        protected override void ExecuteInternal(ushort code, Memory mem)
        {
            var i = (code & 0x0F00) >> 8;
            var value = (code & 0x00FF);
            mem.V[i] = (byte)(Chip8.Random.Next(0, 256) & value);
        }
    }
}
