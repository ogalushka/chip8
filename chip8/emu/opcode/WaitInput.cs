using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8.emu.opcode
{
    class WaitInput : Executor
    {
        public override ushort Mask => 0xF0FF;

        public override ushort Command => 0xF00A;

        protected override void ExecuteInternal(ushort code, Memory mem)
        {
            var vi = (code & 0x0F00) >> 8;
            for (var i = 0; i < mem.key.Length; i++)
            {
                if (mem.key[i])
                {
                    mem.V[vi] = (byte)i;
                    return;
                }
            }

            mem.pc -= 2;
        }
    }
}
