using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8.emu.opcode
{
    class Ret : Executor
    {
        public override ushort Mask => 0xFFFF;

        public override ushort Command => 0x00EE;

        protected override void ExecuteInternal(ushort code, Memory mem)
        {
            mem.pc = mem.stack[--mem.sp];
        }
    }
}
