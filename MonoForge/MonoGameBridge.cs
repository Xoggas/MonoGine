﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoForge;

internal sealed class MonoGameBridge : Game
{
    internal event Action? OnInitialize;
    internal event Action? OnBeginRun;
    internal event Action? OnLoadResources;
    internal event Action? OnUnloadResources;
    internal event Action<GameTime>? OnBeginUpdate;
    internal event Action<GameTime>? OnUpdate;
    internal event Action<GameTime>? OnDraw;

    internal MonoGameBridge()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this);
    }

    internal GraphicsDeviceManager GraphicsDeviceManager { get; }

    protected override void Initialize()
    {
        base.Initialize();

        GraphicsDeviceManager.PreferMultiSampling = true;
        GraphicsDeviceManager.GraphicsProfile = GraphicsProfile.HiDef;
        GraphicsDevice.PresentationParameters.MultiSampleCount = 8;
        GraphicsDeviceManager.ApplyChanges();

        OnInitialize?.Invoke();
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        OnLoadResources?.Invoke();
    }

    protected override void UnloadContent()
    {
        base.UnloadContent();

        OnUnloadResources?.Invoke();
    }

    protected override void Update(GameTime gameTime)
    {
        OnBeginUpdate?.Invoke(gameTime);

        base.Update(gameTime);

        OnUpdate?.Invoke(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        OnDraw?.Invoke(gameTime);
    }

    protected override void BeginRun()
    {
        base.BeginRun();

        OnBeginRun?.Invoke();
    }
}