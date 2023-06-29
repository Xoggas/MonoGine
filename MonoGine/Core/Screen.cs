﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace MonoGine;

/// <summary>
/// Represents a screen in the game engine.
/// </summary>
public sealed class Screen
{
    private readonly Core _core;

    internal Screen(Core core)
    {
        _core = core;
    }

    /// <summary>
    /// Gets an array of all available screen resolutions.
    /// </summary>
    public IEnumerable<Point> Resolutions
    {
        get
        {
            var supportedModes = GraphicsAdapter.DefaultAdapter.SupportedDisplayModes;
            return supportedModes.Select(x => new Point(x.Width, x.Height));
        }
    }
    
    /// <summary>
    /// Gets the current screen resolution.
    /// </summary>
    public Point Resolution
    {
        get
        {
            var displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            return new Point(displayMode.Width, displayMode.Height);
        }
    }
}