using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chip8
{
    class ChipSound
    {
        private const int SampleRate = 44100;
        private const int Frequency = 441;
        private int SamplesPerBuffer = 3000;

        private DynamicSoundEffectInstance chipSound;
        private byte[] soundBuffer;
        private double time;

        public ChipSound()
        {
            chipSound = new DynamicSoundEffectInstance(SampleRate, AudioChannels.Mono);
            chipSound.BufferNeeded += ChipSound_BufferNeeded;
            chipSound.SubmitBuffer(GetBuffer());
            
        }

        public void Play()
        {
            chipSound.Play();
        }

        public void Pause()
        {
            chipSound.Pause();
        }

        private void ChipSound_BufferNeeded(object sender, EventArgs e)
        {
            chipSound.SubmitBuffer(GetBuffer());
        }

        private byte[] GetBuffer()
        {
            var workBuffer = new float[SamplesPerBuffer];
            for (int i = 0; i < SamplesPerBuffer; i++)
            {
                workBuffer[i] = (float)SineWave(time, 880);
                time += 1.0 / SampleRate;
            }

            return ConvertBuffer(workBuffer);
        }

        private static double SineWave(double time, double frequency)
        {
            return Math.Sin(time * 100 * Math.PI * frequency);
        }

        private static byte[] ConvertBuffer(float[] from)
        {
            const int bytesPerSample = 2;
            int samplesPerBuffer = from.Length;
            var to = new byte[samplesPerBuffer * bytesPerSample];

            Debug.Assert(to.Length == samplesPerBuffer * bytesPerSample, "Buffer sizes are mismatched.");
            for (int i = 0; i < samplesPerBuffer; i++)
            {
                float floatSample = MathHelper.Clamp(from[i], -1.0f, 1.0f);
                short shortSample = (short)(floatSample >= 0.0f ? floatSample * short.MaxValue : floatSample * short.MinValue * -1);

                int index = i * bytesPerSample;
                if (!BitConverter.IsLittleEndian)
                {
                    to[index] = (byte)(shortSample >> 8);
                    to[index + 1] = (byte)shortSample;
                }
                else
                {
                    to[index + 1] = (byte)shortSample;
                    to[index] = (byte)(shortSample >> 8);
                }
            }

            return to;
        }
    }
}
    
