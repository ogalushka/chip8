using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8.emu.opcode
{
    class SkipNotPressed : Executor
    {
        public override ushort Mask => 0xF0FF;

        public override ushort Command => 0xE0A1;

        protected override void ExecuteInternal(ushort code, Memory mem)
        {
            var i = (code & 0x0F00) >> 8;
            var key = mem.V[i];
            if (!mem.key[key]) mem.pc += 2;
        }
    }
}
