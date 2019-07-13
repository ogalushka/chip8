using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8.emu.opcode
{
    class CLS : Executor
    {
        public override ushort Mask => 0xFFFF;

        public override ushort Command => 0x00E0;

        protected override void ExecuteInternal(ushort code, Memory mem)
        {
            for (var i = 0; i < mem.gfx.Length; i++)
            {
                mem.gfx[i] = false;
            }
        }
    }
}
