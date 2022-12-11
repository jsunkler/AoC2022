using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    internal class Monkey
    {
        public long Inspections { get; private set; } = 0;

        private Queue<BigInteger> items = new Queue<BigInteger>();

        private readonly Func<BigInteger, BigInteger> inspect;

        private readonly Predicate<BigInteger> throwPredicate;

        private readonly Action<Monkey[], BigInteger> throwActionTrue;
        private readonly Action<Monkey[], BigInteger> throwActionFalse;

        public Monkey(Func<BigInteger, BigInteger> inspect, Predicate<BigInteger> throwPredicate, Action<Monkey[], BigInteger> throwActionTrue, Action<Monkey[], BigInteger> throwActionFalse)
        {
            this.inspect = inspect;
            this.throwPredicate = throwPredicate;
            this.throwActionTrue = throwActionTrue;
            this.throwActionFalse = throwActionFalse;
        }

        public void DoTurn(Monkey[] monkeys, bool isPartTwo = false, long modulus = 1)
        {
            while (items.Count > 0)
            {
                BigInteger curr = items.Dequeue();

                curr = inspect(curr);
                Inspections++;
                if (! isPartTwo) curr /= 3;
                curr %= modulus;

                if (throwPredicate(curr))
                {
                    throwActionTrue(monkeys, curr);
                }
                else
                {
                    throwActionFalse(monkeys, curr);
                }
            }
        }

        public void ReceiveItem(BigInteger item)
        {
            items.Enqueue(item);
        }

    }
}
