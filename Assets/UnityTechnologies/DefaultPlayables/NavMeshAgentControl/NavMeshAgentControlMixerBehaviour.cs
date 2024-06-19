using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.AI;

public class NavMeshAgentControlMixerBehaviour : PlayableBehaviour
{
    private NavMeshAgent m_NavMeshAgent; 
    
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        NavMeshAgent trackBinding = playerData as NavMeshAgent;
        if (!trackBinding)
            return;
        
        m_NavMeshAgent = trackBinding;
        int inputCount = playable.GetInputCount();

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<NavMeshAgentControlBehaviour> inputPlayable = (ScriptPlayable<NavMeshAgentControlBehaviour>)playable.GetInput(i);
            NavMeshAgentControlBehaviour input = inputPlayable.GetBehaviour();

            if (inputWeight > 0.5f && !input.destinationSet && input.destination)
            {
                if (!trackBinding.isOnNavMesh)
                    continue;

                trackBinding.SetDestination (input.destination.position);
                input.destinationSet = true;
            }
        }
    }

    public override void OnGraphStop(Playable playable)
    {
        base.OnGraphStop(playable);
    }
}
