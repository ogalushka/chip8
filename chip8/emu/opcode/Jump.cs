using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8.emu.opcode
{
    class Jump : Executor
    {
        public override ushort Mask => 0xF000;

        public override ushort Command => 0x1000;

        protected override void ExecuteInternal(ushort code, Memory mem)
        {
            var address = code & 0x0FFF;
            mem.pc = (ushort)address;
        }
    }
}
