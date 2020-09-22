using UnityEngine;

namespace MyStateMachine
{
    public abstract class AbstractTrigger : MonoBehaviour
    {
        public abstract bool Predicate();
    }
}