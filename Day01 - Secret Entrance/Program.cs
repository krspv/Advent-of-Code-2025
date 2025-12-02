// Day 01 - Secret Entrance
using Helpers;
using System.Diagnostics;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Stopwatch stopwatch = new();
PrintHelper.ПечатиНаслов(1, "Secret Entrance");

string fileName = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName, "input.txt");





// Read the input
List<int> numbers = [.. File.ReadAllLines(fileName).Select(line => Int32.Parse(line[1..]) * (line[0] == 'L' ? -1 : 1))];




stopwatch.Start();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 1
int nPos = 50, nCount = 0;
numbers.ForEach(number => {
  nPos += number;
  while (Math.Abs(nPos) > 99)
    nPos -= 100 * Math.Sign(nPos);
  if (nPos == 0)
    nCount++;
});
// Part 1
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиПрвДел(stopwatch.ElapsedMilliseconds, nCount);





stopwatch.Restart();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 2
nPos = 50;
nCount = 0;
numbers.ForEach(number => {
  int dir = number > 0 ? 1 : -1;
  for (int x = 0; x < Math.Abs(number); ++x) {
    nPos += dir;
    if (nPos == 100)
      nPos = 0;
    else if (nPos == -1)
      nPos = 99;
    if (nPos == 0)
      nCount++;
  }
});
// Part 2
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиВторДел(stopwatch.ElapsedMilliseconds, nCount);





PrintHelper.ПечатиЕлкаЗаКрај();
