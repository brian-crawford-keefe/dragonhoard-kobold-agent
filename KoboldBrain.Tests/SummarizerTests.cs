using KoboldBrain;
using Xunit;

namespace KoboldBrain.Tests
{
    public class SummarizerTests
    {
        [Fact]
        public void DetectsTodosAndFunctionCount()
        {
            var code = "// TODO: fix\npublic class C { public void M() { } }\n";
            var res = Summarizer.HeuristicSummary(code, "personality");
            Assert.True(res.Todos >= 1);
            Assert.True(res.FunctionCount >= 1);
        }

        [Fact]
        public void DetectsSyncOverAsync()
        {
            var code = "var x = task.Result;";
            var res = Summarizer.HeuristicSummary(code, "");
            Assert.Contains("sync-over-async", res.Intruders);
        }

        [Fact]
        public void EmptyCodeProducesSummary()
        {
            var res = Summarizer.HeuristicSummary("", "");
            Assert.Equal(0, res.Lines);
            Assert.Equal(0, res.FunctionCount);
        }
    }
}
