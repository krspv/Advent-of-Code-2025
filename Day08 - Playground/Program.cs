// Day 08 - Gift Shop
using Helpers;
using System.Diagnostics;

using TJunctionBox = (long X, long Y, long Z);

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

PriorityQueue<(int, int), long> pq = new();
for (int i = 0; i < boxes.Count - 1; ++i)
  for (int j = i + 1; j < boxes.Count; ++j)
    pq.Enqueue((i, j), DistSQ(boxes[i], boxes[j]));

HashSet<HashSet<int>> circuits = [];
HashSet<int> connected = [];  // For Part 2

for (int nNext = 0; nNext < 1000; ++nNext) {
  var (idx1, idx2) = pq.Dequeue();
  connected.UnionWith([idx1, idx2]);
  HashSet<int> c1 = circuits.FirstOrDefault(c => c.Contains(idx1));
  HashSet<int> c2 = circuits.FirstOrDefault(c => c.Contains(idx2));

  switch (c1, c2) {
    case (null, null):
      circuits.Add([idx1, idx2]);
      break;

    case (not null, null):
      c1.Add(idx2);
      break;

    case (null, not null):
      c2.Add(idx1);
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
long nLast2Mul = 0;
while (connected.Count < boxes.Count) {
  var (idx1, idx2) = pq.Dequeue();
  connected.UnionWith([idx1, idx2]);

  if (connected.Count == boxes.Count)
    nLast2Mul = boxes[idx1].X * boxes[idx2].X;
}
// Part 2
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиВторДел(stopwatch.ElapsedMilliseconds, nLast2Mul);





PrintHelper.ПечатиЕлкаЗаКрај();
