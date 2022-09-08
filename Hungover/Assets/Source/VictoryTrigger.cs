using FMODUnity;
using UnityEngine;

namespace Hungover
{
    public class VictoryTrigger : MonoBehaviour
    {
        [SerializeField] private StarterAssets.FirstPersonController controller;

        private void OnTriggerEnter(Collider other)
        {
            MainUI.Instance.RollCredits();
            controller.TransitionToIdleAnimation();
        }
    }
}
