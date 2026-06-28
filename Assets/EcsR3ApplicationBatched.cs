using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EcsR3.Components.Database;
using EcsR3.Components.Lookups;
using EcsR3.Computeds.Components.Registries;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Reflex;
using EcsR3.Systems.Batching.Convention;
using R3;
using SystemsR3.Infrastructure.Dependencies;
using SystemsR3.Infrastructure.Extensions;
using SystemsR3.Pools.Config;
using SystemsR3.Systems;
using SystemsR3.Threading;
using Test;

public class EcsR3IterationBatchedApplication : EcsR3ApplicationBehaviour
{
    public override ComponentDatabaseConfig OverrideComponentDatabaseConfig()
    {
        return new ComponentDatabaseConfig()
        {
            PoolSpecificConfig =
            {
                { typeof(EcsR3Test.TestComponent0), new PoolConfig(BenchmarkEcsR3.ENTITY_COUNT) },
                { typeof(EcsR3Test.TestComponent1), new PoolConfig(BenchmarkEcsR3.ENTITY_COUNT) },
                { typeof(EcsR3Test.TestComponent2), new PoolConfig(BenchmarkEcsR3.ENTITY_COUNT) },
                { typeof(EcsR3Test.TestComponent3), new PoolConfig(BenchmarkEcsR3.ENTITY_COUNT) },
            },
        };
    }

    protected override void LoadModules()
    {
        base.LoadModules();
        DependencyRegistry.Unbind<IComponentTypeAssigner>();
        DependencyRegistry.Bind<IComponentTypeAssigner, CustomComponentTypeAssigner>();
    }

    protected override void BindSystems()
    {
        // string str = this.GetType().Namespace;
        // string[] namespaces = new string[2] { str + ".Systems", str + ".ViewResolvers" };
        // List<Type> list = ((IEnumerable<Assembly>)AppDomain.CurrentDomain.GetAssemblies())
        //     .SelectMany<Assembly, Type>(
        //         (Func<Assembly, IEnumerable<Type>>)(x => (IEnumerable<Type>)x.GetTypes())
        //     )
        //     .Where<Type>(
        //         (Func<Type, bool>)(
        //             x =>
        //                 !x.IsInterface
        //                 && !x.IsAbstract
        //                 && !string.IsNullOrEmpty(x.Namespace)
        //                 && typeof(ISystem).IsAssignableFrom(x)
        //                 && ((IEnumerable<string>)namespaces).Any<string>(
        //                     (Func<string, bool>)(
        //                         namespaceToVerify => x.Namespace.Contains(namespaceToVerify)
        //                     )
        //                 )
        //         )
        //     )
        //     .ToList<Type>();
        // if (!list.Any<Type>())
        //     return;
        // foreach (Type systemType in list)
        // {
        //     BindingConfiguration configuration = new BindingConfiguration()
        //     {
        //         AsSingleton = true,
        //         WithName = systemType.Name,
        //     };
        //     this.DependencyRegistry.Bind(typeof(ISystem), systemType, configuration);
        // }
    }

    protected override void StartSystems()
    {
        // Debug.Log("EcsR3IterationApplicationBatched StartSystems");
        this.BindAndStartSystem<EcsR3IterationBatchedSystem>();
    }

    protected override void ApplicationStarted()
    {
        // var eca = (EcsR3.Entities.Accessors.EntityComponentAccessor)EntityComponentAccessor;
        // var ead = (EcsR3.Collections.Entities.EntityAllocationDatabase)eca.EntityAllocationDatabase;
        // ead.PreAllocate(BenchmarkEcsR3.ENTITY_COUNT);
        var entities = EntityCollection.CreateMany(BenchmarkEcsR3.ENTITY_COUNT);
        EntityComponentAccessor.CreateComponents<
            EcsR3Test.TestComponent0,
            EcsR3Test.TestComponent1,
            EcsR3Test.TestComponent2,
            EcsR3Test.TestComponent3
        >(entities);
        // for (var i = 0; i < BenchmarkEcsR3.ENTITY_COUNT; i++)
        // {
        //     var entity = EntityCollection.Create();
        //     EntityComponentAccessor.AddComponents(
        //         entity,
        //         new EcsR3Test.TestComponent0(),
        //         new EcsR3Test.TestComponent1(),
        //         new EcsR3Test.TestComponent2(),
        //         new EcsR3Test.TestComponent3()
        //     );
        // }
    }
}

public class EcsR3SingleMigrationBatchedApplication : EcsR3ApplicationBehaviour
{
    public override ComponentDatabaseConfig OverrideComponentDatabaseConfig()
    {
        return new ComponentDatabaseConfig()
        {
            PoolSpecificConfig =
            {
                { typeof(EcsR3Test.TestComponent0), new PoolConfig(BenchmarkEcsR3.ENTITY_COUNT) },
                { typeof(EcsR3Test.TestComponent1), new PoolConfig(BenchmarkEcsR3.ENTITY_COUNT) },
                { typeof(EcsR3Test.TestComponent2), new PoolConfig(BenchmarkEcsR3.ENTITY_COUNT) },
                { typeof(EcsR3Test.TestComponent3), new PoolConfig(BenchmarkEcsR3.ENTITY_COUNT) },
            },
        };
    }

    protected override void LoadModules()
    {
        base.LoadModules();
        DependencyRegistry.Unbind<IComponentTypeAssigner>();
        DependencyRegistry.Bind<IComponentTypeAssigner, CustomComponentTypeAssigner>();
    }

    protected override void BindSystems() { }

    protected override void StartSystems()
    {
        this.BindAndStartSystem<EcsR3SingleMigrationBatchedSystem>();
    }

    protected override void ApplicationStarted()
    {
        var entities = EntityCollection.CreateMany(BenchmarkEcsR3.ENTITY_COUNT);
        EntityComponentAccessor.CreateComponents<
            EcsR3Test.TestComponent0,
            EcsR3Test.TestComponent1,
            EcsR3Test.TestComponent2,
            EcsR3Test.TestComponent3
        >(entities);
    }
}

public class EcsR3TripleMigrationBatchedApplication : EcsR3ApplicationBehaviour
{
    public override ComponentDatabaseConfig OverrideComponentDatabaseConfig()
    {
        return new ComponentDatabaseConfig()
        {
            PoolSpecificConfig =
            {
                { typeof(EcsR3Test.TestComponent0), new PoolConfig(BenchmarkEcsR3.ENTITY_COUNT) },
                { typeof(EcsR3Test.TestComponent1), new PoolConfig(BenchmarkEcsR3.ENTITY_COUNT) },
                { typeof(EcsR3Test.TestComponent2), new PoolConfig(BenchmarkEcsR3.ENTITY_COUNT) },
                { typeof(EcsR3Test.TestComponent3), new PoolConfig(BenchmarkEcsR3.ENTITY_COUNT) },
            },
        };
    }

    protected override void LoadModules()
    {
        base.LoadModules();
        DependencyRegistry.Unbind<IComponentTypeAssigner>();
        DependencyRegistry.Bind<IComponentTypeAssigner, CustomComponentTypeAssigner>();
    }

    protected override void BindSystems() { }

    protected override void StartSystems()
    {
        this.BindAndStartSystem<EcsR3TripleMigrationBatchedSystem>();
    }

    protected override void ApplicationStarted()
    {
        var entities = EntityCollection.CreateMany(BenchmarkEcsR3.ENTITY_COUNT);
        EntityComponentAccessor.CreateComponents<
            EcsR3Test.TestComponent0,
            EcsR3Test.TestComponent1,
            EcsR3Test.TestComponent2,
            EcsR3Test.TestComponent3
        >(entities);
    }
}

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class EcsR3IterationBatchedSystem
    : BatchedSystem<
        EcsR3Test.TestComponent0,
        EcsR3Test.TestComponent1,
        EcsR3Test.TestComponent2,
        EcsR3Test.TestComponent3
    >
{
    public EcsR3IterationBatchedSystem(
        IComponentDatabase componentDatabase,
        IEntityComponentAccessor entityComponentAccessor,
        IComputedComponentGroupRegistry computedComponentGroupRegistry,
        IThreadHandler threadHandler
    )
        : base(
            componentDatabase,
            entityComponentAccessor,
            computedComponentGroupRegistry,
            threadHandler
        ) { }

    protected override Observable<Unit> ReactWhen()
    {
        return Observable.EveryUpdate();
    }

    protected override void Process(
        Entity entity,
        ref EcsR3Test.TestComponent0 component0,
        ref EcsR3Test.TestComponent1 component1,
        ref EcsR3Test.TestComponent2 component2,
        ref EcsR3Test.TestComponent3 component3
    )
    {
        component0.Test++;
        component1.Test++;
        component2.Test++;
        component3.Test++;
        // Debug.Log($"Time.frameCount : {Time.frameCount}");
        // Debug.Log($"Time.frameCount : {Time.frameCount}  EntityId : {entity.Id}  TestCount : {component3.Test}");
    }
}

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class EcsR3SingleMigrationBatchedSystem
    : BatchedSystem<
        EcsR3Test.TestComponent0,
        EcsR3Test.TestComponent1,
        EcsR3Test.TestComponent2,
        EcsR3Test.TestComponent3
    >
{
    public EcsR3SingleMigrationBatchedSystem(
        IComponentDatabase componentDatabase,
        IEntityComponentAccessor entityComponentAccessor,
        IComputedComponentGroupRegistry computedComponentGroupRegistry,
        IThreadHandler threadHandler
    )
        : base(
            componentDatabase,
            entityComponentAccessor,
            computedComponentGroupRegistry,
            threadHandler
        ) { }

    protected override Observable<Unit> ReactWhen()
    {
        return Observable.EveryUpdate();
    }

    protected override void Process(
        Entity entity,
        ref EcsR3Test.TestComponent0 component0,
        ref EcsR3Test.TestComponent1 component1,
        ref EcsR3Test.TestComponent2 component2,
        ref EcsR3Test.TestComponent3 component3
    )
    {
        // // EcsR3.Components.ComponentPool<>
        // var pool = this.ComponentDatabase.GetPoolFor<EcsR3Test.TestComponent3>();
        // var eca = (EcsR3.Entities.Accessors.EntityComponentAccessor)EntityComponentAccessor;
        // var ead = (EcsR3.Collections.Entities.EntityAllocationDatabase)eca.EntityAllocationDatabase;
        // var ctl = (EcsR3.Components.Lookups.ComponentTypeLookup)eca.ComponentTypeLookup;
        // var componentTypeId = ctl.GetComponentTypeId(typeof(EcsR3Test.TestComponent3));
        // var index = ead.ComponentAllocationData[componentTypeId, entity.Id];
        // var c = pool.Get(index);

        EntityComponentAccessor.RemoveComponent<EcsR3Test.TestComponent3>(entity);
        // EntityComponentAccessor.AddComponent(entity, new EcsR3Test.TestComponent3());
        EntityComponentAccessor.CreateComponent<EcsR3Test.TestComponent3>(entity);
        // EntityComponentAccessor.AddComponent(entity, c);
        // Debug.Log($"Time.frameCount : {Time.frameCount}");
        // Debug.Log($"Time.frameCount : {Time.frameCount}  EntityId : {entity.Id}");
    }
}

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class EcsR3TripleMigrationBatchedSystem
    : BatchedSystem<
        EcsR3Test.TestComponent0,
        EcsR3Test.TestComponent1,
        EcsR3Test.TestComponent2,
        EcsR3Test.TestComponent3
    >
{
    public EcsR3TripleMigrationBatchedSystem(
        IComponentDatabase componentDatabase,
        IEntityComponentAccessor entityComponentAccessor,
        IComputedComponentGroupRegistry computedComponentGroupRegistry,
        IThreadHandler threadHandler
    )
        : base(
            componentDatabase,
            entityComponentAccessor,
            computedComponentGroupRegistry,
            threadHandler
        ) { }

    protected override Observable<Unit> ReactWhen()
    {
        return Observable.EveryUpdate();
    }

    protected override void Process(
        Entity entity,
        ref EcsR3Test.TestComponent0 component0,
        ref EcsR3Test.TestComponent1 component1,
        ref EcsR3Test.TestComponent2 component2,
        ref EcsR3Test.TestComponent3 component3
    )
    {
        EntityComponentAccessor.RemoveComponents(
            entity,
            typeof(EcsR3Test.TestComponent1),
            typeof(EcsR3Test.TestComponent2),
            typeof(EcsR3Test.TestComponent3)
        );
        EntityComponentAccessor.CreateComponents<
            EcsR3Test.TestComponent1,
            EcsR3Test.TestComponent2,
            EcsR3Test.TestComponent3
        >(entity);
        // Debug.Log($"Time.frameCount : {Time.frameCount}");
        // Debug.Log($"Time.frameCount : {Time.frameCount}  EntityId : {entity.Id}");
    }
}

public class CustomComponentTypeAssigner : IComponentTypeAssigner
{
    public IEnumerable<Type> GetAllComponentTypes()
    {
        string[] assemblyShortNames = new string[] { "Assembly-CSharp" };
        var assemblies = assemblyShortNames.Select(Assembly.Load);
        Type componentType = typeof(EcsR3.Components.IComponent);
        return ((IEnumerable<Assembly>)assemblies)
            .SelectMany<Assembly, Type>(
                (Func<Assembly, IEnumerable<Type>>)(s => (IEnumerable<Type>)s.GetTypes())
            )
            .Where<Type>(
                (Func<Type, bool>)(
                    p => componentType.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract
                )
            );
    }

    public IReadOnlyDictionary<Type, int> GenerateComponentLookups()
    {
        int lookupId = 0;
        IEnumerable<Type> allComponentTypes = this.GetAllComponentTypes();
        return allComponentTypes == null
            ? (IReadOnlyDictionary<Type, int>)null
            : (IReadOnlyDictionary<Type, int>)
                allComponentTypes.ToDictionary<Type, Type, int>(
                    (Func<Type, Type>)(x => x),
                    (Func<Type, int>)(_ => lookupId++)
                );
    }
}
