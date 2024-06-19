using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class GameManager : MonoBehaviour, INotificationReceiver
{
    // Handle notifications from all timelines
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is SignalEmitter signalEmitter)
        {
            var message = signalEmitter.asset.name;
            Debug.Log("OnNotify " + message);
        }
    }

}
