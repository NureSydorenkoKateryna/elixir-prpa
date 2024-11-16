using word_counter_net;

const string filePath = @"YOUR_PATH";
string wordToFind = "about";
int numThreads = Environment.ProcessorCount - 1;
int iterations = 7;


try
{
    byte[] fileBytes = File.ReadAllBytes(filePath);
    var wordsCounter = new WordCounter();

    Console.WriteLine($"Word: '{wordToFind}'");

    Console.WriteLine("-------------------------------------------------- Threads: ");
    (int occurrences, TimeSpan minTime) = TimeMeasurementsHelper.MeasureExecutionTimeTimeSpan(() =>
        wordsCounter.CountWordOccurrencesInFileThreads(fileBytes, wordToFind, numThreads), iterations);
   
    Console.WriteLine($"Occurrences: {occurrences}");
    Console.WriteLine($"Fastest Time: {minTime.TotalMicroseconds} microseconds");

    Console.WriteLine("-------------------------------------------------- Parallel: ");
    (occurrences, TimeSpan minTimeTS) = TimeMeasurementsHelper.MeasureExecutionTimeTimeSpan(() =>
       wordsCounter.CountWordOccurrencesInFileParallel(fileBytes, wordToFind, numThreads), iterations);

    Console.WriteLine($"Occurrences: {occurrences}");
    Console.WriteLine($"Fastest Time: {minTimeTS.TotalMicroseconds} microseconds");

}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}


