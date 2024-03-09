﻿using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace MonoGine.Test;

public sealed class Game : Engine
{
    protected override void OnInitialize()
    {
        base.OnInitialize();
        Cursor.IsVisible = true;
        SetupWindow();
        LoadScene();
    }

    private void SetupWindow()
    {
        Window.Title = "MonoGine";
        Window.Resolution = new Point(1280, 720);
        Window.Viewport.Size = new Point(640, 360);
        Window.IsFullscreen = false;
        Window.Framerate = 60;
    }

    private void LoadScene()
    {
        SceneManager.Load(this, new RenderingTestScene());
    }

    protected override void OnUpdate(GameTime gameTime)
    {
        base.OnUpdate(gameTime);
        Debug.WriteLine(Window.Resolution);
    }
}