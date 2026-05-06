using Leopotam.EcsLite;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Test;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class BenchmarkLeoProto : MonoBehaviour
{
    private ProtoWorld _world;
    private IProtoSystems _systems;

    public void IterationTest(int entitiesCount)
    {
        _world = new ProtoWorld(new TestAspect());
        _systems = new ProtoSystems(_world);
        _systems.AddSystem(new LeoProtoIterationSystem(entitiesCount)).Init();
    }

    public void SingleMigrationTest(int entitiesCount)
    {
        _world = new ProtoWorld(new TestAspect());
        _systems = new ProtoSystems(_world);
        _systems.AddSystem(new LeoProtoSingleMigrationSystem(entitiesCount)).Init();
    }

    public void TripleMigrationTest(int entitiesCount)
    {
        _world = new ProtoWorld(new TestAspect());
        _systems = new ProtoSystems(_world);
        _systems.AddSystem(new LeoProtoTripleMigrationSystem(entitiesCount)).Init();
    }

    private void Update()
    {
        _systems?.Run();
    }

    private void OnDestroy()
    {
        if (_systems != null)
        {
            _systems.Destroy();
            _systems = null;
        }
        if (_world != null)
        {
            _world.Destroy();
            _world = null;
        }
    }
}

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class LeoProtoIterationSystem : IProtoInitSystem, IProtoRunSystem
{
    private int entitiesCount;

    public LeoProtoIterationSystem(int entitiesCount)
    {
        this.entitiesCount = entitiesCount;
    }

    //[DI]
    TestAspect _aspect;

    public void Init(IProtoSystems systems)
    {
        _aspect = (TestAspect)systems.World().Aspect(typeof(TestAspect));
        for (var i = 0; i < this.entitiesCount; i++)
        {
            _aspect.Pool0.NewEntity(out ProtoEntity e);
            _aspect.Pool1.Add(e);
            _aspect.Pool2.Add(e);
            _aspect.Pool3.Add(e);
        }
    }

    public void Run()
    {
        foreach (var ent in _aspect.It3)
        {
            _aspect.Pool0.Get(ent).Test++;
            _aspect.Pool1.Get(ent).Test++;
            _aspect.Pool2.Get(ent).Test++;
            _aspect.Pool3.Get(ent).Test++;
        }
    }
}

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class LeoProtoSingleMigrationSystem : IProtoInitSystem, IProtoRunSystem
{
    private int entitiesCount;

    public LeoProtoSingleMigrationSystem(int entitiesCount)
    {
        this.entitiesCount = entitiesCount;
    }

    //[DI]
    TestAspect _aspect;

    public void Init(IProtoSystems systems)
    {
        _aspect = (TestAspect)systems.World().Aspect(typeof(TestAspect));
        for (var i = 0; i < this.entitiesCount; i++)
        {
            _aspect.Pool0.NewEntity(out ProtoEntity e);
            _aspect.Pool1.Add(e);
            _aspect.Pool2.Add(e);
            _aspect.Pool3.Add(e);
        }
    }

    public void Run()
    {
        foreach (var ent in _aspect.It3)
        {
            _aspect.Pool3.Del(ent);
        }

        foreach (var ent in _aspect.It0)
        {
            _aspect.Pool3.Add(ent);
        }
    }
}

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class LeoProtoTripleMigrationSystem : IProtoInitSystem, IProtoRunSystem
{
    private int entitiesCount;

    public LeoProtoTripleMigrationSystem(int entitiesCount)
    {
        this.entitiesCount = entitiesCount;
    }

    //[DI]
    TestAspect _aspect;

    public void Init(IProtoSystems systems)
    {
        _aspect = (TestAspect)systems.World().Aspect(typeof(TestAspect));
        for (var i = 0; i < this.entitiesCount; i++)
        {
            _aspect.Pool0.NewEntity(out ProtoEntity e);
            _aspect.Pool1.Add(e);
            _aspect.Pool2.Add(e);
            _aspect.Pool3.Add(e);
        }
    }

    public void Run()
    {
        foreach (var ent in _aspect.It3)
        {
            _aspect.Pool1.Del(ent);
            _aspect.Pool2.Del(ent);
            _aspect.Pool3.Del(ent);
        }

        foreach (var ent in _aspect.It0)
        {
            _aspect.Pool1.Add(ent);
            _aspect.Pool2.Add(ent);
            _aspect.Pool3.Add(ent);
        }
    }
}

class TestAspect : ProtoAspectInject
{
    public ProtoPool<Test.TestComponent0> Pool0;
    public ProtoPool<Test.TestComponent1> Pool1;
    public ProtoPool<Test.TestComponent2> Pool2;
    public ProtoPool<Test.TestComponent3> Pool3;

    public ProtoIt It0 = new(It.Inc<Test.TestComponent0>());
    public ProtoIt It1 = new(It.Inc<Test.TestComponent0, Test.TestComponent1>());
    public ProtoIt It2 = new(
        It.Inc<Test.TestComponent0, Test.TestComponent1, Test.TestComponent2>()
    );
    public ProtoIt It3 = new(
        It.Inc<Test.TestComponent0, Test.TestComponent1, Test.TestComponent2, Test.TestComponent3>()
    );
}
