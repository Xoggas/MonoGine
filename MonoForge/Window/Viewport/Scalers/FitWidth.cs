﻿using Microsoft.Xna.Framework;

namespace MonoForge;

public sealed class FitWidth : IViewportScaler
{
    public float AspectRatio { get; set; } = 16f / 9f;

    public Point GetSize(Point windowResolution)
    {
        var width = windowResolution.X;
        var height = (int)(windowResolution.X / AspectRatio);
        return new Point(width, height);
    }
}