using DeepThought.src.DeepThought.Strategies;
using Xunit;

public class SlowCountStrategyTests
{
    [Fact]
    public async Task SlowCountStrategy_AdvancesProgress_To100()
    {
        var strat = new SlowCountStrategy(TimeSpan.Zero);
        var reports = new List<int>();
        var progress = new Progress<int>(i => reports.Add(i));

        var result = await strat.AnswerQuestion(CancellationToken.None, progress);

        Assert.Contains(100, reports);
        Assert.True(reports.SequenceEqual(reports.OrderBy(x => x)));
        Assert.Equal("42", result);
    }
[Fact]
    public async Task SlowCountStrategy_HonorsCancellation_BeforeCompletion()
    {
        var strat = new SlowCountStrategy(TimeSpan.FromMilliseconds(50));
        var reports = new List<int>();
        var progress = new Progress<int>(i => reports.Add(i));
        using var cts = new CancellationTokenSource();
        var work = strat.AnswerQuestion(cts.Token, progress);
        cts.CancelAfter(120); // cancel before it reaches 100
   
        await Assert.ThrowsAsync<OperationCanceledException>(() => work);

        Assert.DoesNotContain(100, reports);

    }
}
