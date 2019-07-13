using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8.emu.opcode
{
    class Or : Executor
    {
        public override ushort Mask => 0xF00F;

        public override ushort Command => 0x8001;

        protected override void ExecuteInternal(ushort code, Memory mem)
        {
            var xi = (code & 0x0F00) >> 8;
            var yi = (code & 0x00F0) >> 4;

            mem.V[xi] = (byte)(mem.V[xi] | mem.V[yi]);
        }
    }
}
