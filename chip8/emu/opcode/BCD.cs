using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8.emu.opcode
{
    class BCD : Executor
    {
        public override ushort Mask => 0xF0FF;

        public override ushort Command => 0xF033;

        protected override void ExecuteInternal(ushort code, Memory mem)
        {
            var registerIndex = (code & 0x0F00) >> 8;

            var value = mem.V[registerIndex];

            mem.memory[mem.I] = (byte)(value / 100);
            mem.memory[mem.I + 1] = (byte)((value / 10) % 10);
            mem.memory[mem.I + 2] = (byte)(value % 10);
        }
    }
}
