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

        public double GetRangeLenght()
        {
            return To - From;
        }

        public bool IsInside(double number)
        {
            return number >= From && number <= To;
        }

        public Range GetCrossingRange(Range range)
        {
            if (From >= range.To || To <= range.From)
            {
                return null;
            }

            double from = From > range.From ? From : range.From;
            double to = To < range.To ? To : range.To;

            return new Range(from, to);
        }

        public Range[] GetUnionRanges(Range range)
        {
            if (From > range.To || To < range.From)
            {
                return new Range[] { this, range };
            }

            double from = From < range.From ? From : range.From;
            double to = To > range.To ? To : range.To;

            return new Range[] { new Range(from, to) };
        }

        public Range[] GetDifferenceRanges(Range range)
        {
            if (From >= range.To || To <= range.From)
            {
                return new Range[] { this, range };
            }

            if (From >= range.From && To <= range.To)
            {
                return new Range[0];
            }

            double from = From >= range.From ? range.To : From;
            double to = To <= range.To ? range.From : To;

            return new Range[] { new Range(from, to) };
        }
    }
}
