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
            // make a temporary file so we don't touch the real one
            string tempFile = Path.Combine(Path.GetTempPath(), $"deepthought-{Guid.NewGuid()}.json");

            // change the private _fileName inside JobStore to point to our temp file
            typeof(JobStore)
                .GetField("_fileName", BindingFlags.NonPublic | BindingFlags.Static)!
                .SetValue(null, tempFile);

            try
            {
                // start clean
                JobStore.Jobs.Clear();

                // create a fake job
                var jobId = Guid.NewGuid().ToString();
                var job = new Job(jobId, "What is life?", "Trivial", "Pending", 0);

                // save it to disk
                JobStore.UpdateJobsToDisk(job);

                // clear memory to act like we restarted the app
                JobStore.Jobs.Clear();

                // load it again from disk
                JobStore.Load();

                // make sure it was saved and loaded correctly
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
                // delete temp file when done
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }
    }
}
