using Microsoft.Z3;

namespace AdventOfCode2025.Day10;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 10;

    private struct Machine
    {
        public List<bool> LightTargets;
        public SortedDictionary<byte, List<byte>> ButtonIdxToLights;
        public Dictionary<byte, List<byte>> LightIdxToButtons;
        public List<int> JoltageRequirements;

        public override string ToString()
        {
            return "[" + new string(LightTargets.Select(b => b ? '#' : '.').ToArray()) + "] " +
                   string.Join(' ',
                       ButtonIdxToLights.Values.Select(lights => "(" + string.Join(',', lights) + ")")) + ' ' +
                   "{" + string.Join(',', JoltageRequirements) + "}";
        }
    }

    private List<Machine> ParseInput()
    {
        return Input.Select(ParseMachine).ToList();
    }

    private static Machine ParseMachine(string machineStr)
    {
        var parts = machineStr.Split(' ');

        var lightTargets = parts[0].Trim('[', ']').Select(c => c == '#').ToList();
        var buttonIdxToLights = new SortedDictionary<byte, List<byte>>();
        var lightIdxToButtons = new Dictionary<byte, List<byte>>();
        var joltageRequirements = parts[^1].Trim('{', '}').Split(',').Select(int.Parse).ToList();

        for (var i = 1; i < parts.Length - 1; i++)
        {
            var buttonIdx = (byte)(i - 1);

            var lights = parts[i].Trim('(', ')').Split(',').Select(byte.Parse).ToList();
            buttonIdxToLights.Add(buttonIdx, lights);

            foreach (var light in lights)
            {
                if (lightIdxToButtons.TryGetValue(light, out var buttons))
                {
                    buttons.Add(buttonIdx);
                }
                else
                {
                    lightIdxToButtons.Add(light, [buttonIdx]);
                }
            }
        }

        return new Machine
        {
            LightTargets = lightTargets,
            ButtonIdxToLights = buttonIdxToLights,
            LightIdxToButtons = lightIdxToButtons,
            JoltageRequirements = joltageRequirements
        };
    }

    public override string Part1Solution()
    {
        var machines = ParseInput();
        foreach (var machine in machines)
        {
            Console.WriteLine(machine.ToString());
        }

        return machines.Select(FewestButtonPresses).Sum().ToString();
    }

    private static int FewestButtonPresses(Machine machine)
    {
        var ctx = new Context();
        var solver = ctx.MkOptimize();

        var buttonsVars = machine.ButtonIdxToLights.Keys
            .ToDictionary(buttonIdx => "b_" + buttonIdx, buttonIdx => ctx.MkBoolConst("b_" + buttonIdx));

        var lightConstraints = new List<BoolExpr>();
        foreach (var lightIdx in machine.LightIdxToButtons.Keys)
        {
            var buttons = machine.LightIdxToButtons[lightIdx].Select(idx => buttonsVars["b_" + idx]);
            lightConstraints.Add(ctx.MkEq(ctx.MkXor(buttons), ctx.MkBool(machine.LightTargets[lightIdx])));
        }

        var buttonPressesCount = buttonsVars.Values
            .Select(b => (ArithExpr)ctx.MkITE(b, ctx.MkInt(1), ctx.MkInt(0)))
            .ToList();
        var buttonsSumVar = ctx.MkAdd(buttonPressesCount);

        solver.Assert(lightConstraints);
        solver.MkMinimize(buttonsSumVar);

        var status = solver.Check();
        if (status != Status.SATISFIABLE)
        {
            throw new InvalidOperationException();
        }

        return int.Parse(solver.Model.Eval(buttonsSumVar).ToString());
    }

    public override string Part2Solution()
    {
        throw new NotImplementedException();
    }
}