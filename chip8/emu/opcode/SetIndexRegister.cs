using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8.emu.opcode
{
    class SetIndexRegister : Executor
    {
        public override ushort Mask => 0xF000;

        public override ushort Command => 0xA000;

        protected override void ExecuteInternal(ushort code, Memory mem)
        {
            mem.I = (ushort)(code & 0xFFF);
        }
    }
}
