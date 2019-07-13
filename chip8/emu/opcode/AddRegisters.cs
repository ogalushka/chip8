using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8.emu.opcode
{
    class AddRegisters : Executor
    {
        public override ushort Mask => 0xF00F;

        public override ushort Command => 0x8004;

        protected override void ExecuteInternal(ushort code, Memory mem)
        {
            var vx = (code & 0x0F00) >> 8;
            var vy = (code & 0x00F0) >> 4;

            var result = mem.V[vx] + mem.V[vy];
            mem.V[0xF] = result > byte.MaxValue ? (byte)1 : (byte)0;
            mem.V[vx] = (byte)(result & 0xFF);
        }
    }
}
