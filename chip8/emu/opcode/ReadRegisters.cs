using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8.emu.opcode
{
    class ReadRegisters : Executor
    {
        public override ushort Mask => 0xF0FF;

        public override ushort Command => 0xF065;

        protected override void ExecuteInternal(ushort code, Memory mem)
        {
            var registerCount = (code & 0x0F00) >> 8;

            for (var i = 0; i <= registerCount; i++)
            {
                if (Chip8Options.ChangeIOnRegisterSaveLoad)
                {
                    mem.V[i] = mem.memory[mem.I];
                    mem.I++;
                }
                else
                {
                    mem.V[i] = mem.memory[mem.I + 1];
                }
            }
        }
    }
}
