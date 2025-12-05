// Day 04 - Printing Department
using Helpers;
using System.Diagnostics;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Stopwatch stopwatch = new();
PrintHelper.ПечатиНаслов(4, "Printing Department");

string fileName = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName, "input.txt");





// Read the input
List<string> input = [.. File.ReadAllLines(fileName)];





stopwatch.Start();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 1
List<string> transformed = [
  new('.', input[0].Length + 2),
  ..input.Select(x => $".{x}."),
  new('.', input[0].Length + 2),
  ];

int[,] matrix = new int[transformed.Count, transformed.Count];  // For Part2
HashSet<(int, int)> removable = [];

for (int r = 1; r < transformed.Count - 1; ++r)
  for (int c = 1; c < transformed.Count - 1; ++c)
    if (transformed[r][c] == '@') {
      int nAdjacents = 0;
      for (int rr = r - 1; rr < r + 2; ++rr)
        for (int cc = c - 1; cc < c + 2; ++cc)
          if (transformed[rr][cc] == '@')
            nAdjacents++;
      if (nAdjacents < 5) {
        removable.Add((r, c));
      }

      matrix[r, c] = nAdjacents - 1;
    }
// Part 1
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиПрвДел(stopwatch.ElapsedMilliseconds, removable.Count);





stopwatch.Restart();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 2
int nTotal = 0;
while (removable.Count > 0) {
  HashSet<(int, int)> next = [];
  nTotal += removable.Count;
  foreach (var (R, C) in removable) {
    matrix[R, C] = -1;
    for (int r = R - 1; r < R + 2; ++r)
      for (int c = C - 1; c < C + 2; ++c)
        if (!removable.Contains((r, c)) && transformed[r][c] == '@' && matrix[r, c] != -1) {
          if (--matrix[r, c] < 4) {
            next.Add((r, c));
          }
        }
  }
  removable = next;
}
// Part 2
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиВторДел(stopwatch.ElapsedMilliseconds, nTotal);





PrintHelper.ПечатиЕлкаЗаКрај();
