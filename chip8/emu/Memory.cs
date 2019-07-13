using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8.emu
{
    class Memory
    {
        public const int ProgramStart = 0x200;

        public readonly byte[] memory = new byte[4096];
        public readonly byte[] V = new byte[16];
        public ushort I;
        public ushort pc;
        public readonly bool[] gfx = new bool[64 * 32];
        public byte delay_timer;
        public byte sound_timer;

        public readonly ushort[] stack = new ushort[16];
        public ushort sp = 0;
        public readonly bool[] key = new bool[16];


        private byte[] fontset = new byte[80]
        {
          0xF0, 0x90, 0x90, 0x90, 0xF0, // 0
          0x20, 0x60, 0x20, 0x20, 0x70, // 1
          0xF0, 0x10, 0xF0, 0x80, 0xF0, // 2
          0xF0, 0x10, 0xF0, 0x10, 0xF0, // 3
          0x90, 0x90, 0xF0, 0x10, 0x10, // 4
          0xF0, 0x80, 0xF0, 0x10, 0xF0, // 5
          0xF0, 0x80, 0xF0, 0x90, 0xF0, // 6
          0xF0, 0x10, 0x20, 0x40, 0x40, // 7
          0xF0, 0x90, 0xF0, 0x90, 0xF0, // 8
          0xF0, 0x90, 0xF0, 0x10, 0xF0, // 9
          0xF0, 0x90, 0xF0, 0x90, 0x90, // A
          0xE0, 0x90, 0xE0, 0x90, 0xE0, // B
          0xF0, 0x80, 0x80, 0x80, 0xF0, // C
          0xE0, 0x90, 0x90, 0x90, 0xE0, // D
          0xF0, 0x80, 0xF0, 0x80, 0xF0, // E
          0xF0, 0x80, 0xF0, 0x80, 0x80  // F
        };

        public const ushort CharSpriteHeight = 5;

        public Memory()
        {
            for (var i = 0; i < fontset.Length; i++)
            {
                memory[i] = fontset[i];
            }
        }
    }
}
