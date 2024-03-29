﻿using System;
using Microsoft.Xna.Framework.Graphics;

namespace MonoForge.Rendering;

public sealed class Shader : IEquatable<Shader>, IDeepCopyable<Shader>, IDisposable
{
    private readonly Effect _effect;
    private readonly Properties _properties;

    private Shader(Effect effect)
    {
        _effect = effect;
        _properties = new Properties();
    }

    private Shader(Effect effect, Properties properties)
    {
        _effect = effect;
        _properties = properties;
    }

    public Properties Properties => _properties;
    public EffectPassCollection Passes => _effect.CurrentTechnique.Passes;

    public static Shader FromEffect(Effect effect)
    {
        return new Shader(effect);
    }

    public void ApplyProperties()
    {
        Properties.ApplyTo(_effect);
    }

    public Shader DeepCopy()
    {
        return new Shader(_effect, _properties.DeepCopy());
    }

    public bool Equals(Shader? other)
    {
        return other != null && _properties.Equals(other._properties);
    }

    public void Dispose()
    {
        _effect.Dispose();
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Shader);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (_effect.GetHashCode() * 397) ^ _properties.GetHashCode();
        }
    }
}