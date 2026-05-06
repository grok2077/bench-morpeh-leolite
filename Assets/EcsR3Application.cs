using System;
using System.Collections.Generic;
using EcsR3.Collections.Entities;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Zenject;
using R3;
using SystemsR3.Infrastructure.Extensions;
using SystemsR3.Systems.Conventional;
using Test;
using UnityEngine;
using Zenject;

public class EcsR3IterationApplication : EcsR3ApplicationBehaviour
{
    protected override void StartSystems()
    {
        this.BindAndStartSystem<EcsR3IterationSystem>();
    }

    protected override void ApplicationStarted() { }
}

public class EcsR3SingleMigrationApplication : EcsR3ApplicationBehaviour
{
    protected override void StartSystems()
    {
        this.BindAndStartSystem<EcsR3SingleMigrationSystem>();
    }

    protected override void ApplicationStarted() { }
}

public class EcsR3TripleMigrationApplication : EcsR3ApplicationBehaviour
{
    protected override void StartSystems()
    {
        this.BindAndStartSystem<EcsR3TripleMigrationSystem>();
    }

    protected override void ApplicationStarted() { }
}

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class EcsR3IterationSystem : IManualSystem
{
    private IEntityCollection EntityCollection;

    private IEntityComponentAccessor EntityComponentAccessor;

    private IDisposable _updateLoop;

    private IEnumerable<Entity> _matchingGroup;

    private static readonly IGroup Group = new Group(
        typeof(EcsR3Test.TestComponent0),
        typeof(EcsR3Test.TestComponent1),
        typeof(EcsR3Test.TestComponent2),
        typeof(EcsR3Test.TestComponent3)
    );

    public EcsR3IterationSystem(
        IEntityCollection entityCollection,
        IEntityComponentAccessor entityComponentAccessor
    )
    {
        EntityCollection = entityCollection;
        EntityComponentAccessor = entityComponentAccessor;
        _matchingGroup = EntityCollection.MatchingGroup(EntityComponentAccessor, Group);
    }

    public void StartSystem()
    {
        for (int i = 0; i < BenchmarkEcsR3.ENTITY_COUNT; i++)
        {
            var entity = EntityCollection.Create();
            EntityComponentAccessor.AddComponents(
                entity,
                new EcsR3Test.TestComponent0(),
                new EcsR3Test.TestComponent1(),
                new EcsR3Test.TestComponent2(),
                new EcsR3Test.TestComponent3()
            );
        }
        _updateLoop = Observable.EveryUpdate().Subscribe(OnUpdate);
    }

    private void OnUpdate(Unit _)
    {
        foreach (var entity in EntityCollection.MatchingGroup(EntityComponentAccessor, Group)
        //var entity in _matchingGroup
        )
        {
            EntityComponentAccessor.GetComponentRef<EcsR3Test.TestComponent0>(entity).Test++;
            EntityComponentAccessor.GetComponentRef<EcsR3Test.TestComponent1>(entity).Test++;
            EntityComponentAccessor.GetComponentRef<EcsR3Test.TestComponent2>(entity).Test++;
            EntityComponentAccessor.GetComponentRef<EcsR3Test.TestComponent3>(entity).Test++;
        }
    }

    public void StopSystem()
    {
        _updateLoop?.Dispose();
    }
}

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class EcsR3SingleMigrationSystem : IManualSystem
{
    public IEntityCollection EntityCollection { get; private set; }

    public IEntityComponentAccessor EntityComponentAccessor { get; private set; }

    private IDisposable _updateLoop;

    private static readonly IGroup Group3 = new Group(
        typeof(EcsR3Test.TestComponent0),
        typeof(EcsR3Test.TestComponent1),
        typeof(EcsR3Test.TestComponent2),
        typeof(EcsR3Test.TestComponent3)
    );

    private static readonly IGroup Group0 = new Group(typeof(EcsR3Test.TestComponent0));

    public EcsR3SingleMigrationSystem(
        IEntityCollection entityCollection,
        IEntityComponentAccessor entityComponentAccessor
    )
    {
        EntityCollection = entityCollection;
        EntityComponentAccessor = entityComponentAccessor;
    }

    public void StartSystem()
    {
        for (int i = 0; i < BenchmarkEcsR3.ENTITY_COUNT; i++)
        {
            var entity = EntityCollection.Create();
            EntityComponentAccessor.AddComponents(
                entity,
                new EcsR3Test.TestComponent0(),
                new EcsR3Test.TestComponent1(),
                new EcsR3Test.TestComponent2(),
                new EcsR3Test.TestComponent3()
            );
        }
        _updateLoop = Observable
            .EveryUpdate()
            .Subscribe(x =>
            {
                foreach (
                    var entity in EntityCollection.MatchingGroup(EntityComponentAccessor, Group3)
                )
                {
                    EntityComponentAccessor.RemoveComponent<EcsR3Test.TestComponent3>(entity);
                }
                foreach (
                    var entity in EntityCollection.MatchingGroup(EntityComponentAccessor, Group0)
                )
                {
                    EntityComponentAccessor.AddComponent(entity, new EcsR3Test.TestComponent3());
                }
            });
    }

    public void StopSystem()
    {
        _updateLoop?.Dispose();
    }
}

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class EcsR3TripleMigrationSystem : IManualSystem
{
    public IEntityCollection EntityCollection { get; private set; }

    public IEntityComponentAccessor EntityComponentAccessor { get; private set; }

    private IDisposable _updateLoop;

    private static readonly IGroup Group3 = new Group(
        typeof(EcsR3Test.TestComponent0),
        typeof(EcsR3Test.TestComponent1),
        typeof(EcsR3Test.TestComponent2),
        typeof(EcsR3Test.TestComponent3)
    );

    private static readonly IGroup Group0 = new Group(typeof(EcsR3Test.TestComponent0));

    public EcsR3TripleMigrationSystem(
        IEntityCollection entityCollection,
        IEntityComponentAccessor entityComponentAccessor
    )
    {
        EntityCollection = entityCollection;
        EntityComponentAccessor = entityComponentAccessor;
    }

    public void StartSystem()
    {
        for (int i = 0; i < BenchmarkEcsR3.ENTITY_COUNT; i++)
        {
            var entity = EntityCollection.Create();
            EntityComponentAccessor.AddComponents(
                entity,
                new EcsR3Test.TestComponent0(),
                new EcsR3Test.TestComponent1(),
                new EcsR3Test.TestComponent2(),
                new EcsR3Test.TestComponent3()
            );
        }
        _updateLoop = Observable
            .EveryUpdate()
            .Subscribe(x =>
            {
                foreach (
                    var entity in EntityCollection.MatchingGroup(EntityComponentAccessor, Group3)
                )
                {
                    EntityComponentAccessor.RemoveComponents(
                        entity,
                        typeof(EcsR3Test.TestComponent1),
                        typeof(EcsR3Test.TestComponent2),
                        typeof(EcsR3Test.TestComponent3)
                    );
                }
                foreach (
                    var entity in EntityCollection.MatchingGroup(EntityComponentAccessor, Group0)
                )
                {
                    EntityComponentAccessor.AddComponents(
                        entity,
                        new EcsR3Test.TestComponent1(),
                        new EcsR3Test.TestComponent2(),
                        new EcsR3Test.TestComponent3()
                    );
                }
            });
    }

    public void StopSystem()
    {
        _updateLoop?.Dispose();
    }
}
