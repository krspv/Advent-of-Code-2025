// Day 08 - Gift Shop
using Helpers;
using System.Diagnostics;

using TJunctionBox = (long X, long Y, long Z);
using TLink = (int idx1, int idx2, long dist);

Console.OutputEncoding = System.Text.Encoding.UTF8;
Stopwatch stopwatch = new();
PrintHelper.ПечатиНаслов(8, "Playground");

string fileName = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName, "input.txt");





// Read the input
List<TJunctionBox> boxes = [
  .. File.ReadAllLines(fileName)
  .Select(line => line.Split(','))
  .Select(p => (Int64.Parse(p[0]), Int64.Parse(p[1]), Int64.Parse(p[2])))
  ];




stopwatch.Start();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 1
static long DistSQ(TJunctionBox box1, TJunctionBox box2) {
  long dX = box1.X - box2.X;
  long dY = box1.Y - box2.Y;
  long dZ = box1.Z - box2.Z;
  return dX*dX + dY*dY + dZ*dZ;
}

Dictionary<(int, int), long> distances = [];
for (int i = 0; i < boxes.Count - 1; ++i)
  for (int j = i + 1; j < boxes.Count; ++j)
    distances[(i, j)] = DistSQ(boxes[i], boxes[j]);

List<TLink> links = [.. distances.Select(item => (item.Key.Item1, item.Key.Item2, item.Value)).OrderBy(link => link.Value)];

HashSet<HashSet<int>> circuits = [];

for (int nNext = 0; nNext < 1000; ++nNext) {
  TLink L = links[nNext];
  HashSet<int> c1 = circuits.FirstOrDefault(c => c.Contains(L.idx1));
  HashSet<int> c2 = circuits.FirstOrDefault(c => c.Contains(L.idx2));

  switch (c1, c2) {
    case (null, null):
      circuits.Add([L.idx1, L.idx2]);
      break;

    case (not null, null):
      c1.Add(L.idx2);
      break;

    case (null, not null):
      c2.Add(L.idx1);
      break;

    case (not null, not null) when c1 != c2:
      c1.UnionWith(c2);
      circuits.Remove(c2);
      break;
  }
}

long nTop3Multiple = circuits
    .Select(c => c.Count)
    .OrderByDescending(size => size)
    .Take(3)
    .Aggregate(1L, (acc, val) => acc * val);
// Part 1
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиПрвДел(stopwatch.ElapsedMilliseconds, nTop3Multiple);





stopwatch.Restart();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 2
bool[] connected = new bool[boxes.Count];
int nCountConnected = 0;
long nLast2Mul = 0;

foreach (TLink L in links) {
  if (!connected[L.idx1]) {
    connected[L.idx1] = true;
    nCountConnected++;
  }
  if (!connected[L.idx2]) {
    connected[L.idx2] = true;
    nCountConnected++;
  }
  if (nCountConnected == boxes.Count) {
    nLast2Mul = boxes[L.idx1].X * boxes[L.idx2].X;
    break;
  }
}
// Part 2
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиВторДел(stopwatch.ElapsedMilliseconds, nLast2Mul);





PrintHelper.ПечатиЕлкаЗаКрај();
