using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8
{
    class ChipInput
    {
        public Dictionary<Keys, int> KeyMap = new Dictionary<Keys, int>
            {
                { Keys.D1, 1 },
                { Keys.D2, 2 },
                { Keys.D3, 3 },
                { Keys.Q, 4 },
                { Keys.W, 5 },
                { Keys.E, 6 },
                { Keys.A, 7 },
                { Keys.S, 8 },
                { Keys.D, 9 },
                { Keys.Z, 10 },
                { Keys.X, 0 },
                { Keys.C, 11 },
                { Keys.D4, 12 },
                { Keys.R, 13 },
                { Keys.F, 14 },
                { Keys.V, 15 }
            };

        public bool[] GetPressedKeys()
        {
            var result = new bool[16];
            var state = Keyboard.GetState();
            foreach (var pair in KeyMap)
            {
                result[pair.Value] = state.IsKeyDown(pair.Key);
            }
            return result;
        }
    }
}
