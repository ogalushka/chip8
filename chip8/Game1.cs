using chip8.emu;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.IO;

namespace chip8
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        Chip8 chip;
        ChipSound chipSound;
        ChipInput chipInput;
        ChipRenderer chipRenderer;
        byte[] program;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 256;
            graphics.PreferredBackBufferWidth = graphics.PreferredBackBufferHeight * 3;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            base.Initialize();

            UserInterface.Initialize(Content, BuiltinThemes.editor);
            //UserInterface.Active.UseRenderTarget = true;
            //Panel panel = new Panel(new Vector2(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferHeight), anchor: Anchor.CenterRight);
            //panel.PanelOverflowBehavior = PanelOverflowBehavior.VerticalScroll;
            //UserInterface.Active.AddEntity(panel);
            SelectList list = new SelectList(new Vector2(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferHeight), anchor: Anchor.CenterRight);
            UserInterface.Active.AddEntity(list);

            var directory = new DirectoryInfo($"{Content.RootDirectory}/roms/");
            foreach (var file in directory.GetFiles("*.*"))
            {
                list.AddItem(file.Name);
            }
            list.SelectedIndex = 0;
            list.OnValueChange += e => StartRom(list.SelectedValue);

            chip = new Chip8();
            chip.LoadGame(program);
            chipSound = new ChipSound();
            chipInput = new ChipInput();
            chipRenderer = new ChipRenderer(Chip8.ScreenWidth, Chip8.ScreenHeight, 512, 256, GraphicsDevice);
        }

        private void StartRom(string name)
        {
            using (var stream = TitleContainer.OpenStream($"Content/roms/{name}"))
            {
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                program = bytes;
            }

            chip = new Chip8();
            chip.LoadGame(program);
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            StartRom("15PUZZLE");
        }
        
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            chip.EmulateCycle();
            chip.SetKeys(chipInput.GetPressedKeys());
            if (chip.PlaySound)
            {
                chipSound.Play();
            }
            else
            {
                chipSound.Pause();
            }

            UserInterface.Active.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            chipRenderer.Draw(spriteBatch, chip.Screen);
            spriteBatch.End();

            UserInterface.Active.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
