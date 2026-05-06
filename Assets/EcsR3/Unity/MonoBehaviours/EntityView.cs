using EcsR3.Collections.Entities;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using Zenject;

namespace EcsR3.Unity.MonoBehaviours
{
    public class EntityView : InjectableMonoBehaviour
    {
        [Inject]
        public IEntityComponentAccessor EntityComponentAccessor { get; set; }
        
        public Entity Entity { get; set; } = new(IEntityAllocationDatabase.NoAllocation, 0);

        public override void DependenciesResolved()
        { }
    }
}