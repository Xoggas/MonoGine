﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoForge.Audio;
using MonoForge.InputSystem;
using MonoForge.Rendering;
using MonoForge.Rendering.Batching;
using MonoForge.SceneManagement;

namespace MonoForge;

/// <summary>
/// Represents the base class for the game framework.
/// </summary>
public abstract class GameBase : IDisposable
{
    private readonly MonoGameBridge _monoGameBridge;

    /// <summary>
    /// Initializes a new instance of the Engine class.
    /// </summary>
    protected GameBase()
    {
        _monoGameBridge = new MonoGameBridge();
        _monoGameBridge.OnInitialize += OnInitialize;
        _monoGameBridge.OnBeginRun += OnStart;
        _monoGameBridge.OnLoadResources += OnLoadResources;
        _monoGameBridge.OnUnloadResources += OnUnloadResources;
        _monoGameBridge.OnDraw += OnDraw;
        _monoGameBridge.OnBeginUpdate += OnBeginUpdate;
        _monoGameBridge.OnUpdate += OnUpdate;

        ContentManager = _monoGameBridge.Content;
        Time = new Time();
        Cursor = new Cursor(_monoGameBridge);
        Input = new Input(_monoGameBridge.Window);
        AudioManager = new AudioManager();
        SceneManager = new SceneManager();
    }

    /// <summary>
    /// Gets the graphics device manager associated with the engine.
    /// </summary>
    public GraphicsDeviceManager GraphicsDeviceManager => _monoGameBridge.GraphicsDeviceManager;

    /// <summary>
    /// Gets the graphics device associated with the engine.
    /// </summary>
    public GraphicsDevice GraphicsDevice => _monoGameBridge.GraphicsDevice;

    /// <summary>
    /// Gets the time instance associated with the engine.
    /// </summary>
    public Time Time { get; }

    /// <summary>
    /// Gets the window instance associated with the engine.
    /// </summary>
    public Window Window { get; private set; } = default!;

    /// <summary>
    /// Gets the input provider.
    /// </summary>
    public IInput Input { get; }

    /// <summary>
    /// Gets the cursor instance associated with the engine.
    /// </summary>
    public Cursor Cursor { get; }

    /// <summary>
    /// Gets or sets the resource manager instance associated with the engine.
    /// </summary>
    public ContentManager ContentManager { get; }

    /// <summary>
    /// Gets or sets the scene manager instance associated with the engine.
    /// </summary>
    public SceneManager SceneManager { get; }

    /// <summary>
    /// Gets or sets the audio manager instance associated with the engine.
    /// </summary>
    public IAudioManager AudioManager { get; protected set; }

    /// <summary>
    /// Gets the renderer.
    /// </summary>
    public IRenderer Renderer { get; protected set; } = default!;

    /// <summary>
    /// Exits the engine.
    /// </summary>
    public void Exit()
    {
        _monoGameBridge.Exit();
    }

    /// <summary>
    /// Disposes the engine and releases any resources it holds.
    /// </summary>
    public virtual void Dispose()
    {
        Window.Dispose();
        Input.Dispose();
        ContentManager.Dispose();
        SceneManager.Dispose();
        AudioManager.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Runs the engine.
    /// </summary>
    public void Run()
    {
        _monoGameBridge.Run();
    }

    /// <summary>
    /// Handles the initialization of the engine.
    /// </summary>
    protected virtual void OnInitialize()
    {
        Window = new Window(_monoGameBridge);
        Renderer = new Renderer(this, new DynamicBatcher(), new DrawingService(), RenderConfig.Default);
    }

    /// <summary>
    /// Called once the engine is initialized.
    /// </summary>
    protected virtual void OnStart()
    {
    }

    /// <summary>
    /// Handles the loading of resources.
    /// </summary>
    protected virtual void OnLoadResources()
    {
    }

    /// <summary>
    /// Handles the unloading of resources.
    /// </summary>
    protected virtual void OnUnloadResources()
    {
    }

    /// <summary>
    /// Handles the beginning of the update phase.
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    protected virtual void OnBeginUpdate(GameTime gameTime)
    {
        AudioManager.Update(this, Time.DeltaTime);
    }

    /// <summary>
    /// Handles the update phase.
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    protected virtual void OnUpdate(GameTime gameTime)
    {
        Time.Update(gameTime);
        Input.Update(this, Time.DeltaTime);
        SceneManager.Update(this);
    }

    /// <summary>
    /// Handles the draw phase.
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    protected virtual void OnDraw(GameTime gameTime)
    {
        if (Renderer == null)
        {
            throw new NullReferenceException("The renderer wasn't set");
        }

        if (SceneManager.CurrentScene == null)
        {
            return;
        }

        Renderer.Draw(this, SceneManager.CurrentScene);
    }
}