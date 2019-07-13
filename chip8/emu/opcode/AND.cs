﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8.emu.opcode
{
    class AND : Executor
    {
        public override ushort Mask => 0xF00F;

        public override ushort Command => 0x8002;

        protected override void ExecuteInternal(ushort code, Memory mem)
        {
            var ix = (code & 0x0F00) >> 8;
            var iy = (code & 0x00F0) >> 4;

            mem.V[ix] = (byte)(mem.V[ix] & mem.V[iy]);
        }
    }
}
