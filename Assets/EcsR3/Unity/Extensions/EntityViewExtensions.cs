using System.Collections.Generic;
using EcsR3.Collections.Entities;
using EcsR3.Components;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Unity.MonoBehaviours;
using UnityEngine;

namespace EcsR3.Unity.Extensions
{
    public static class EntityViewExtensions
    {
        public static T GetEcsComponent<T>(this EntityView entityView) where T : IComponent
        {
            if (entityView.Entity.Id == EntityAllocationDatabase.NoAllocation)
            {
                Debug.LogWarning("EntityView is not initialized with a valid Entity, cannot GetEcsComponent");
                return default;
            }
            return entityView.EntityComponentAccessor.GetComponent<T>(entityView.Entity);
        }

        public static IEnumerable<IComponent> GetEcsComponents(this EntityView entityView)
        {
            if (entityView.Entity.Id == EntityAllocationDatabase.NoAllocation)
            {
                Debug.LogWarning("EntityView is not initialized with a valid Entity, cannot GetEcsComponents");
                return default;
            }
            return entityView.EntityComponentAccessor.GetComponents(entityView.Entity);
        }

        public static bool HasEcsComponent<T>(this EntityView entityView) where T : IComponent
        {
            if (entityView.Entity.Id == EntityAllocationDatabase.NoAllocation)
            {
                Debug.LogWarning("EntityView is not initialized with a valid Entity, cannot check for HasEcsComponent");
                return false;
            }
            return entityView.EntityComponentAccessor.HasComponent<T>(entityView.Entity);
        }

        public static void RemoveEcsComponent<T>(this EntityView entityView) where T : IComponent
        {
            if (entityView.Entity.Id == EntityAllocationDatabase.NoAllocation)
            {
                Debug.LogWarning("EntityView is not initialized with a valid Entity, cannot RemoveEcsComponent");
                return;
            }
            entityView.EntityComponentAccessor.RemoveComponent<T>(entityView.Entity);
        }

        public static void AddEcsComponent<T>(this EntityView entityView) where T : IComponent, new()
        {
            if (entityView.Entity.Id == EntityAllocationDatabase.NoAllocation)
            {
                Debug.LogWarning("EntityView is not initialized with a valid Entity, cannot AddEcsComponent");
                return;
            }
            entityView.EntityComponentAccessor.CreateComponent<T>(entityView.Entity);
        }

        public static void AddEcsComponent(this EntityView entityView, IComponent component)
        {
            if (entityView.Entity.Id == EntityAllocationDatabase.NoAllocation)
            {
                Debug.LogWarning("EntityView is not initialized with a valid Entity, cannot AddEcsComponent");
                return;
            }
            entityView.EntityComponentAccessor.AddComponent(entityView.Entity, component);
        }

    }
}