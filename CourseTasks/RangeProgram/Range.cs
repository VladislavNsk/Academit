using System;

namespace RangeProgram
{
    class Range
    {
        public double From { get; set; }

        public double To { get; set; }

        public Range(double from, double to)
        {
            From = from;
            To = to;
        }

        public double GetLength()
        {
            return To - From;
        }

        public bool IsInside(double number)
        {
            return number >= From && number <= To;
        }

        public Range GetIntersection(Range range)
        {
            if (From >= range.To || To <= range.From)
            {
                return null;
            }

            double from = Math.Max(From, range.From);
            double to = Math.Min(To, range.To);

            return new Range(from, to);
        }

        public Range[] GetUnion(Range range)
        {
            if (From > range.To || To < range.From)
            {
                return new Range[]
                {
                    new Range(From, To),
                    new Range(range.From, range.To)
                };
            }

            double from = Math.Min(From, range.From);
            double to = Math.Max(To, range.To);

            return new Range[] { new Range(from, to) };
        }

        public Range[] GetDifference(Range range)
        {
            if (From >= range.To || To <= range.From)
            {
                return new Range[] { new Range(From, To) };
            }

            if (From >= range.From && To <= range.To)
            {
                return new Range[0];
            }

            if (From < range.From)
            {
                if (To > range.To)
                {
                    return new Range[]
                    {
                        new Range(From, range.From),
                        new Range(range.To, To)
                    };
                }

                return new Range[] { new Range(From, range.From) };
            }

            return new Range[] { new Range(range.To, To) };
        }
    }
}
