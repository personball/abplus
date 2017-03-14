using System;
using Abp.TimeRanges;
using Shouldly;
using Xunit;

namespace Abplus.Tests.TimeRanges
{
    public class TimeRange_Tests
    {
        [Fact]
        public void 时间区间起点不应大于等于终点()
        {
            Should.Throw<ArgumentException>(() =>
            {
                var from = DateTime.Now;
                var to = DateTime.Now.AddMinutes(-5);
                var tr = new TimeRange(from, to);
            });
        }

        [Fact]
        public void 时间区间相交情况A()
        {
            var from = DateTime.Now;
            var to = from.AddMinutes(5);
            var trA = new TimeRange(from, to);

            var trB = new TimeRange(to, to.AddMinutes(5));

            var res = trA.IsIntersect(trB);
            res.ShouldBe(true);

            var trC = new TimeRange(from.AddMinutes(4), to.AddMinutes(5));
            var res2 = trA.IsIntersect(trC);
            res2.ShouldBe(true);

            var trD = new TimeRange(to.AddMinutes(1), to.AddMinutes(5));
            var res3 = trA.IsIntersect(trD);
            res3.ShouldBe(false);
        }

        [Fact]
        public void 时间区间相交情况B()
        {
            var from = DateTime.Now;
            var to = from.AddMinutes(5);
            var trA = new TimeRange(from, to);

            var trB = new TimeRange(from, to);

            var res = trA.IsIntersect(trB);
            res.ShouldBe(true);

            var trC = new TimeRange(from.AddMinutes(-1), to.AddMinutes(1));
            var res2 = trA.IsIntersect(trC);
            res2.ShouldBe(true);
        }

        [Fact]
        public void 时间区间相交情况C()
        {
            var from = DateTime.Now;
            var to = from.AddMinutes(5);
            var trA = new TimeRange(from, to);

            var trB = new TimeRange(from.AddMinutes(-5), from);

            var res = trA.IsIntersect(trB);
            res.ShouldBe(true);

            var trC = new TimeRange(from.AddMinutes(-5), from.AddMinutes(1));
            var res2 = trA.IsIntersect(trC);
            res2.ShouldBe(true);

            var trD = new TimeRange(from.AddMinutes(-5), from.AddMinutes(-1));
            var res3 = trA.IsIntersect(trD);
            res3.ShouldBe(false);
        }
    }
}
