using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8.emu.opcode
{
    class StoreRegisters : Executor
    {
        public override ushort Mask => 0xF0FF;

        public override ushort Command => 0xF055;

        protected override void ExecuteInternal(ushort code, Memory mem)
        {
            var regCount = (code & 0x0F00) >> 8;
            for (var i = 0; i <= regCount; i++)
            {
                if (Chip8Options.ChangeIOnRegisterSaveLoad)
                {
                    mem.memory[mem.I] = mem.V[i];
                    mem.I++;
                }
                else
                {
                    mem.memory[mem.I + i] = mem.V[i];
                }
            }
        }
    }
}
