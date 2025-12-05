// Day 02 - Gift Shop
using Helpers;
using System.Diagnostics;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Stopwatch stopwatch = new();
PrintHelper.ПечатиНаслов(2, "Gift Shop");

string fileName = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName, "input.txt");





// Read the input
List<(long first, long last)> input = [.. File.ReadAllText(fileName).Split(',').Select(pair => pair.Split('-')).Select(arr => (Int64.Parse(arr[0]), Int64.Parse(arr[1])))];





stopwatch.Start();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 1
bool IsInvalid(long num) {
  string strNum = num.ToString();
  if (strNum.Length % 2 != 0) return false;
  return strNum.AsSpan(0, strNum.Length / 2).SequenceEqual(strNum.AsSpan(strNum.Length / 2));
}

long nSum = 0;
foreach (var (first, last) in input) {
  for (long val = first; val <= last; ++val) {
    if (IsInvalid(val))
      nSum += val;
  }
}
// Part 1
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиПрвДел(stopwatch.ElapsedMilliseconds, nSum);





stopwatch.Restart();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 2
bool IsInvalid2(string seq) {
  for (int splitter = 1; splitter <= seq.Length / 2; ++splitter) {
    if (seq.Length % splitter != 0) continue;

    int nNext = splitter;
    while (nNext <= seq.Length - splitter) {
      if (!seq.AsSpan(0, splitter).SequenceEqual(seq.AsSpan(nNext, splitter)))
        break;
      nNext += splitter;
    }

    if (nNext > seq.Length - splitter)
      return true;
  }

  return false;
}

nSum = 0;
foreach (var (first, last) in input) {
  for (long val = first; val <= last; ++val) {
    if (IsInvalid2(val.ToString()))
      nSum += val;
  }
}
// Part 2
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиВторДел(stopwatch.ElapsedMilliseconds, nSum);





PrintHelper.ПечатиЕлкаЗаКрај();
