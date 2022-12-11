using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    internal class Game
    {
        public Monkey[] Monkeys { get; private set; }

        public Game()
        {
            initializeGame();
        }

        public void initializeGame()
        {
#if DEBUG
            Monkeys = new Monkey[]
            {
                new Monkey(
                        (i) => i * 19,
                        (i) => i % 23 == 0,
                        (m, i) => m[2].ReceiveItem(i),
                        (m, i) => m[3].ReceiveItem(i)
                    ),
                new Monkey(
                        (i) => i + 6,
                        (i) => i % 19 == 0,
                        (m, i) => m[2].ReceiveItem(i),
                        (m, i) => m[0].ReceiveItem(i)
                    ),
                new Monkey(
                        (i) => i * i,
                        (i) => i % 13 == 0,
                        (m, i) => m[1].ReceiveItem(i),
                        (m, i) => m[3].ReceiveItem(i)
                    ),
                new Monkey(
                        (i) => i + 3,
                        (i) => i % 17 == 0,
                        (m, i) => m[0].ReceiveItem(i),
                        (m, i) => m[1].ReceiveItem(i)
                    )
            };
            Monkeys[0].ReceiveItem(79);
            Monkeys[0].ReceiveItem(98);
            Monkeys[1].ReceiveItem(54);
            Monkeys[1].ReceiveItem(65);
            Monkeys[1].ReceiveItem(75);
            Monkeys[1].ReceiveItem(74);
            Monkeys[2].ReceiveItem(79);
            Monkeys[2].ReceiveItem(60);
            Monkeys[2].ReceiveItem(97);
            Monkeys[3].ReceiveItem(74);
#else
            Monkeys = new Monkey[]
            {
                new Monkey(
                        (i) => i * 13,
                        (i) => i % 7 == 0,
                        (m, i) => m[1].ReceiveItem(i),
                        (m, i) => m[5].ReceiveItem(i)
                    ),
                new Monkey(
                        (i) => i * i,
                        (i) => i % 3 == 0,
                        (m, i) => m[3].ReceiveItem(i),
                        (m, i) => m[5].ReceiveItem(i)
                    ),
                new Monkey(
                        (i) => i + 7,
                        (i) => i % 2 == 0,
                        (m, i) => m[0].ReceiveItem(i),
                        (m, i) => m[4].ReceiveItem(i)
                    ),
                new Monkey(
                        (i) => i + 4,
                        (i) => i % 11 == 0,
                        (m, i) => m[7].ReceiveItem(i),
                        (m, i) => m[6].ReceiveItem(i)
                    ),
                new Monkey(
                        (i) => i * 19,
                        (i) => i % 17 == 0,
                        (m, i) => m[1].ReceiveItem(i),
                        (m, i) => m[0].ReceiveItem(i)
                    ),
                new Monkey(
                        (i) => i + 3,
                        (i) => i % 5 == 0,
                        (m, i) => m[7].ReceiveItem(i),
                        (m, i) => m[3].ReceiveItem(i)
                    ),
                new Monkey(
                        (i) => i + 5,
                        (i) => i % 13 == 0,
                        (m, i) => m[4].ReceiveItem(i),
                        (m, i) => m[2].ReceiveItem(i)
                    ),
                new Monkey(
                        (i) => i + 1,
                        (i) => i % 19 == 0,
                        (m, i) => m[2].ReceiveItem(i),
                        (m, i) => m[6].ReceiveItem(i)
                    )
            };
            Monkeys[0].ReceiveItem(91);
            Monkeys[0].ReceiveItem(58);
            Monkeys[0].ReceiveItem(52);
            Monkeys[0].ReceiveItem(69);
            Monkeys[0].ReceiveItem(95);
            Monkeys[0].ReceiveItem(54);

            Monkeys[1].ReceiveItem(80);
            Monkeys[1].ReceiveItem(80);
            Monkeys[1].ReceiveItem(97);
            Monkeys[1].ReceiveItem(84);

            Monkeys[2].ReceiveItem(86);
            Monkeys[2].ReceiveItem(92);
            Monkeys[2].ReceiveItem(71);

            Monkeys[3].ReceiveItem(96);
            Monkeys[3].ReceiveItem(90);
            Monkeys[3].ReceiveItem(99);
            Monkeys[3].ReceiveItem(76);
            Monkeys[3].ReceiveItem(79);
            Monkeys[3].ReceiveItem(85);
            Monkeys[3].ReceiveItem(98);
            Monkeys[3].ReceiveItem(61);
            
            Monkeys[4].ReceiveItem(60);
            Monkeys[4].ReceiveItem(83);
            Monkeys[4].ReceiveItem(68);
            Monkeys[4].ReceiveItem(64);
            Monkeys[4].ReceiveItem(73);

            Monkeys[5].ReceiveItem(96);
            Monkeys[5].ReceiveItem(52);
            Monkeys[5].ReceiveItem(52);
            Monkeys[5].ReceiveItem(94);
            Monkeys[5].ReceiveItem(76);
            Monkeys[5].ReceiveItem(51);
            Monkeys[5].ReceiveItem(57);

            Monkeys[6].ReceiveItem(75);

            Monkeys[7].ReceiveItem(83);
            Monkeys[7].ReceiveItem(75);

#endif
        }

        public void DoRound(bool isPartTwo = false)
        {
            for (int i = 0; i < Monkeys.Length; i++)
            {
                Monkeys[i].DoTurn(Monkeys, isPartTwo);
            }
        }

    }
}
