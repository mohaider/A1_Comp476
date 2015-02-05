using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AI.Character;

namespace Assets.Scripts.AI.Character.MovementDelegator

{
    interface MovementDelegator
    {
 
       void HeuristicA1();
        void HeuristicA2();
        void HeuristicB1();
        void HeuristicB2();
        void HeuristicC1();
        void HeuristicC2();

        void SetMovementBehaviour(MovementBehaviour movementBehaviour);




   }
}
