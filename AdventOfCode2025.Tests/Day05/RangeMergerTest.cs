using AdventOfCode2025.Day05;

namespace AdventOfCode2025.Tests.Day05;

using Range = RangeMerger.Range;

[TestFixture]
[TestOf(typeof(RangeMerger))]
public class RangeMergerTest
{
    [Test]
    public void MergeIntoEmptyList()
    {
        Assert.That(RangeMerger.Merge(
                [],
                new Range { Min = 0, Max = 100 }),
            Is.EquivalentTo(new List<Range> { new() { Min = 0, Max = 100 } }));
    }

    [Test]
    public void MergeIntoSingleRangeList()
    {
        Assert.Multiple(() =>
        {
            // Exact overlap
            Assert.That(RangeMerger.Merge(
                    [new Range { Min = 0, Max = 100 }],
                    new Range { Min = 0, Max = 100 }),
                Is.EquivalentTo(new List<Range> { new() { Min = 0, Max = 100 } }));

            // B inside A
            Assert.That(RangeMerger.Merge(
                    [new Range { Min = 0, Max = 100 }],
                    new Range { Min = 10, Max = 90 }),
                Is.EquivalentTo(new List<Range> { new() { Min = 0, Max = 100 } }));

            // A inside B
            Assert.That(RangeMerger.Merge(
                    [new Range { Min = 10, Max = 90 }],
                    new Range { Min = 0, Max = 100 }),
                Is.EquivalentTo(new List<Range> { new() { Min = 0, Max = 100 } }));

            // No overlap, A < B
            Assert.That(RangeMerger.Merge(
                    [new Range { Min = 0, Max = 100 }],
                    new Range { Min = 101, Max = 200 }),
                Is.EquivalentTo(new List<Range> { new() { Min = 0, Max = 100 }, new() { Min = 101, Max = 200 } }));

            // No overlap, A > B
            Assert.That(RangeMerger.Merge(
                    [new Range { Min = 101, Max = 200 }],
                    new Range { Min = 0, Max = 100 }),
                Is.EquivalentTo(new List<Range> { new() { Min = 0, Max = 100 }, new() { Min = 101, Max = 200 } }));
        });
    }

    [Test]
    public void MergeIntoMultipleRangesList()
    {
        Assert.Multiple(() =>
        {
            // No overlap of B with A1 or A2
            Assert.That(RangeMerger.Merge(
                    [new Range { Min = 0, Max = 100 }, new Range { Min = 200, Max = 300 }],
                    new Range { Min = 400, Max = 500 }),
                Is.EquivalentTo(new List<Range>
                    { new() { Min = 0, Max = 100 }, new() { Min = 200, Max = 300 }, new() { Min = 400, Max = 500 } }));

            // B inside A1
            Assert.That(RangeMerger.Merge(
                    [new Range { Min = 0, Max = 100 }, new Range { Min = 200, Max = 300 }],
                    new Range { Min = 10, Max = 90 }),
                Is.EquivalentTo(new List<Range> { new() { Min = 0, Max = 100 }, new() { Min = 200, Max = 300 } }));

            // B inside A2
            Assert.That(RangeMerger.Merge(
                    [new Range { Min = 0, Max = 100 }, new Range { Min = 200, Max = 300 }],
                    new Range { Min = 210, Max = 290 }),
                Is.EquivalentTo(new List<Range> { new() { Min = 0, Max = 100 }, new() { Min = 200, Max = 300 } }));

            // B overlaps only with left of A1
            Assert.That(RangeMerger.Merge(
                    [new Range { Min = 0, Max = 100 }, new Range { Min = 200, Max = 300 }],
                    new Range { Min = -50, Max = 50 }),
                Is.EquivalentTo(new List<Range> { new() { Min = -50, Max = 100 }, new() { Min = 200, Max = 300 } }));

            // B overlaps only with right of A1
            Assert.That(RangeMerger.Merge(
                    [new Range { Min = 0, Max = 100 }, new Range { Min = 200, Max = 300 }],
                    new Range { Min = 50, Max = 150 }),
                Is.EquivalentTo(new List<Range> { new() { Min = 0, Max = 150 }, new() { Min = 200, Max = 300 } }));

            // B overlaps only with left of A2
            Assert.That(RangeMerger.Merge(
                    [new Range { Min = 0, Max = 100 }, new Range { Min = 200, Max = 300 }],
                    new Range { Min = 150, Max = 250 }),
                Is.EquivalentTo(new List<Range> { new() { Min = 0, Max = 100 }, new() { Min = 150, Max = 300 } }));

            // B overlaps only with right of A2
            Assert.That(RangeMerger.Merge(
                    [new Range { Min = 0, Max = 100 }, new Range { Min = 200, Max = 300 }],
                    new Range { Min = 250, Max = 350 }),
                Is.EquivalentTo(new List<Range> { new() { Min = 0, Max = 100 }, new() { Min = 200, Max = 350 } }));

            // B overlaps with A1 and A2 (A1 < A2)
            Assert.That(RangeMerger.Merge(
                    [new Range { Min = 0, Max = 100 }, new Range { Min = 200, Max = 300 }],
                    new Range { Min = 50, Max = 250 }),
                Is.EquivalentTo(new List<Range> { new() { Min = 0, Max = 300 } }));

            // B overlaps with A1 and A2 (A1 > A2)
            Assert.That(RangeMerger.Merge(
                    [new Range { Min = 200, Max = 300 }, new Range { Min = 0, Max = 100 }],
                    new Range { Min = 50, Max = 250 }),
                Is.EquivalentTo(new List<Range> { new() { Min = 0, Max = 300 } }));
        });
    }
}