using chip8.emu.opcode;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8.emu
{
    class Chip8
    {
        public const byte ScreenWidth = 64;
        public const byte ScreenHeight = 32;
        public static Random Random = new Random();
        private readonly Memory mem = new Memory();
        private readonly IEnumerable<Executor> executers;

        public Chip8()
        {
            mem.pc = Memory.ProgramStart;
            mem.I = 0;
            mem.sp = 0;

            var type = typeof(Executor);

            executers = type.Assembly.GetTypes()
                .Where(t => type.IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                .Select(t => (Executor)Activator.CreateInstance(t))
                .OrderByDescending(executor => GetSetBitCount(executor.Mask));
        }

        public void EmulateCycle()
        {
            for (var i = 0; i < Chip8Options.TimeMultiplier; i++)
            {
                ExecuteOpCode(GetOpCode());

                if (mem.delay_timer > 0)
                {
                    mem.delay_timer--;
                }

                if (mem.sound_timer > 0)
                {
                    mem.sound_timer--;
                }
            }
        }

        public void LoadGame(byte[] game)
        {
            for (var i = 0; i < game.Length; i++)
            {
                mem.memory[Memory.ProgramStart + i] = game[i];
            }
        }

        public void SetKeys(bool[] keys)
        {
            Debug.Assert(keys.Length == mem.key.Length);
            for (var i = 0; i < mem.key.Length; i++)
            {
                mem.key[i] = keys[i];
            }
        }

        public bool PlaySound => mem.sound_timer > 0;
        public bool[] Screen => mem.gfx;

        private ushort GetOpCode()
        {
            var opCode = (ushort)(mem.memory[mem.pc] << 8 | mem.memory[mem.pc + 1]);
            return opCode;
        }

        private void ExecuteOpCode(ushort opCode)
        {
            var opCodeString = opCode.ToString("X4");
            foreach (var executer in executers)
            {
                if (executer.CanExecute(opCode))
                {
                    executer.Execute(opCode, mem);
                    return;
                }
            }
            throw new Exception($"Unsupported op code {opCodeString}");
        }

        private int GetSetBitCount(int value)
        {
            var result = 0;
            while (value != 0)
            {
                value &= value - 1;
                result++;
            }

            return result;
        }
    }
}
