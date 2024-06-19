using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class InitPlayer : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    public Transform movingTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        // Load and instantiate player
        var prefab = Resources.Load("Player");
        var playerGo = Instantiate(prefab) as GameObject;
        var player = playerGo.GetComponent<Player>();
        virtualCamera.Follow = player.virtualCameraFollowTarget;
        
        // Load and instantiate timeline
        prefab = Resources.Load("Timeline_PlayerMove_Nav");
        var movingPlayableDirectorGo = Instantiate(prefab) as GameObject;
        var movingPlayableDirector = movingPlayableDirectorGo.GetComponent<PlayableDirector>();

        // Rebinding timeline's tracks and play
        var timeline = movingPlayableDirector.playableAsset as TimelineAsset;
        foreach (var track in timeline.GetOutputTracks())
        {
            // Binding first track - animator
            if (track is AnimationTrack)
            {
                var animator = player.GetComponent<Animator>();
                if (animator != null)
                {
                    movingPlayableDirector.SetGenericBinding(track, animator);
                    Debug.Log($"Track {track.name} bound to {player.name}'s Animator.");
                }
            }
            
            // Binding the second track - navMeshAgent
            if (track is NavMeshAgentControlTrack navMeshAgentControlTrack)
            {
                movingPlayableDirector.SetReferenceValue("235eb14e5a418e34c95be19fa433e59e", movingTarget);
                
                var navMeshAgent = player.GetComponent<NavMeshAgent>();
                if (navMeshAgent != null)
                {
                    movingPlayableDirector.SetGenericBinding(track, navMeshAgent);
                    Debug.Log($"Track {track.name} bound to {player.name}'s NavMeshAgent.");
                }
            }
        }
        movingPlayableDirector.enabled = true;
        movingPlayableDirector.Play();
    }
}
