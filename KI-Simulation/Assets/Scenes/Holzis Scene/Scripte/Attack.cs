using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : FSMBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (prey != null)
        {
            agent.SetDestination(prey.transform.position);
<<<<<<< Updated upstream

            if (hunter.GetComponent<Hunter>().hit == true)
            {
                prey.GetComponent<HungerAllg>().getdamage(damage);
                Debug.Log("Ich hab was getroffen");
            }
=======
>>>>>>> Stashed changes
        }
        else
        {
            animator.SetBool("noTarget", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
