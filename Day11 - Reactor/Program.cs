// Day 11 - Reactor
using Helpers;
using System.Diagnostics;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Stopwatch stopwatch = new();
PrintHelper.ПечатиНаслов(11, "Reactor");

string fileName = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName, "input.txt");





// Read the input
Dictionary<string, List<string>> directed = [];
foreach (string line in File.ReadAllLines(fileName)) {
  string[] keyTo = line.Split(':', StringSplitOptions.TrimEntries);
  directed[keyTo[0]] = [.. keyTo[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)];
}





stopwatch.Start();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 1
Queue<string> qNode= [];
qNode.Enqueue("you");
int nCount = 0;
while (qNode.Count > 0) {
  string cur = qNode.Dequeue();
  if (cur == "out")
    nCount++;
  else {
    if (directed.ContainsKey(cur)) {
      foreach (string next in directed[cur]) {
        qNode.Enqueue(next);
      }
    }
  }
}
// Part 1
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиПрвДел(stopwatch.ElapsedMilliseconds, nCount);





stopwatch.Restart();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 2
Dictionary<string, long> history = [];

long CountPaths(string cur, bool bVisitedFft = false, bool bVisitedDac = false) {
  if (cur == "out" && bVisitedFft && bVisitedDac) return 1L;
  string key = cur + (bVisitedDac ? '1' : '0') + (bVisitedFft ? '1' : '0');

  if (history.ContainsKey(key))
    return history[key];
  if (!directed.ContainsKey(cur))
    return 0;

  long nTotal = 0;
  foreach (string next in directed[cur])
    nTotal += CountPaths(next, bVisitedFft || next == "fft", bVisitedDac || next == "dac");
  history[key] = nTotal;

  return nTotal;
}
// Part 2
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиВторДел(stopwatch.ElapsedMilliseconds, CountPaths("svr"));





PrintHelper.ПечатиЕлкаЗаКрај();
