using DeepThought.src.DeepThought.Strategies;
using Xunit;

public class RandomGuessStrategyTests
{
[Fact] 
public async Task RandomGuessStrategy_ProducesNumberString()
    {
        var strat = new RandomGuessStrategy(TimeSpan.Zero);
        var reports = new List<int>();
        var progress = new Progress<int>(i => reports.Add(i));
        var result = await strat.AnswerQuestion(CancellationToken.None, progress);
        Assert.Equal("42", result);
    }
}