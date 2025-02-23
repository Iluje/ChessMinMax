using UnityEngine;

namespace Handlers
{
   public class AiHandler : MonoBehaviour
   {
      public int MinMax(Node node, int depth, bool maximizing)
      {
         if (depth == 0 || node.IsTerminal())
         {
            return node.HeursticValue();
         }
         if (maximizing)
         {
            int value = int.MinValue;
            foreach (Node child in node.Children())
            {
               int min = MinMax(child, depth - 1, false);
               value = Mathf.Max(min, value);
            }
            
            return value;
         }
         else
         {
            int value = int.MaxValue;
            foreach (Node child in node.Children())
            {
               int max = MinMax(child, depth - 1, true);
               value = Mathf.Min(max, value);
            }
            return value;
         }
      }

      public int AlphaBeta(Node node,int detph, int alpha, int beta, bool maximizing)
      {
         if (detph == 0 && node.IsTerminal())
         {
            return node.HeuristicValue;
         }

         if (maximizing)
         {
            int value = int.MaxValue;
            
            foreach (Node child in node.Children())
            {
               value = Mathf.Min(value, AlphaBeta(child, detph - 1, alpha, beta, false));
               
               if (alpha > value)
               {
                  break;
               }

               beta = Mathf.Min(beta, value);
            }
            
            return value;
         }
         else
         {
            int value = int.MinValue;
            
            foreach (Node child in node.Children())
            {
               value = Mathf.Max(value, AlphaBeta(child, detph - 1, alpha, beta, true));

               if (beta < value)
               {
                  break;
               }

               alpha = Mathf.Min(alpha, value);
            }

            return value;
         }
      }
   }
}
