using System;
using UnityEngine;

namespace Handlers
{
   public class AiHandler : MonoBehaviour
   {
      public int Depth = 0;
      public bool IsTerminal;

      public int MinMax(Node node,int depth, bool maximizing)
      {
         if (depth == 0 || node.IsTerminal())
         {
            node.HeursticValue();
         }

         if (!maximizing)
         {
            int Value = int.MinValue;
            foreach (Node child in node.Children())
            {
               int max = MinMax(child, depth - 1, false);
            }
            return Value;
         }
         else
         {
            int Value = int.MaxValue;
            foreach (Node child in node.Children())
            {
               int min = MinMax(child, depth - 1, true);
            }
            return Value;
         }
      }
   }
}
