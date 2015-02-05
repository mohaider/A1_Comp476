using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Scripts.AI.Character.MovementDelegator
{
    class KinematicDelagtorHeur:MonoBehaviour, MovementDelegator
    {
        MovementBehaviour movementBehaviour;
        public void HeuristicA1()
        {
           movementBehaviour.KinematicArrive();
        }

        public void HeuristicA2()
        {
           movementBehaviour.KinematicArrive();
            movementBehaviour.InterpolateRotate();
        }

        public void HeuristicB1()
        {
            movementBehaviour.KinematicArrive();
            movementBehaviour.InterpolateRotate();
     
           
        }

        public void HeuristicB2()
        {
            movementBehaviour.KinematicArrive();
            movementBehaviour.InterpolateRotate();
        }

        public void HeuristicC1()
        {
            throw new NotImplementedException();
        }

        public void HeuristicC2()
        {
            throw new NotImplementedException();
        }


        public void SetMovementBehaviour(MovementBehaviour movementBehaviour)
        {
            throw new NotImplementedException();
        }
    }
}
