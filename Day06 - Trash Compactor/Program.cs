// Day 06 - Trash Compactor
using Helpers;
using System.Diagnostics;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Stopwatch stopwatch = new();
PrintHelper.ПечатиНаслов(6, "Trash Compactor");

string fileName = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName, "input.txt");





stopwatch.Start();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 1

// Read the input (for Part 1)
List<List<long>> numbers = [];
List<char> operations = [];
foreach (string L in File.ReadLines(fileName)) {
  string line = L.Trim();
  if (Char.IsDigit(line[0]))
    numbers.Add([.. line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(Int64.Parse)]);
  else
    operations = [.. line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(Char.Parse)];
}

long nTotal = operations
  .Select((op, idx) => op == '+'
    ? numbers.Sum(line => line[idx])
    : numbers.Aggregate(1L, (acc, line) => acc * line[idx]))
  .Sum();

// Part 1
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиПрвДел(stopwatch.ElapsedMilliseconds, nTotal);





stopwatch.Restart();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 2

// Read the input (for Part 2) [cannot use Part1's input]
List<string> transposed = [.. File.ReadAllLines(fileName)];

nTotal = 0;
List<string> task = [];

for (int pos = transposed[0].Length - 1; pos >= 0; --pos) {
  string line = transposed.Aggregate(new StringBuilder(), (acc, val) => acc.Append(val[pos])).ToString().Trim();

  if (line.Length == 0) continue;

  char operation = line.Last();
  if (!Char.IsDigit(operation)) {
    task.Add(line[..^1]);

    nTotal += operation == '+'
      ? task.Sum(Int64.Parse)
      : task.Aggregate(1L, (acc, val) => acc * Int64.Parse(val));

    task = [];
  } else
    task.Add(line);
}

// Part 2
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиВторДел(stopwatch.ElapsedMilliseconds, nTotal);





PrintHelper.ПечатиЕлкаЗаКрај();
