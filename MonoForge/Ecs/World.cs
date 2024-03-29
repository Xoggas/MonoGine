﻿using System.Collections.Generic;
using System.Linq;

namespace MonoForge.Ecs;

public class World : IObject, IUpdatable
{
    private readonly List<IEntity> _entities;

    internal World()
    {
        _entities = new List<IEntity>();
    }

    public void AddEntity(IEntity entity)
    {
        _entities.Add(entity);
    }

    public IEnumerable<T> GetEntitiesOfType<T>() where T : IEntity
    {
        return (IEnumerable<T>)from entity in _entities where entity is T select entity;
    }

    public void DestroyEntitiesOfType<T>() where T : IEntity
    {
        foreach (T entity in GetEntitiesOfType<T>())
        {
            entity.Destroy();
        }
    }

    public void DestroyAllEntities()
    {
        for (var i = 0; i < _entities.Count; i++)
        {
            IEntity entity = _entities[i];
            entity.Destroy();
        }
    }

    public void Update(GameBase gameBase, float deltaTime)
    {
        for (var i = 0; i < _entities.Count; i++)
        {
            IEntity entity = _entities[i];

            if (entity.ShouldBeSkipped)
            {
                continue;
            }

            entity.Update(gameBase, deltaTime);
        }

        RemoveDestroyedEntities();
    }

    public void Dispose()
    {
        for (var i = 0; i < _entities.Count; i++)
        {
            _entities[i].Dispose();
        }
    }

    private void RemoveDestroyedEntities()
    {
        _entities.RemoveAll(entity =>
        {
            if (entity.IsDestroyed)
            {
                entity.Dispose();
            }

            return entity.IsDestroyed;
        });
    }
}