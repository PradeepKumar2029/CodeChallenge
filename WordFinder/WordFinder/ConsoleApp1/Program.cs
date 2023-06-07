// See https://aka.ms/new-console-template for more information

using ConsoleApp1;
using System.Diagnostics;

List<string> list = new List<string>();
list.Add("TBCOLD");
list.Add("AGOIOO");
list.Add("COLDLG");
list.Add("PQDSDG");
list.Add("UVDXYY");
IEnumerable<string> matrix = list.ToList();

List<string> inputList = new List<string>();
inputList.Add("COLD");
inputList.Add("COLD");
inputList.Add("DOG");
inputList.Add("TACP");
inputList.Add("TADR");


IEnumerable<string> input = inputList.ToList();


WordFinder wordFinder = new WordFinder(matrix, 5, 6);

Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();
IEnumerable<string>? output = wordFinder.Find(input);
if (output != null)
{
    foreach (string word in output)
    {
        Console.WriteLine(word);
    }
}
stopwatch.Stop();
TimeSpan ts = stopwatch.Elapsed;
string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
ts.Minutes, ts.Seconds, ts.Milliseconds,
ts.Nanoseconds);
Console.WriteLine("Time elapsed: {0}", elapsedTime);
Console.ReadLine();
