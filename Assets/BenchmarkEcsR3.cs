using System;
using EcsR3.Collections.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.Zenject;
using R3;
using SystemsR3.Infrastructure.Extensions;
using SystemsR3.Systems.Conventional;
using UnityEngine;
using Zenject;

public class BenchmarkEcsR3 : MonoBehaviour
{
    public static int ENTITY_COUNT = int.MaxValue;

    private void OnApplicationQuit()
    {
        ENTITY_COUNT = int.MaxValue;
    }

    public void IterationTest(int entitiesCount)
    {
        ENTITY_COUNT  = entitiesCount;
        var go = new GameObject();
        go.SetActive(false);
        go.AddComponent<SceneContext>();
        go.AddComponent<EcsR3IterationApplication>();
        go.SetActive(true);
    }

    public void SingleMigrationTest(int entitiesCount)
    {
        ENTITY_COUNT  = entitiesCount;
        var go = new GameObject();
        go.SetActive(false);
        go.AddComponent<SceneContext>();
        go.AddComponent<EcsR3SingleMigrationApplication>();
        go.SetActive(true);
    }

    public void TripleMigrationTest(int entitiesCount)
    {
        ENTITY_COUNT  = entitiesCount;
        var go = new GameObject();
        go.SetActive(false);
        go.AddComponent<SceneContext>();
        go.AddComponent<EcsR3TripleMigrationApplication>();
        go.SetActive(true);
    }
}
