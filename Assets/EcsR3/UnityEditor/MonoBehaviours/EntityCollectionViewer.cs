using EcsR3.Collections.Entities;
using Reflex.Attributes;
using UnityEngine;

namespace EcsR3.UnityEditor.MonoBehaviours
{
    public class EntityCollectionViewer : MonoBehaviour
    {
         [Inject]
         public IEntityCollection EntityCollection { get; private set; }
    }
}