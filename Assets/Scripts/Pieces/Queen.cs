using System.Collections.Generic;
using Handlers;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(fileName = "Queen", menuName = "Piece/Queen")]
    public class Queen : Piece
    {
        private Vector2Int _basDroite  = new Vector2Int(1,1);
        private Vector2Int _basGauche  = new Vector2Int(1,-1);
        private Vector2Int _hautdroite  = new Vector2Int(-1,1);
        private Vector2Int _hautGauche  = new Vector2Int(-1,-1);
        
        private Vector2Int _testDirection  = new Vector2Int(0,0);

        private bool _movementBasDroite = true;
        private bool _movementBasGauche = true;
        private bool _movementHautDroite = true;
        private bool _movementHautGauche = true;
        
        public override List<Vector2Int> AvailableMovements(Vector2Int position)
        {
            _movementBasDroite = true;
            _movementBasGauche = true;
            _movementHautDroite = true;
            _movementHautGauche = true;
            
            List<Vector2Int> movements = new List<Vector2Int>();

            int i;
            int j;
            
            for (i = position.x + 1; i <= 7; i++) 
            { 
                if (BoardsHandler.Instance.Pieces[i, position.y] == null)
                { 
                    movements.Add(new Vector2Int(i, position.y)); 
                    continue;
                } 
                if (BoardsHandler.Instance.Pieces[i, position.y].isWhite != isWhite)
                { 
                    movements.Add(new Vector2Int(i, position.y)); 
                    break;
                }
                else 
                { 
                    break;
                }
            } 
            for (i = position.x - 1; i >= 0; i--) 
            { 
                if (BoardsHandler.Instance.Pieces[i, position.y] == null) 
                { 
                    movements.Add(new Vector2Int(i, position.y)); 
                    continue;
                } 
                if (BoardsHandler.Instance.Pieces[i, position.y].isWhite != isWhite) 
                { 
                    movements.Add(new Vector2Int(i, position.y)); 
                    break;
                }
                else 
                { 
                    break;
                }
            }
            
            for (j = position.y + 1; j <= 7; j++) 
            { 
                if (BoardsHandler.Instance.Pieces[position.x, j] == null)
                { 
                    movements.Add(new Vector2Int(position.x, j)); 
                    continue;
                } 
                if (BoardsHandler.Instance.Pieces[position.x, j].isWhite != isWhite) 
                { 
                    movements.Add(new Vector2Int(position.x, j)); 
                    break;
                }
                else 
                { 
                    break;
                }
            } 
            for (j = position.y - 1; j >= 0; j--) 
            { 
                if (BoardsHandler.Instance.Pieces[position.x, j] == null) 
                { 
                    movements.Add(new Vector2Int(position.x, j)); 
                    continue;
                } 
                if (BoardsHandler.Instance.Pieces[position.x, j].isWhite != isWhite) 
                { 
                    movements.Add(new Vector2Int(position.x, j)); 
                    break;
                }
                else 
                { 
                    break;
                }
            }
            
            _testDirection = position;
            while (_movementBasDroite)
            {
                _testDirection += _basDroite;
                
                if (_testDirection.x < 0 || _testDirection.x > 7 || _testDirection.y < 0 || _testDirection.y > 7)
                {
                    _movementBasDroite = false;
                    break;
                }
                if (BoardsHandler.Instance.Pieces[_testDirection.x, _testDirection.y] == null)
                { 
                    movements.Add(new Vector2Int(_testDirection.x, _testDirection.y)); 
                    continue;
                } 
                if (BoardsHandler.Instance.Pieces[_testDirection.x, _testDirection.y].isWhite != isWhite)
                { 
                    movements.Add(new Vector2Int(_testDirection.x, _testDirection.y));
                    _movementBasDroite = false;
                }
                else
                {
                    _movementBasDroite = false;
                }
            }
            
            _testDirection = position;
            while (_movementBasGauche)
            {
                _testDirection += _basGauche;
                
                if (_testDirection.x < 0 || _testDirection.x > 7 || _testDirection.y < 0 || _testDirection.y > 7)
                {
                    _movementBasGauche = false;
                    break;
                }
                if (BoardsHandler.Instance.Pieces[_testDirection.x, _testDirection.y] == null)
                { 
                    movements.Add(new Vector2Int(_testDirection.x, _testDirection.y)); 
                    continue;
                } 
                if (BoardsHandler.Instance.Pieces[_testDirection.x, _testDirection.y].isWhite != isWhite)
                { 
                    movements.Add(new Vector2Int(_testDirection.x, _testDirection.y));
                    _movementBasGauche = false;
                }
                else
                {
                    _movementBasGauche = false;
                }
            }
            
            _testDirection = position;
            while (_movementHautDroite)
            {
                _testDirection += _hautdroite;
                
                if (_testDirection.x < 0 || _testDirection.x > 7 || _testDirection.y < 0 || _testDirection.y > 7)
                {
                    _movementHautDroite = false;
                    break;
                }
                if (BoardsHandler.Instance.Pieces[_testDirection.x, _testDirection.y] == null)
                { 
                    movements.Add(new Vector2Int(_testDirection.x, _testDirection.y)); 
                    continue;
                } 
                if (BoardsHandler.Instance.Pieces[_testDirection.x, _testDirection.y].isWhite != isWhite)
                { 
                    movements.Add(new Vector2Int(_testDirection.x, _testDirection.y));
                    _movementHautDroite = false;
                }
                else
                {
                    _movementHautDroite = false;
                }
            }
            
            _testDirection = position;
            while (_movementHautGauche)
            {
                _testDirection += _hautGauche;
                
                if (_testDirection.x < 0 || _testDirection.x > 7 || _testDirection.y < 0 || _testDirection.y > 7)
                {
                    _movementHautGauche = false;
                    break;
                }
                if (BoardsHandler.Instance.Pieces[_testDirection.x, _testDirection.y] == null)
                { 
                    movements.Add(new Vector2Int(_testDirection.x, _testDirection.y)); 
                    continue;
                } 
                if (BoardsHandler.Instance.Pieces[_testDirection.x, _testDirection.y].isWhite != isWhite)
                { 
                    movements.Add(new Vector2Int(_testDirection.x, _testDirection.y));
                    _movementHautGauche = false;
                }
                else
                {
                    _movementHautGauche = false;
                }
            }
            
            return movements;
        }
    }
}