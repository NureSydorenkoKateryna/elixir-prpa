using word_counter_net;

const string filePath = @"YOUR_PATH";
string wordToFind = "now";
int numThreads = Environment.ProcessorCount - 1;
int iterations = 7;

byte[] fileBytes = File.ReadAllBytes(filePath);
var wordsCounter = new WordCounter();

// Tests
//var aboutOccurrences = wordsCounter.CountWordOccurrencesInFileThreads(fileBytes, "about", numThreads);
//Console.WriteLine($"Word 'about' occurrences: {aboutOccurrences}");
//Console.WriteLine($"Word 'now' occurrences: {wordsCounter.CountWordOccurrencesInFileParallel(fileBytes, "now", numThreads)}");

try
{

    Console.WriteLine($"Word: '{wordToFind}'");

    Console.WriteLine("-------------------------------------------------- Threads: ");
    (int occurrences, TimeSpan minTime) = TimeMeasurementsHelper.MeasureExecutionTimeTimeSpan(() =>
        wordsCounter.CountWordOccurrencesInFileThreads(fileBytes, wordToFind, numThreads), iterations);

    Console.WriteLine($"Occurrences: {occurrences}");
    Console.WriteLine($"Fastest Time: {minTime.TotalSeconds} seconds");
    //Console.WriteLine($"Fastest Time: {minTime.TotalMicroseconds} microseconds");

    Console.WriteLine("-------------------------------------------------- Parallel: ");
    (occurrences, TimeSpan minTimeTS) = TimeMeasurementsHelper.MeasureExecutionTimeTimeSpan(() =>
       wordsCounter.CountWordOccurrencesInFileParallel(fileBytes, wordToFind, numThreads), iterations);

    Console.WriteLine($"Occurrences: {occurrences}");
    //Console.WriteLine($"Fastest Time: {minTimeTS.TotalMicroseconds} microseconds");
    Console.WriteLine($"Fastest Time: {minTimeTS.TotalSeconds} seconds");


}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}


