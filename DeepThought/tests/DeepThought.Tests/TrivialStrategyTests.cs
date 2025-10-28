using DeepThought.src.DeepThought.Strategies;
using Xunit;

public class TrivialStrategyTests
{
    [Fact] 
    public async Task TrivialStrategy_Return42_AndReportsProgress()
    {
        var strat = new TrivialStrategy();
        var progressValues = new List<int>();
        var progress = new Progress<int>(p => progressValues.Add(p));

        var result = await strat.AnswerQuestion(CancellationToken.None, progress);

        Assert.Equal("42", result);
        Assert.Contains(100, progressValues);
    }
}
