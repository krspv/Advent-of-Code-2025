// Day 10 - Factory
using Helpers;
using System.Diagnostics;
using Google.OrTools.LinearSolver;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Stopwatch stopwatch = new();
PrintHelper.ПечатиНаслов(10, "Factory");

string fileName = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName, "input.txt");





// Read the input
List<int> finalStates = [];
List<List<int>> buttons = [];
List<List<int[]>> buttons2 = [];  // For Part2
List<int[]> joltages = [];  // For part2
foreach (string line in File.ReadAllLines(fileName)) {
  string[] arr = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

  int nFinalState = 0;
  for (int i = 1; i < arr[0].Length - 1; ++i) {
    int bit = arr[0][i] == '#' ? 1 : 0;
    if (bit != 0)
      nFinalState |= bit << (i - 1);
  }

  buttons.Add([]);
  buttons2.Add([]);
  for (int i = 1; i < arr.Length - 1; ++i) {
    int[] bits = [.. arr[i][1..^1].Split(',').Select(Int32.Parse)];
    buttons2.Last().Add(bits);

    int btn = 0;
    foreach (int shift in bits)
      btn |= 1 << shift;
    buttons.Last().Add(btn);
  }
  finalStates.Add(nFinalState);

  joltages.Add([.. arr.Last()[1..^1].Split(',').Select(Int32.Parse)]);
}





stopwatch.Start();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 1
int Steps(int nEndState, List<int> btns) {
  if (nEndState == 0) return 0;

  Queue<(int, int)> q = [];
  q.Enqueue((0, 0));

  while (true) {
    var (nPrevState, nNumPresses) = q.Dequeue();
    foreach (int press in btns) {
      int nextState = nPrevState ^ press;
      if (nextState == nEndState)
        return nNumPresses + 1;
      q.Enqueue((nextState, nNumPresses + 1));
    }
  }
}

int nSum = finalStates.Select((val, idx) => Steps(val, buttons[idx])).Sum();
// Part 1
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиПрвДел(stopwatch.ElapsedMilliseconds, nSum);





stopwatch.Restart();
////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Part 2
int Solve(List<int[]> lstButtons, int[] final) {
  Solver solver = Solver.CreateSolver("SCIP");
  if (solver == null) throw new Exception("No solver available!");

  // Create integer variables
  Dictionary<string, Variable> vars = [];
  int nVarOrdinal = 0;
  foreach (int[] button in lstButtons) {
    // Find the max value for this variable
    int nMaxVal = Int32.MaxValue;
    for (int j = 0; j < final.Length; j++)
      if (button.Contains(j))
        nMaxVal = Math.Min(nMaxVal, final[j]);

    string varName = $"v{nVarOrdinal++}";
    vars[varName] = solver.MakeIntVar(0, nMaxVal, varName);
  }

  for (int i = 0; i < final.Length; ++i) {
    LinearExpr lhs = new();
    for (int btn = 0; btn < lstButtons.Count; ++btn) {
      if (lstButtons[btn].Contains(i))
        lhs += vars[$"v{btn}"];
    }
    solver.Add(lhs == final[i]);
  }

  LinearExpr objective = new();
  foreach (var v in vars.Values)
    objective += v;
  solver.Minimize(objective);

  var result = solver.Solve();
  return vars.Values.Select(var => (int)var.SolutionValue()).Sum();
}

nSum = 0;
for (int i = 0; i < joltages.Count; ++i) {
  nSum += Solve(buttons2[i], joltages[i]);
}
// Part 2
////////////////////////////////////////////////////////////////////////////////////////////////////////////
stopwatch.Stop();

PrintHelper.ПечатиВторДел(stopwatch.ElapsedMilliseconds, nSum);





PrintHelper.ПечатиЕлкаЗаКрај();
