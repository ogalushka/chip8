using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8.emu.opcode
{
    abstract class Executor
    {
        public abstract ushort Mask { get; }
        public abstract ushort Command { get; }

        public bool CanExecute(ushort code)
        {
            return (Mask & code) == Command;
        }

        public void Execute(ushort code, Memory mem)
        {
            mem.pc += 2;
            ExecuteInternal(code, mem);
        }

        protected abstract void ExecuteInternal(ushort code, Memory mem);
    }
}
