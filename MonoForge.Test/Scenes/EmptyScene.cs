﻿using MonoForge.SceneManagement;
using MonoForge.SceneManagement.Interfaces;

namespace MonoForge.Test;

public sealed class EmptyScene : Scene
{
    public EmptyScene(GameBase gameBase, ISceneLoadingArgs args) : base(gameBase, args)
    {
    }

    protected override void OnUnload(GameBase gameBase)
    {
    }
}