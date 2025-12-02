using AdventOfCode2025.Day02;

namespace AdventOfCode2025.Tests.Day02;

public class IdValidityCheckerTest
{
    [Test]
    public void SimpleIsValidTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That(IdValidityChecker.SimpleIsValid(0), Is.True);
            Assert.That(IdValidityChecker.SimpleIsValid(1), Is.True);
            Assert.That(IdValidityChecker.SimpleIsValid(2), Is.True);
            Assert.That(IdValidityChecker.SimpleIsValid(3), Is.True);
            Assert.That(IdValidityChecker.SimpleIsValid(4), Is.True);
            Assert.That(IdValidityChecker.SimpleIsValid(5), Is.True);
            Assert.That(IdValidityChecker.SimpleIsValid(6), Is.True);
            Assert.That(IdValidityChecker.SimpleIsValid(7), Is.True);
            Assert.That(IdValidityChecker.SimpleIsValid(8), Is.True);
            Assert.That(IdValidityChecker.SimpleIsValid(9), Is.True);

            Assert.That(IdValidityChecker.SimpleIsValid(10), Is.True);
            Assert.That(IdValidityChecker.SimpleIsValid(100), Is.True);
            Assert.That(IdValidityChecker.SimpleIsValid(1000), Is.True);
            Assert.That(IdValidityChecker.SimpleIsValid(10000), Is.True);
            Assert.That(IdValidityChecker.SimpleIsValid(100000), Is.True);

            Assert.That(IdValidityChecker.SimpleIsValid(11), Is.False);
            Assert.That(IdValidityChecker.SimpleIsValid(111), Is.True);
            Assert.That(IdValidityChecker.SimpleIsValid(1111), Is.False);
            Assert.That(IdValidityChecker.SimpleIsValid(11111), Is.True);
            Assert.That(IdValidityChecker.SimpleIsValid(111111), Is.False);

            Assert.That(IdValidityChecker.SimpleIsValid(22), Is.False);
            Assert.That(IdValidityChecker.SimpleIsValid(33), Is.False);
            Assert.That(IdValidityChecker.SimpleIsValid(44), Is.False);
            Assert.That(IdValidityChecker.SimpleIsValid(55), Is.False);
            Assert.That(IdValidityChecker.SimpleIsValid(66), Is.False);
            Assert.That(IdValidityChecker.SimpleIsValid(77), Is.False);
            Assert.That(IdValidityChecker.SimpleIsValid(88), Is.False);
            Assert.That(IdValidityChecker.SimpleIsValid(99), Is.False);

            Assert.That(IdValidityChecker.SimpleIsValid(1010), Is.False);
            Assert.That(IdValidityChecker.SimpleIsValid(1212), Is.False);
            Assert.That(IdValidityChecker.SimpleIsValid(9595), Is.False);
            Assert.That(IdValidityChecker.SimpleIsValid(959959), Is.False);
            Assert.That(IdValidityChecker.SimpleIsValid(12211221), Is.False);
        });
    }

    [Test]
    public void ComplexIsValidTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That(IdValidityChecker.ComplexIsValid(0), Is.True);
            Assert.That(IdValidityChecker.ComplexIsValid(1), Is.True);
            Assert.That(IdValidityChecker.ComplexIsValid(2), Is.True);
            Assert.That(IdValidityChecker.ComplexIsValid(3), Is.True);
            Assert.That(IdValidityChecker.ComplexIsValid(4), Is.True);
            Assert.That(IdValidityChecker.ComplexIsValid(5), Is.True);
            Assert.That(IdValidityChecker.ComplexIsValid(6), Is.True);
            Assert.That(IdValidityChecker.ComplexIsValid(7), Is.True);
            Assert.That(IdValidityChecker.ComplexIsValid(8), Is.True);
            Assert.That(IdValidityChecker.ComplexIsValid(9), Is.True);

            Assert.That(IdValidityChecker.ComplexIsValid(10), Is.True);
            Assert.That(IdValidityChecker.ComplexIsValid(100), Is.True);
            Assert.That(IdValidityChecker.ComplexIsValid(1000), Is.True);
            Assert.That(IdValidityChecker.ComplexIsValid(10000), Is.True);
            Assert.That(IdValidityChecker.ComplexIsValid(100000), Is.True);

            Assert.That(IdValidityChecker.ComplexIsValid(11), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(111), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(1111), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(11111), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(111111), Is.False);

            Assert.That(IdValidityChecker.ComplexIsValid(22), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(33), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(44), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(55), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(66), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(77), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(88), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(99), Is.False);

            Assert.That(IdValidityChecker.ComplexIsValid(1010), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(1212), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(9595), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(959959), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(12211221), Is.False);

            Assert.That(IdValidityChecker.ComplexIsValid(101010), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(121212), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(959595), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(959959959), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(122112211221), Is.False);

            Assert.That(IdValidityChecker.ComplexIsValid(10101010), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(12121212), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(95959595), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(959959959959), Is.False);
            Assert.That(IdValidityChecker.ComplexIsValid(1221122112211221), Is.False);
        });
    }
}