using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8.emu.opcode
{
    class GetTimer : Executor
    {
        public override ushort Mask => 0xF0FF;

        public override ushort Command => 0xF007;

        protected override void ExecuteInternal(ushort code, Memory mem)
        {
            var i = (code & 0x0F00) >> 8;
            mem.V[i] = mem.sound_timer;
        }
    }
}
