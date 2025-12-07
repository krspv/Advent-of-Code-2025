// Day 07 - Laboratories
using Helpers;
using System.Diagnostics;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Stopwatch stopwatch = new();
PrintHelper.ПечатиНаслов(7, "Laboratories");

string fileName = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName, "input.txt");





// Read the input
List<string> input = [.. File.ReadAllLines(fileName)];





stopwatch.Start();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 1
HashSet<int> beamPos = [ input[0].IndexOf('S') ];
int nSplitCount = 0;

foreach (string line in input[1..]) {
  HashSet<int> next = [];

  foreach (int nCol in beamPos)
    if (line[nCol] == '^') {
      next.Add(nCol - 1);
      next.Add(nCol + 1);
      nSplitCount++;
    } else {
      next.Add(nCol);
    }

  beamPos = next;
}
// Part 1
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиПрвДел(stopwatch.ElapsedMilliseconds, nSplitCount);





stopwatch.Restart();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 2
Dictionary<int, long> timeBeam = [];
timeBeam.Add(input[0].IndexOf('S'), 1);

foreach (string line in input[1..]) {
  Dictionary<int, long> next = [];
  long nTemp;

  foreach (var (col, times) in timeBeam) {
    if (line[col] == '^') {
      next[col - 1] = next.TryGetValue(col - 1, out nTemp) ? nTemp + times : times;
      next[col + 1] = next.TryGetValue(col + 1, out nTemp) ? nTemp + times : times;
    } else {
      next[col] = next.TryGetValue(col, out nTemp) ? nTemp + times : times;
    }
  }

  timeBeam = next;
}

long nTimelines = timeBeam.Values.Sum();
// Part 2
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиВторДел(stopwatch.ElapsedMilliseconds, nTimelines);





PrintHelper.ПечатиЕлкаЗаКрај();
