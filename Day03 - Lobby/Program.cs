// Day 03 - Lobby
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

int nSolution01 = input.Sum(MaxJoltage);
// Part 1
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиПрвДел(stopwatch.ElapsedMilliseconds, nSolution01);





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

long nSolution02 = input.Sum(MaxJoltage2);
// Part 2
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиВторДел(stopwatch.ElapsedMilliseconds, nSolution02);





PrintHelper.ПечатиЕлкаЗаКрај();
