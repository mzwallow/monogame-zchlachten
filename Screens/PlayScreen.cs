using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Diagnostics;
using tainicom.Aether.Physics2D.Common;
using Microsoft.Xna.Framework.Media;
using Zchlachten.Entities;
using Zchlachten.Graphics;
using Microsoft.Xna.Framework.Audio;
using Zchlachten.Components;

namespace Zchlachten.Screens
{
    public class PlayScreen : Screen
    {
        private SpriteBatch _spriteBatch;

        private BasicEffect _spriteEffect;

        private World _world;

        private SpriteFont _debugFont, _buttonFont;

        private Texture2D _demonLordTxr, _braveTxr;
        private Texture2D _groundTxr, _corpsesPileTxr, _buttonTexture;
        private Texture2D[] _weaponTxrs;
        private Sprite _backgroundSprite, _winBraveSprite, _winDemonSprite, _backgroundWinSprite, _buttonSprite;

        private Ground _ground;
        private CorpsesPile _corpsesPile;

        private EntityManager _entityManager;
        private PlayerManager _playerManager;
        private WeaponManager _weaponManager;
        private Player _player;
        private StatusEffectManager _statusEffectManager;
        private ItemManager _itemManager;


        private DebugUI _debugUI;
        private Vector2 relativeMousePosition;
        private SoundEffectInstance _endSFX;
        private Song song;

        public PlayScreen(Zchlachten game) : base(game)
        {
            _spriteEffect = new BasicEffect(base.GraphicsDevice);
            _spriteEffect.TextureEnabled = true;
            _spriteEffect.VertexColorEnabled = true;

            Globals.Camera = new Camera2D(base.GraphicsDevice);
            Globals.Camera.Position.X = Globals.Camera.Width / 2;
            Globals.Camera.Position.Y = Globals.Camera.Height / 2;

            _world = new World(new Vector2(0, -9.8f));

            Globals.DebugView = new DebugView(_world);
            Globals.DebugView.LoadContent(base.GraphicsDevice, base.Content);
            Globals.DebugView.Enabled = true;

            _entityManager = new EntityManager();

            Globals.GameState = GameState.START;
            var values = Enum.GetValues(typeof(PlayerTurn));
            Globals.PlayerTurn = (PlayerTurn)values.GetValue(new Random().Next(values.Length));

        }

        public override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(base.GraphicsDevice);

            // Load fonts
            _debugFont = base.Content.Load<SpriteFont>("Fonts/Arial");

            // Load SoundEffectInstanceWin
            Globals.soundFX = base.Content.Load<SoundEffect>("Sound/Winning");
            _endSFX = Globals.soundFX.CreateInstance();

            // Load players
            _demonLordTxr = base.Content.Load<Texture2D>("Players/DemonLord");
            _braveTxr = base.Content.Load<Texture2D>("Players/Brave");
            _playerManager = new PlayerManager(
                _world,
                _entityManager,
                _demonLordTxr,
                _braveTxr
            );
            _playerManager.LoadContent(base.Content);

            // Load BGM
            song = Content.Load<Song>("Sound/BGM_Playing");
            MediaPlayer.Play(song);

            // Load weapons
            _weaponManager = new WeaponManager(
                _world,
                _entityManager,
                _playerManager.DemonLord,
                _playerManager.Brave
            );
            _weaponManager.LoadContent(base.Content);

            // Load buffs & debuffs
            _statusEffectManager = new StatusEffectManager(
                _world,
                _entityManager,
                _playerManager.DemonLord,
                _playerManager.Brave
            );
            _statusEffectManager.LoadContent(base.Content);

            // Load environments
            _backgroundSprite = new Sprite(base.Content.Load<Texture2D>("Environments/BG"));
            _groundTxr = base.Content.Load<Texture2D>("Environments/Ground");
            _ground = new Ground(_groundTxr, _world);
            _corpsesPileTxr = base.Content.Load<Texture2D>("Environments/CorpsesPile");
            _corpsesPile = new CorpsesPile(_corpsesPileTxr, _world);

            // Load items
            _itemManager = new ItemManager(
                _world,
                _entityManager,
                _playerManager.DemonLord,
                _playerManager.Brave
            );
            _itemManager.LoadContent(base.Content);

            //Load debug UI
            _debugUI = new DebugUI(
                _world,
                _groundTxr,
                _debugFont,
                _playerManager.DemonLord,
                _playerManager.Brave
            );
            // Load button 
            _buttonTexture = base.Content.Load<Texture2D>("Controls/Button");
            _buttonFont = base.Content.Load<SpriteFont>("Fonts/Text");
            _buttonSprite = new Sprite(_buttonTexture);

            //Load WinPlayerSide
            Texture2D _winBrave = base.Content.Load<Texture2D>("UI/BraveWin");
            Texture2D _winDemon = base.Content.Load<Texture2D>("UI/DemonWin");
            _winBraveSprite = new Sprite(_winBrave);
            _winDemonSprite = new Sprite(_winDemon);

            Texture2D _winBg = base.Content.Load<Texture2D>("UI/BgTransparent");
            _backgroundWinSprite = new Sprite(_winBg);

            _debugUI.LoadContent();

            _entityManager.AddEntry(_ground);
            _entityManager.AddEntry(_corpsesPile);

            _entityManager.AddEntry(_weaponManager);
            _entityManager.AddEntry(_statusEffectManager);
            _entityManager.AddEntry(_playerManager);
            _entityManager.AddEntry(_itemManager);

            // _entityManager.AddEntry(_debugUI);
        }

        public override void Update(GameTime gameTime)
        {
            relativeMousePosition = Globals.Camera.ConvertScreenToWorld(Globals.CurrentMouseState.Position);

            _world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            Globals.Camera.Update();

            // Update entiites
            _entityManager.Update(gameTime);

            if (Globals.GameState == GameState.START)
                Globals.GameState = GameState.PRE_PLAY;

            if (Globals.CurrentKeyboardState.IsKeyDown(Keys.G))
                Globals.DebugView.Enabled = !Globals.DebugView.Enabled;

            if (Globals.GameState == GameState.END)

            {
                if (relativeMousePosition.Y <= 11.7f && relativeMousePosition.Y >= 10.5f
                && relativeMousePosition.X >= 11.9f && relativeMousePosition.X <= 18f)
                {
                    if (Globals.IsClicked())
                    {
                        MediaPlayer.Stop();
                        Globals.ScreenManager.LoadScreen(new MenuScreen(base.Game));
                    }
                }
                _endSFX.Play();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Globals.DebugView.BeginCustomDraw(
                Globals.Camera.Projection,
                Globals.Camera.View,
                null,
                null,
                null,
                RasterizerState.CullNone,
                1f
            );

            _spriteEffect.View = Globals.Camera.View;
            _spriteEffect.Projection = Globals.Camera.Projection;

            _spriteBatch.Begin(
                SpriteSortMode.Deferred,
                null,
                null,
                null,
                RasterizerState.CullNone,
                _spriteEffect
            );

            // Draw background
            _backgroundSprite.Draw(
                _spriteBatch,
                new Vector2(Globals.Camera.Width / 2, Globals.Camera.Height / 2)
            );
            // Draw entities
            _entityManager.Draw(gameTime, _spriteBatch);

            //Draw EndGame Window
            if (Globals.GameState == GameState.END)
            {
                _backgroundWinSprite.Draw(_spriteBatch, new Vector2(Globals.Camera.Width / 2, Globals.Camera.Height / 2));

                if (Globals.PlayerTurn == PlayerTurn.DEMON_LORD)
                {
                    _winBraveSprite.Draw(_spriteBatch, new Vector2(Globals.Camera.Width / 2, Globals.Camera.Height / 2 + 4f));

                }
                else if (Globals.PlayerTurn == PlayerTurn.BRAVE)
                {
                    _winDemonSprite.Draw(_spriteBatch, new Vector2(Globals.Camera.Width / 2, Globals.Camera.Height / 2 + 4f));

                }
                _buttonSprite.Draw(_spriteBatch, new Vector2(Globals.Camera.Width / 2, (Globals.Camera.Height / 2) + 2.3f));//12.1,7.5
                _spriteBatch.DrawString(_buttonFont, "Back To Menu", new Vector2(12.3f, 10.4f), Color.Black, 0f, new Vector2(0, 0), Globals.Camera.Scale, SpriteEffects.FlipVertically, 0f);
            }

            _spriteBatch.End();
            Globals.DebugView.EndCustomDraw();
        }

        private void backToMenuButtonClick(object sender, EventArgs e)
        {
            MediaPlayer.Stop();
            Globals.ScreenManager.LoadScreen(new MenuScreen(base.Game));
        }

    }
}