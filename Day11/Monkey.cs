using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    internal class Monkey
    {
        public int Inspections { get; private set; } = 0;

        private Queue<long> items = new Queue<long>();

        private readonly Func<long, long> inspect;

        private readonly Predicate<long> throwPredicate;

        private readonly Action<Monkey[], long> throwActionTrue;
        private readonly Action<Monkey[], long> throwActionFalse;

        public Monkey(Func<long, long> inspect, Predicate<long> throwPredicate, Action<Monkey[], long> throwActionTrue, Action<Monkey[], long> throwActionFalse)
        {
            this.inspect = inspect;
            this.throwPredicate = throwPredicate;
            this.throwActionTrue = throwActionTrue;
            this.throwActionFalse = throwActionFalse;
        }

        public void DoTurn(Monkey[] monkeys, bool isPartTwo = false)
        {
            while (items.Count > 0)
            {
                long curr = items.Dequeue();

                curr = inspect(curr);
                Inspections++;
                if (! isPartTwo) curr /= 3;

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

        public void ReceiveItem(long item)
        {
            items.Enqueue(item);
        }

    }
}
