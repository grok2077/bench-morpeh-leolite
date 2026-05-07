using DCFApixels.DragonECS;
using EcsR3.Components;
using TMPro;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public GameObject rootOverlay;
    public TMP_InputField inputField;

    public BenchmarkDragon dragon;
    
    public BenchmarkLeoProto leoProte;
    
    public BenchmarkLeo leoLite;
    
    public BenchmarkMorpeh morpeh;
    
    public BenchmarkEcsR3 ecsr3;

    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        QualitySettings.vSyncCount = 0;
        // Application.targetFrameRate = Screen.currentResolution.refreshRate;
        Application.targetFrameRate = 90;
#endif
    }
    
    public void LeoProIteration()
    {
        rootOverlay.SetActive(false);
        leoProte.IterationTest(int.Parse(inputField.text));
    }
    
    public void LeoProSingleMigration()
    {
        rootOverlay.SetActive(false);
        leoProte.SingleMigrationTest(int.Parse(inputField.text));
    }
    
    public void LeoProTripleMigration()
    {
        rootOverlay.SetActive(false);
        leoProte.TripleMigrationTest(int.Parse(inputField.text));
    }
    
    public void LeoLiteIteration()
    {
        rootOverlay.SetActive(false);
        leoLite.IterationTest(int.Parse(inputField.text));
    }
    
    public void LeoLiteSingleMigration()
    {
        rootOverlay.SetActive(false);
        leoLite.SingleMigrationTest(int.Parse(inputField.text));
    }
    
    public void LeoLiteTripleMigration()
    {
        rootOverlay.SetActive(false);
        leoLite.TripleMigrationTest(int.Parse(inputField.text));
    }
    
    public void MorpehIteration()
    {
        rootOverlay.SetActive(false);
        morpeh.IterationTest(int.Parse(inputField.text));
    }
    
    public void MorpehSingleMigration()
    {
        rootOverlay.SetActive(false);
        morpeh.SingleMigrationTest(int.Parse(inputField.text));
    }
    
    public void MorpehTripleMigration()
    {
        rootOverlay.SetActive(false);
        morpeh.TripleMigrationTest(int.Parse(inputField.text));
    }
    
    public void DragonIteration()
    {
        rootOverlay.SetActive(false);
        dragon.IterationTest(int.Parse(inputField.text));
    }
    
    public void DragonSingleMigration()
    {
        rootOverlay.SetActive(false);
        dragon.SingleMigrationTest(int.Parse(inputField.text));
    }
    
    public void DragonTripleMigration()
    {
        rootOverlay.SetActive(false);
        dragon.TripleMigrationTest(int.Parse(inputField.text));
    }
    
    
    public void EcsR3Iteration()
    {
        rootOverlay.SetActive(false);
        ecsr3.IterationTest(int.Parse(inputField.text));
    }
    
    public void EcsR3SingleMigration()
    {
        rootOverlay.SetActive(false);
        ecsr3.SingleMigrationTest(int.Parse(inputField.text));
    }
    
    public void EcsR3TripleMigration()
    {
        rootOverlay.SetActive(false);
        ecsr3.TripleMigrationTest(int.Parse(inputField.text));
    }
    
    public void EcsR3IterationBatched()
    {
        rootOverlay.SetActive(false);
        ecsr3.IterationBatchedTest(int.Parse(inputField.text));
    }
    
    public void EcsR3SingleMigrationBatched()
    {
        rootOverlay.SetActive(false);
        ecsr3.SingleMigrationBatchedTest(int.Parse(inputField.text));
    }
    
    public void EcsR3TripleMigrationBatched()
    {
        rootOverlay.SetActive(false);
        ecsr3.TripleMigrationBatchedTest(int.Parse(inputField.text));
    }
}

#if ENABLE_IL2CPP
// Unity IL2CPP performance optimization attribute.
namespace Test
{
    using System;

    enum Option
    {
        NullChecks = 1,
        ArrayBoundsChecks = 2,
        DivideByZeroChecks = 3,
    }

    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property,
        Inherited = false,
        AllowMultiple = true
    )]
    class Il2CppSetOptionAttribute : Attribute
    {
        public Option Option { get; private set; }
        public object Value { get; private set; }

        public Il2CppSetOptionAttribute(Option option, object value)
        {
            Option = option;
            Value = value;
        }
    }

    public struct TestComponent0
    {
        public int Test;
    }

    public struct TestComponent1
    {
        public int Test;
    }

    public struct TestComponent2
    {
        public int Test;
    }

    public struct TestComponent3
    {
        public int Test;
    }
}
#endif

namespace DragonTest
{
    public struct TestComponent0 : IEcsComponent
    {
        public int Test;
    }

    public struct TestComponent1 : IEcsComponent
    {
        public int Test;
    }

    public struct TestComponent2 : IEcsComponent
    {
        public int Test;
    }

    public struct TestComponent3 : IEcsComponent
    {
        public int Test;
    }
}

namespace EcsR3Test
{
    public struct TestComponent0 : IComponent
    {
        public int Test { get; set; }
    }

    public struct TestComponent1 : IComponent
    {
        public int Test { get; set; }
    }

    public struct TestComponent2 : IComponent
    {
        public int Test { get; set; }
    }

    public struct TestComponent3 : IComponent
    {
        public int Test { get; set; }
    }
}
