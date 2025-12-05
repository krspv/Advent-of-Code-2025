// Day 05 - Cafeteria
using Helpers;
using System.Diagnostics;

using TRange = (long left, long right);

Console.OutputEncoding = System.Text.Encoding.UTF8;
Stopwatch stopwatch = new();
PrintHelper.ПечатиНаслов(5, "Cafeteria");

string fileName = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName, "input.txt");





// Read the input
List<TRange> ranges = [];
List<long> ingredients = [];

foreach (string line in File.ReadLines(fileName)) {
  if (String.IsNullOrEmpty(line.Trim())) continue;

  if (line.Contains('-')) {
    var range = line.Split('-').Select(Int64.Parse).ToArray();
    ranges.Add((range[0], range[1]));
  } else
    ingredients.Add(Int64.Parse(line));
}





stopwatch.Start();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 1
int nTotal = ingredients.Count(item => ranges.Any(range => range.left <= item && item <= range.right));
// Part 1
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиПрвДел(stopwatch.ElapsedMilliseconds, nTotal);





stopwatch.Restart();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 2
bool Overlap(TRange r1, TRange r2) => r1.left <= r2.right && r2.left <= r1.right;
TRange Merge(TRange r1, TRange r2) => (Math.Min(r1.left, r2.left), Math.Max(r1.right, r2.right));

bool[] used = new bool[ranges.Count];
for (int i = 0; i < ranges.Count - 1; ++i)
  for (int j = i + 1; j < ranges.Count; ++j)
    if (Overlap(ranges[i], ranges[j])) {
      used[i] = true;
      ranges[j] = Merge(ranges[i], ranges[j]);
      break;
    }

long nAll = ranges.Where((range, idx) => !used[idx]).Sum(range => 1 + range.right - range.left);
// Part 2
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиВторДел(stopwatch.ElapsedMilliseconds, nAll);





PrintHelper.ПечатиЕлкаЗаКрај();
