using DCFApixels.DragonECS;
using Test;
using UnityEngine;
using EcsWorld = DCFApixels.DragonECS.EcsWorld;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class BenchmarkDragon : MonoBehaviour
{
    private EcsDefaultWorld  _world;
    private EcsPipeline _pipeline;
    
    public void IterationTest(int entitiesCount)
    {
        _world = new EcsDefaultWorld();
        _pipeline = EcsPipeline.New()
            .Add(new DragonIterationSystem(entitiesCount))
            .Inject(_world)
            .Build();
        _pipeline.Init();
    }
    
    public void SingleMigrationTest(int entitiesCount)
    {
        _world = new EcsDefaultWorld();
        _pipeline = EcsPipeline.New()
            .Add(new DragonSingleMigrationSystem(entitiesCount))
            .Inject(_world)
            .Build();
        _pipeline.Init();
    }
    
    public void TripleMigrationTest(int entitiesCount)
    {
        _world = new EcsDefaultWorld();
        _pipeline = EcsPipeline.New()
            .Add(new DragonTripleMigrationSystem(entitiesCount))
            .Inject(_world)
            .Build();
        _pipeline.Init();
    }
    
    private void Update () 
    {
        _pipeline?.Run ();
    }

    private void OnDestroy () 
    {
        if (_pipeline != null) 
        {
            _pipeline.Destroy ();
            _pipeline = null;
        }
        if (_world != null) 
        {
            _world.Destroy ();
            _world = null;
        }
    }
}

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class DragonIterationSystem : IEcsInject<EcsDefaultWorld>, IEcsInit, IEcsRun 
{
    EcsDefaultWorld _world;
    
    private int entitiesCount;

    public DragonIterationSystem(int entitiesCount)
    {
        this.entitiesCount = entitiesCount;
    }
    
    public void Init () {
        var a3 = _world.GetAspect<Aspect3> ();
        for (var i = 0; i < entitiesCount; i++) {
            var e = _world.NewEntity ();
            a3.C0.Add (e);
            a3.C1.Add (e);
            a3.C2.Add (e);
            a3.C3.Add (e);
        }
    }

    public void Inject (EcsDefaultWorld obj) {
        _world = obj;
    }

    public void Run () {
        foreach (var ent in _world.Where (out Aspect3 a3)) {
            a3.C0.Get (ent).Test++;
            a3.C1.Get (ent).Test++;
            a3.C2.Get (ent).Test++;
            a3.C3.Get (ent).Test++;
        }
    }
}

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class DragonSingleMigrationSystem : IEcsInject<EcsDefaultWorld>, IEcsInit, IEcsRun 
{
    EcsDefaultWorld _world;
    
    private int entitiesCount;

    public DragonSingleMigrationSystem(int entitiesCount)
    {
        this.entitiesCount = entitiesCount;
    }
    
    public void Init () {
        var a3 = _world.GetAspect<Aspect3> ();
        for (var i = 0; i < entitiesCount; i++) {
            var e = _world.NewEntity ();
            a3.C0.Add (e);
            a3.C1.Add (e);
            a3.C2.Add (e);
            a3.C3.Add (e);
        }
    }

    public void Inject (EcsDefaultWorld obj) {
        _world = obj;
    }

    public void Run () {
        foreach (var ent in _world.Where (out Aspect3 a3)) {
            a3.C3.Del (ent);
        }

        foreach (var ent in _world.Where (out Aspect0 a0)) {
            a0.C3.Add (ent);
        }
    }
}

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class DragonTripleMigrationSystem : IEcsInject<EcsDefaultWorld>, IEcsInit, IEcsRun 
{
    EcsDefaultWorld _world;
    
    private int entitiesCount;

    public DragonTripleMigrationSystem(int entitiesCount)
    {
        this.entitiesCount = entitiesCount;
    }
    
    public void Init () {
        var a3 = _world.GetAspect<Aspect3> ();
        for (var i = 0; i < entitiesCount; i++) {
            var e = _world.NewEntity ();
            a3.C0.Add (e);
            a3.C1.Add (e);
            a3.C2.Add (e);
            a3.C3.Add (e);
        }
    }

    public void Inject (EcsDefaultWorld obj) {
        _world = obj;
    }

    public void Run () {
        foreach (var ent in _world.Where (out Aspect3 a3)) {
            a3.C1.Del (ent);
            a3.C2.Del (ent);
            a3.C3.Del (ent);
        }

        foreach (var ent in _world.Where (out Aspect0 a0)) {
            a0.C1.Add (ent);
            a0.C2.Add (ent);
            a0.C3.Add (ent);
        }
    }
}

class Aspect0 : EcsAspect {
    public DCFApixels.DragonECS.EcsPool<DragonTest.TestComponent0> C0 = Inc;
    public DCFApixels.DragonECS.EcsPool<DragonTest.TestComponent1> C1 = Opt;
    public DCFApixels.DragonECS.EcsPool<DragonTest.TestComponent2> C2 = Opt;
    public DCFApixels.DragonECS.EcsPool<DragonTest.TestComponent3> C3 = Opt;
}

class Aspect1 : EcsAspect {
    public DCFApixels.DragonECS.EcsPool<DragonTest.TestComponent0> C0 = Inc;
    public DCFApixels.DragonECS.EcsPool<DragonTest.TestComponent1> C1 = Inc;
}

class Aspect2 : EcsAspect {
    public DCFApixels.DragonECS.EcsPool<DragonTest.TestComponent0> C0 = Inc;
    public DCFApixels.DragonECS.EcsPool<DragonTest.TestComponent1> C1 = Inc;
    public DCFApixels.DragonECS.EcsPool<DragonTest.TestComponent2> C2 = Inc;
}

class Aspect3 : EcsAspect {
    public DCFApixels.DragonECS.EcsPool<DragonTest.TestComponent0> C0 = Inc;
    public DCFApixels.DragonECS.EcsPool<DragonTest.TestComponent1> C1 = Inc;
    public DCFApixels.DragonECS.EcsPool<DragonTest.TestComponent2> C2 = Inc;
    public DCFApixels.DragonECS.EcsPool<DragonTest.TestComponent3> C3 = Inc;
}