using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8.emu.opcode
{
    class SetRegister : Executor
    {
        public override ushort Mask => 0xF000;

        public override ushort Command => 0x6000;

        protected override void ExecuteInternal(ushort code, Memory mem)
        {
            var vIndex = (code & 0x0F00) >> 8;
            var vValue = (code & 0x00FF);
            mem.V[vIndex] = (byte)vValue;
        }
    }
}
