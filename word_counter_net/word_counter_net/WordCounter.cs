using System.Text;
using System.Text.RegularExpressions;

namespace word_counter_net;

public class WordCounter
{
    public int CountWordOccurrencesInFileThreads(byte[] fileBytes, string wordToFind, int numThreads)
    {
        int fileSize = fileBytes.Length;
        int chunkSize = fileSize / numThreads;
        int numChunks = numThreads;

        var results = new int[numChunks];

        Thread[] threads = new Thread[numChunks];

        for (int chunkIndex = 0; chunkIndex < numChunks; chunkIndex++)
        {
            int index = chunkIndex; 
            threads[index] = new Thread(() =>
            {
                int start = index * chunkSize;
                int length = Math.Min(chunkSize, fileSize - start);

                results[index] = CountWordOccurrencesInChunk(fileBytes, start, length, wordToFind);
            });

            threads[index].Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        return results.Sum();
    }

    public int CountWordOccurrencesInFileParallel(byte[] fileBytes, string wordToFind, int numThreads)
    {
        int fileSize = fileBytes.Length;
        var chunkSize = (int)Math.Ceiling((double)fileSize / numThreads);
        int numChunks = numThreads;

        var results = new int[numThreads];

        Parallel.For(0, numChunks, new ParallelOptions { MaxDegreeOfParallelism = numThreads }, chunkIndex =>
        {
            var start = chunkIndex * chunkSize;
            var length = Math.Min(chunkSize, fileSize - start);
            results[chunkIndex] = CountWordOccurrencesInChunk(fileBytes, start, length, wordToFind);
        });

        return results.Sum();
    }

    private int CountWordOccurrencesInChunk(byte[] fileBytes, int start, int length, string wordToFind)
    {
        string chunkText = Encoding.UTF8.GetString(fileBytes, start, length);

        Regex wordRegex = new Regex($@"\b{Regex.Escape(wordToFind)}\b", RegexOptions.IgnoreCase);
        return wordRegex.Matches(chunkText).Count;
    }
}
