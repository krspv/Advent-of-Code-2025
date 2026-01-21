// Day 12 - Christmas Tree Farm
using Helpers;
using System.Diagnostics;

using TProblem = (int nArea, int nTotalDots);

Console.OutputEncoding = System.Text.Encoding.UTF8;
Stopwatch stopwatch = new();
PrintHelper.ПечатиНаслов(12, "Christmas Tree Farm");

string fileName = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName, "input.txt");





// Read the input
string[] lines = File.ReadAllLines(fileName);
List<int> dots = [];
for (int i = 0; i < 6; ++i) {
  int r = 5 * i + 1;
  int nDots = 0;
  for (int row = 0; row < 3; ++row)
    for (int col = 0; col < 3; ++col) {
      if (lines[r + row][col] == '#') nDots++;
    }
  dots.Add(nDots);
}
List<TProblem> problems = [];
for (int i = 30; i < lines.Length; ++i) {
  string line = lines[i];
  string[] ab = line.Split(':', StringSplitOptions.TrimEntries);
  string[] wh = ab[0].Split('x');
  string[] tms = ab[1].Split(' ', StringSplitOptions.TrimEntries);
  List<int> lst = tms.Select(Int32.Parse).ToList();


  problems.Add((Int32.Parse(wh[0]) * Int32.Parse(wh[1]), lst.Zip(dots, (a, b) => a * b).Sum()));
}




stopwatch.Start();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 1
int nCount = problems.Count(prob => prob.nArea > prob.nTotalDots);
// Part 1
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиПрвДел(stopwatch.ElapsedMilliseconds, nCount);





stopwatch.Restart();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 2
// Part 2
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиВторДел(stopwatch.ElapsedMilliseconds, "Нема");





PrintHelper.ПечатиЕлкаЗаКрај();
