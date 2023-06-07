using UnityEngine;

public class LifecycleNotifier : MonoBehaviour
{
    private readonly MonobehaviourUpdate monobehaviourUpdate;
    
    private void Update()
    {
        MessageSystem.SendMessage(monobehaviourUpdate);
    }
}