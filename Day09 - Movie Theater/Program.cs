// Day 09 - Movie Theater
using Helpers;
using System.Diagnostics;

using TPos = (long Row, long Col);
using TEdge = ((long Row, long Col) First, (long Row, long Col) Second);  // Direction matters!

Console.OutputEncoding = System.Text.Encoding.UTF8;
Stopwatch stopwatch = new();
PrintHelper.ПечатиНаслов(9, "Movie Theater");

string fileName = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName, "input.txt");





// Read the input
List<TPos> input = [.. File.ReadAllLines(fileName)
  .Select(line => line.Split(','))
  .Select(p => (Int64.Parse(p[0]), Int64.Parse(p[1])))
];





stopwatch.Start();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 1
long nMaxArea = 0;
for (int i = 0; i < input.Count - 1; ++i)
  for (int j = i + 1; j < input.Count; ++j) {
    long nArea = (1L + Math.Abs(input[i].Row - input[j].Row)) * (1L + Math.Abs(input[i].Col - input[j].Col));
    if (nArea > nMaxArea)
      nMaxArea = nArea;
  }
// Part 1
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиПрвДел(stopwatch.ElapsedMilliseconds, nMaxArea);





stopwatch.Restart();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 2
input = [.. input.OrderBy(p => p.Row).ThenBy(p => p.Col)];
bool[] used = new bool[input.Count];
List<TEdge> contour = [(input[0], input[1])];
int nLastVertex = 1;  // input[1]
bool bPreviousVertical = false;

// Build a contour to determine all of the edges
bool bNoMore = false;
while (!bNoMore) {
  bNoMore = true;
  for (int nDelta = 1; nLastVertex + nDelta < input.Count || nLastVertex - nDelta >= 0; ++nDelta) {
    int nNextRight = nLastVertex + nDelta;
    if (nNextRight < input.Count
      && !used[nNextRight]
      && ((bPreviousVertical && input[nLastVertex].Row == input[nNextRight].Row)
        || (!bPreviousVertical && input[nLastVertex].Col == input[nNextRight].Col))) {
      used[nNextRight] = true;
      contour.Add((input[nLastVertex], input[nNextRight]));
      nLastVertex = nNextRight;
      bNoMore = false;
      break;
    }

    int nNextLeft = nLastVertex - nDelta;
    if (nNextLeft >= 0
      && !used[nNextLeft]
      && ((bPreviousVertical && input[nLastVertex].Row == input[nNextLeft].Row)
        || (!bPreviousVertical && input[nLastVertex].Col == input[nNextLeft].Col))) {
      used[nNextLeft] = true;
      contour.Add((input[nLastVertex], input[nNextLeft]));
      nLastVertex = nNextLeft;
      bNoMore = false;
      break;
    }
  }
  bPreviousVertical = !bPreviousVertical;
}

List<TEdge> verticals = [.. contour.Where(edge => edge.First.Col == edge.Second.Col).OrderBy(edge => edge.First.Col)];
List<long> allRows = [.. input.Select(pos => pos.Row).Distinct().Order()];
List<long> columns = [.. verticals.Select(e => e.First.Col)];

nMaxArea = 0;

for (int i = 0; i < input.Count - 1; ++i)
  for (int j = i + 1; j < input.Count; ++j) {
    TPos v1 = input[i];
    TPos v2 = input[j];
    TPos vMin = (Math.Min(v1.Row, v2.Row), Math.Min(v1.Col, v2.Col));
    TPos vMax = (Math.Max(v1.Row, v2.Row), Math.Max(v1.Col, v2.Col));

    // Reduce allRows into rowsToCheck
    int startIdx = allRows.BinarySearch(vMin.Row);
    if (startIdx < 0) startIdx = ~startIdx;
    int endIdx = allRows.BinarySearch(vMax.Row);
    if (endIdx < 0) endIdx = ~endIdx - 1;
    List<long> rowsToCheck = allRows.GetRange(startIdx, endIdx - startIdx + 1);

    // Reduce verticals into verticalsToCheck
    startIdx = columns.BinarySearch(vMin.Col);
    if (startIdx < 0) startIdx = ~startIdx;
    endIdx = columns.BinarySearch(vMax.Col);
    if (endIdx < 0) endIdx = ~endIdx - 1;
    List<TEdge> verticalsToCheck = verticals.GetRange(startIdx, endIdx - startIdx + 1);

    bool bOutside = false;
    foreach (long nRow in rowsToCheck) {
      foreach (TEdge edge in verticalsToCheck) {
        bOutside = ((edge.First.Row < edge.Second.Row) && edge.First.Row <= nRow && nRow < edge.Second.Row && vMin.Col <= edge.First.Col && edge.First.Col < vMax.Col)
          || ((edge.First.Row > edge.Second.Row) && edge.Second.Row < nRow && nRow <= edge.First.Row && vMin.Col < edge.First.Col && edge.First.Col <= vMax.Col);
        if (bOutside) break;
      }
      if (bOutside) break;
    }

    if (!bOutside)
      nMaxArea = Math.Max(nMaxArea, (1L + vMax.Row - vMin.Row) * (1L + vMax.Col - vMin.Col));
  }
// Part 2
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиВторДел(stopwatch.ElapsedMilliseconds, nMaxArea);





PrintHelper.ПечатиЕлкаЗаКрај();
