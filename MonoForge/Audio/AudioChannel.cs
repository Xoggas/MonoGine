﻿using System;
using System.Collections.Generic;

namespace MonoForge.Audio;

public sealed class AudioChannel : IAudioChannel
{
    private readonly List<IAudioSource> _sources;

    internal AudioChannel()
    {
        _sources = new List<IAudioSource>();
    }

    public float Volume { get; set; } = 1f;
    public float Pitch { get; set; } = 1f;

    public void Update(GameBase gameBase, float deltaTime)
    {
        foreach (IAudioSource source in _sources)
        {
            source.Update(gameBase, deltaTime);
        }

        RemoveDestroyedSources();
    }

    public IAudioSource CreateSource()
    {
        var source = new AudioSource();

        _sources.Add(source);

        return source;
    }

    public void RemoveSource(IAudioSource source)
    {
        _sources.Remove(source);
    }

    public void PauseById(string id, StringComparison comparison)
    {
        ExecuteCallbackForSources(GetSourcesById(id, comparison), source => source.Pause());
    }

    public void PauseAll()
    {
        ExecuteCallbackForSources(_sources, source => source.Pause());
    }

    public void StopById(string id, StringComparison comparison)
    {
        ExecuteCallbackForSources(GetSourcesById(id, comparison), source => source.Stop());
    }

    public void StopAll()
    {
        ExecuteCallbackForSources(_sources, source => source.Stop());
    }

    public void DestroyById(string id, StringComparison comparison)
    {
        ExecuteCallbackForSources(GetSourcesById(id, comparison), source => source.Destroy());
    }

    public void DestroyAll()
    {
        ExecuteCallbackForSources(_sources, source => source.Destroy());
    }

    public void Dispose()
    {
        ExecuteCallbackForSources(_sources, source => source.Dispose());
    }

    private IEnumerable<IAudioSource> GetSourcesById(string id, StringComparison comparison)
    {
        return _sources.FindAll(x => x.Id.Equals(id, comparison));
    }

    private void ExecuteCallbackForSources(IEnumerable<IAudioSource> sources, Action<IAudioSource> callback)
    {
        foreach (IAudioSource source in sources)
        {
            callback.Invoke(source);
        }
    }

    private void RemoveDestroyedSources()
    {
        for (var i = 0; i < _sources.Count; i++)
        {
            if (_sources[i].IsDestroyed)
            {
                _sources.RemoveAt(i);
            }
        }
    }
}