using System;
using System.IO;
using System.Reflection;
using DeepThought.src.DeepThought.Domain;
using DeepThought.src.DeepThought.Services;
using Xunit;

namespace DeepThought.Tests
{
    public class StoreTests
    {
        [Fact]
        public void JobStore_SavesAndLoads_JobsRoundTrip()
        {
            string tempFile = Path.Combine(Path.GetTempPath(), $"deepthought-{Guid.NewGuid()}.json");
            typeof(JobStore)
                .GetField("_fileName", BindingFlags.NonPublic | BindingFlags.Static)!
                .SetValue(null, tempFile);

            try
            {

                JobStore.Jobs.Clear();

                var jobId = Guid.NewGuid().ToString();
                var job = new Job(jobId, "What is life?", "Trivial", "Pending", 0);

                JobStore.UpdateJobsToDisk(job);
                JobStore.Jobs.Clear();

                JobStore.Load();


                Assert.True(JobStore.Jobs.ContainsKey(jobId));
                var loadedJob = JobStore.Jobs[jobId];
                Assert.Equal(jobId, loadedJob.JobId);
                Assert.Equal("What is life?", loadedJob.QuestionText);
                Assert.Equal("Trivial", loadedJob.AlgorithmKey);
                Assert.Equal("Pending", loadedJob.Status);
                Assert.Equal(0, loadedJob.Progress);
            }
            finally
            {
     
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }
    }
}
