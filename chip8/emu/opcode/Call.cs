using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8.emu.opcode
{
    class Call : Executor
    {
        public override ushort Mask => 0xF000;

        public override ushort Command => 0x2000;

        protected override void ExecuteInternal(ushort code, Memory mem)
        {
            mem.stack[mem.sp] = mem.pc;
            mem.sp++;
            mem.pc = (ushort)(code & 0xFFF);
        }
    }
}
