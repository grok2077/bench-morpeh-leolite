using EcsR3.Collections;
using EcsR3.Computeds.Entities.Registries;
using Reflex.Attributes;
using SystemsR3.Executor;
using UnityEngine;

namespace EcsR3.UnityEditor.MonoBehaviours
{
    public class ActiveSystemsViewer : MonoBehaviour
    {
        [Inject]
        public ISystemExecutor SystemExecutor { get; private set; }
        
        [Inject]
        public IComputedEntityGroupRegistry ObservableGroupManager { get; private set; }
    }
}