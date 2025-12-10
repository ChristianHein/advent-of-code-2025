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
        public List<short> JoltageTargets;
        public SortedDictionary<byte, List<byte>> ButtonIdxToJoltages;
        public Dictionary<byte, List<byte>> JoltageIdxToButtons;

        public override string ToString()
        {
            return "[" + new string(LightTargets.Select(b => b ? '#' : '.').ToArray()) + "] " +
                   string.Join(' ',
                       ButtonIdxToLights.Values.Select(lights => "(" + string.Join(',', lights) + ")")) + ' ' +
                   "{" + string.Join(',', JoltageTargets) + "}";
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
        var lightIdxToButtons = new Dictionary<byte, List<byte>>();

        var joltageRequirements = parts[^1].Trim('{', '}').Split(',').Select(short.Parse).ToList();
        var joltageIdxToButtons = new Dictionary<byte, List<byte>>();

        var buttonIdxToLightOrJoltage = new SortedDictionary<byte, List<byte>>();

        for (var i = 1; i < parts.Length - 1; i++)
        {
            var buttonIdx = (byte)(i - 1);

            var controlledIds = parts[i].Trim('(', ')').Split(',').Select(byte.Parse).ToList();
            buttonIdxToLightOrJoltage.Add(buttonIdx, controlledIds);

            foreach (var id in controlledIds)
            {
                if (lightIdxToButtons.TryGetValue(id, out var lightButtons))
                {
                    lightButtons.Add(buttonIdx);
                }
                else
                {
                    lightIdxToButtons.Add(id, [buttonIdx]);
                }

                if (joltageIdxToButtons.TryGetValue(id, out var joltageButtons))
                {
                    joltageButtons.Add(buttonIdx);
                }
                else
                {
                    joltageIdxToButtons.Add(id, [buttonIdx]);
                }
            }
        }

        return new Machine
        {
            LightTargets = lightTargets,
            ButtonIdxToLights = buttonIdxToLightOrJoltage,
            LightIdxToButtons = lightIdxToButtons,
            JoltageTargets = joltageRequirements,
            ButtonIdxToJoltages = buttonIdxToLightOrJoltage,
            JoltageIdxToButtons = joltageIdxToButtons
        };
    }

    public override string Part1Solution()
    {
        var machines = ParseInput();
        return machines.Select(FewestButtonPressesForLights).Sum().ToString();
    }

    public override string Part2Solution()
    {
        var machines = ParseInput();
        return machines.Select(FewestButtonPressesForJoltages).Sum().ToString();
    }

    private static int FewestButtonPressesForLights(Machine machine)
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

    private static long FewestButtonPressesForJoltages(Machine machine)
    {
        var ctx = new Context();
        var solver = ctx.MkOptimize();

        var buttonVars = machine.ButtonIdxToJoltages.Keys
            .ToDictionary(buttonIdx => "b_" + buttonIdx, buttonIdx => ctx.MkIntConst("b_" + buttonIdx));

        var buttonConstraints = new List<BoolExpr>();
        foreach (var buttonVar in buttonVars.Values)
        {
            buttonConstraints.Add(ctx.MkGe(buttonVar, ctx.MkInt(0)));
        }

        var joltageConstraints = new List<BoolExpr>();
        foreach (var joltageIdx in machine.JoltageIdxToButtons.Keys)
        {
            var buttons = machine.JoltageIdxToButtons[joltageIdx].Select(idx => buttonVars["b_" + idx]);
            joltageConstraints.Add(
                ctx.MkEq(ctx.MkAdd(buttons), ctx.MkInt(machine.JoltageTargets[joltageIdx])));
        }

        var buttonsSumVar = ctx.MkAdd(buttonVars.Values);

        solver.Assert(buttonConstraints);
        solver.Assert(joltageConstraints);
        solver.MkMinimize(buttonsSumVar);

        var status = solver.Check();
        if (status != Status.SATISFIABLE)
        {
            throw new InvalidOperationException();
        }

        return long.Parse(solver.Model.Eval(buttonsSumVar).ToString());
    }
}