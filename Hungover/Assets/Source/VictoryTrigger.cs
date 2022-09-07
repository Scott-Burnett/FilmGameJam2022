using UnityEngine;

namespace Hungover
{
    public class VictoryTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            MainUI.Instance.RollCredits();
        }
    }
}
