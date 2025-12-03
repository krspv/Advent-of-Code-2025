// Day 02 - Gift Shop
using Helpers;
using System.Diagnostics;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Stopwatch stopwatch = new();
PrintHelper.ПечатиНаслов(3, "Lobby");

string fileName = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName, "input.txt");





// Read the input
List<string> input = [.. File.ReadAllLines(fileName)];




stopwatch.Start();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 1
int MaxJoltage(string bank) {
  char chFirst = bank[..^1].Max();
  char chSecond = bank[(1 + bank.IndexOf(chFirst))..].Max();

  return 10 * (chFirst - '0') + (chSecond - '0');
}
// Part 1
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиПрвДел(stopwatch.ElapsedMilliseconds, input.Sum(MaxJoltage));





stopwatch.Restart();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 2
long MaxJoltage2(string bank) {
  long nJoltage = 0;
  
  for (int nLastPos = 11; nLastPos >= 0; --nLastPos) {
    char chNext = bank[..^nLastPos].Max();
    bank = bank[(1 + bank.IndexOf(chNext))..];
    nJoltage = 10 * nJoltage + (chNext - '0');
  }

  return nJoltage;
}
// Part 2
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиВторДел(stopwatch.ElapsedMilliseconds, input.Sum(MaxJoltage2));





PrintHelper.ПечатиЕлкаЗаКрај();
