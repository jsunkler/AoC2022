using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    internal class Game
    {
        private readonly CircularList<(int xOffset, int yOffset)> movements;
        private readonly IEnumerator<(int xOffset, int yOffset)> movementEnumerator;

        private long hiddenLines = 0;

        private readonly CircularList<char> rocks = new()
        {
            '-', '+', 'l', 'i', 'o'
        };
        private readonly IEnumerator<char> rocksEnumerator;

        private HashSet<(long x, long y)> game = new();
        //private List<(long x, long y)> game = new();

        public Game(CircularList<(int xOffset, int yOffset)> movements)
        {
            this.movements = movements;
            movementEnumerator = movements.GetEnumerator();
            rocksEnumerator = rocks.GetEnumerator();
        }

        public void Play(long fallingRocks = 2022)
        {
            for (long i = 0; i < fallingRocks; i++)
            {
                if (i % 1000000 == 0) Console.WriteLine($"Iteration {i}");

                char currentRock = rocksEnumerator.Current;
                rocksEnumerator.MoveNext();

                List<(long x, long y)> r = newRock(currentRock);

                while (true) 
                {
                    var resultSide = moveRock(r);
                    if (resultSide.moved) r = resultSide.newPosition;


                    var resultDown = moveRock(r, false);
                    if (resultDown.moved) r = resultDown.newPosition;
                    else
                    {
                        r.ForEach(p => game.Add(p));
                        break;
                    }
                }
            }
        }

        private (bool moved, List<(long x, long y)> newPosition) moveRock(List<(long x, long y)> rock, bool side = true)
        {
            bool moved = false;

            if (side)
            {
                var movement = movementEnumerator.Current;
                movementEnumerator.MoveNext();

                bool canMoveToSide = rock.Select(p => (x: p.x + movement.xOffset, y: p.y + movement.yOffset))
                    .All(p => p.x > 0 && p.x < 8 && !game.Contains(p));

                if (canMoveToSide)
                {
                    rock = rock.Select(p => (x: p.x + movement.xOffset, y: p.y + movement.yOffset)).ToList();
                    moved = true;
                }
            }
            else
            {
                bool canMoveToBottom = rock.Select(p => (x: p.x, y: p.y - 1))
                    .All(p => p.x > 0 && p.x < 8 && p.y > 0 && !game.Contains(p));

                if (canMoveToBottom)
                {
                    rock = rock.Select(p => (x: p.x, y: p.y - 1)).ToList();
                    moved = true;
                }
            }

            return (moved, rock);
        }

        private List<(long x, long y)> newRock(char rock)
        {
            long startY = (game.Count > 0 ? game.Select(g => g.y).Max() : 0) + 4;

            if (rock == '-')
                return new List<(long x, long y)>
                {
                    (3, 0 + startY),
                    (4, 0 + startY),
                    (5, 0 + startY),
                    (6, 0 + startY)
                };
            if (rock == '+')
                return new List<(long x, long y)>
                {
                    (4, 0 + startY),
                    (3, 1 + startY),
                    (4, 1 + startY),
                    (5, 1 + startY),
                    (4, 2 + startY)
                };
            if (rock == 'l')
                return new List<(long x, long y)>
                {
                    (3, 0 + startY),
                    (4, 0 + startY),
                    (5, 0 + startY),
                    (5, 1 + startY),
                    (5, 2 + startY)
                };
            if (rock == 'i')
                return new List<(long x, long y)>
                {
                    (3, 0 + startY),
                    (3, 1 + startY),
                    (3, 2 + startY),
                    (3, 3 + startY)
                };
            if (rock == 'o')
                return new List<(long x, long y)>
                {
                    (3, 0 + startY),
                    (4, 0 + startY),
                    (3, 1 + startY),
                    (4, 1 + startY)
                };
            return new List<(long x, long y)>();
        }

        public long GetHeight()
        {
            return game.Select(p => p.y).Max();
        }
    }

}
