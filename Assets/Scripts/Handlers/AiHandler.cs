using UnityEngine;

namespace Handlers
{
   public class AiHandler : MonoBehaviour
   {
      public int MinMax(Node node,int depth, bool maximizing)
      {
         // si la deph est égal, ou que IsTerminal est true, alors on renvoie la HeursticValue
         if (depth == 0 || node.IsTerminal())
         {
            return node.HeursticValue();
         }
         
         // Si le joueur cherche à minimiser
         if (!maximizing)
         {
            
            // créer une variable qui serra égal à + l'infinie
            int value = int.MaxValue;
            
            // pour chaque Node enfant dans Children
            foreach (Node child in node.Children())
            {
               // Créer une variable int, qui serra égal au résultat de MinMax.
               int max = MinMax(child, depth - 1, false);
               
               // la value  va être égal à la valeur la plus petite entre, max et value.
               value = Mathf.Min(max, value);
            }
            
            // retourn la value la plus grande
            return value;
         }
         else
         {
            // créer une variable qui serra égal à - l'infinie
            int value = int.MinValue;
            
            // pour chaque Node enfant dans Children
            foreach (Node child in node.Children())
            {
               // Créer une variable int, qui serra égal au résultat de MinMax.
               int min = MinMax(child, depth - 1, true);
               
               // la value  va être égal à la valeur la plus grande entre, min et value.
               value = Mathf.Max(min, value);
            }
            
            // retourn la value la plus petite
            return value;
         }
      }
   }
}
