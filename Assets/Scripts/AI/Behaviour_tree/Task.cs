using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AI.Behaviour_tree
{
   abstract class Task
   {
       #region class variables

       public List<Task> Children { get; set; } //hold a list of children if applicable

       /// <summary>
       /// always terminate with either true or false
       /// </summary>
       /// <returns>bool</returns>
       public abstract bool Run();

       #endregion


   }
}
