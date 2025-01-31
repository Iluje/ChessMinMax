using UnityEngine;

namespace Handlers
{
   public class AiHandler : MonoBehaviour
   {
      public int MinMax(Node node,int depth, bool maximizing)
      {
         if (depth == 0 || node.IsTerminal())
         {
            return node.HeursticValue();
         }
         
         if (!maximizing)
         {
            int value = int.MaxValue;
            foreach (Node child in node.Children())
            {
               int max = MinMax(child, depth - 1, false);
               value = Mathf.Min(max, value);
            }
            return value;
         }
         else
         {
            int value = int.MinValue;
            foreach (Node child in node.Children())
            {
               int min = MinMax(child, depth - 1, true);
               value = Mathf.Max(min, value);
            }
            return value;
         }
         return int.MinValue;
      }
   }
}
