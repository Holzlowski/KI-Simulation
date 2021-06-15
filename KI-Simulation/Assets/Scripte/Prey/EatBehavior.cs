using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EatBehavior : StateMachineBehaviour
{
   HungerAllg hunger;
   Plant plant;
   public int value;

   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      hunger = animator.GetComponent<HungerAllg>();
      GameObject plantObject = animator.GetComponent<Prey>().target;
      plant = plantObject.GetComponent<Plant>();
      plant.losingCapacity(value);
   }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      
   }

   // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      hunger.eating(value);
   }
}
