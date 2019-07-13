using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8.emu.opcode
{
    class AddIndex : Executor
    {
        public override ushort Mask => 0xF0FF;

        public override ushort Command => 0xF01E;

        protected override void ExecuteInternal(ushort code, Memory mem)
        {
            var vi = (code & 0x0F00) >> 8;
            mem.I += mem.V[vi];
        }
    }
}
