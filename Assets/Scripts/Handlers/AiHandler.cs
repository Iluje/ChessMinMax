using System.Collections;
using System.Collections.Generic;
using Handlers;
using UnityEngine;

public class AiHandler : MonoBehaviour
{
   public int Depth = 0;
   public bool IsTerminal;

   public void MiniMax(Node node,int depth, bool maximizing)
   {
      if (Depth == 0 || !IsTerminal)
      {
         
      }
   }
}
