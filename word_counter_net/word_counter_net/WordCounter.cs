using System.Text;
using System.Text.RegularExpressions;

namespace word_counter_net;

public class WordCounter
{
    public int CountWordOccurrencesInFileThreads(byte[] file, string wordToFind, int numThreads)
    {
        // Read the file into memory as a string
        string fileContent = Encoding.UTF8.GetString(file);
        string[] lines = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        int numChunks = numThreads;
        int chunkSize = (int)Math.Ceiling((double)lines.Length / numThreads);
        var results = new int[numChunks];

        Thread[] threads = new Thread[numChunks];

        for (int chunkIndex = 0; chunkIndex < numChunks; chunkIndex++)
        {
            int index = chunkIndex;
            threads[index] = new Thread(() =>
            {
                int start = index * chunkSize;
                int end = Math.Min((index + 1) * chunkSize, lines.Length);

                results[index] = CountWordOccurrencesInLines(lines, start, end, wordToFind);
            });

            threads[index].Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        return results.Sum();
    }

    public int CountWordOccurrencesInFileParallel(byte[] file, string wordToFind, int numThreads)
    {
        string fileContent = Encoding.UTF8.GetString(file);
        string[] lines = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        int chunkSize = (int)Math.Ceiling((double)lines.Length / numThreads);
        int numChunks = numThreads;
        var results = new int[numChunks];

        Parallel.For(0, numChunks, new ParallelOptions { MaxDegreeOfParallelism = numThreads }, chunkIndex =>
        {
            int start = chunkIndex * chunkSize;
            int end = Math.Min((chunkIndex + 1) * chunkSize, lines.Length);

            results[chunkIndex] = CountWordOccurrencesInLines(lines, start, end, wordToFind);
        });

        return results.Sum();
    }

    private int CountWordOccurrencesInLines(string[] lines, int start, int end, string wordToFind)
    {
        string wordPattern = $@"\b{Regex.Escape(wordToFind)}\b";
        Regex wordRegex = new Regex(wordPattern, RegexOptions.IgnoreCase);

        int count = 0;
        for (int i = start; i < end; i++)
        {
            string line = lines[i];
            count += wordRegex.Matches(line).Count;
        }

        return count;
    }
}
