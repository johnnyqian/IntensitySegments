using NUnit.Framework;

namespace IntensitySegments.UnitTests
{
    public class Tests
    {
        [Test]
        public void FullSequence_ShouldProduceExpectedStrings()
        {
            var segments = new IntensitySegments();

            Assert.That(segments.ToString(), Is.EqualTo("[]"));

            segments.Add(10, 30, 1);
            Assert.That(segments.ToString(), Is.EqualTo("[[10,1],[30,0]]"));

            segments.Add(20, 40, 1);
            Assert.That(segments.ToString(), Is.EqualTo("[[10,1],[20,2],[30,1],[40,0]]"));

            segments.Add(10, 40, -2);
            Assert.That(segments.ToString(), Is.EqualTo("[[10,-1],[20,0],[30,-1],[40,0]]"));

            segments.Set(15, 35, 5);
            Assert.That(segments.ToString(), Is.EqualTo("[[10,-1],[15,5],[35,-1],[40,0]]"));

            segments.Add(20, 30, 3);
            Assert.That(segments.ToString(), Is.EqualTo("[[10,-1],[15,5],[20,8],[30,5],[35,-1],[40,0]]"));

            segments.Add(18, 20, 3);
            Assert.That(segments.ToString(), Is.EqualTo("[[10,-1],[15,5],[18,8],[30,5],[35,-1],[40,0]]"));

            segments.Set(25, 32, 5);
            Assert.That(segments.ToString(), Is.EqualTo("[[10,-1],[15,5],[18,8],[25,5],[35,-1],[40,0]]"));

            segments.Set(30, 32, 5);
            Assert.That(segments.ToString(), Is.EqualTo("[[10,-1],[15,5],[18,8],[25,5],[35,-1],[40,0]]"));

            segments.Add(-10, 12, 2);
            Assert.That(segments.ToString(), Is.EqualTo("[[-10,2],[10,1],[12,-1],[15,5],[18,8],[25,5],[35,-1],[40,0]]"));

            segments.Set(-20, 50, 2);
            Assert.That(segments.ToString(), Is.EqualTo("[[-20,2],[50,0]]"));
        }

        [Test]
        public void InvalidRange_ThrowsArgumentException()
        {
            var segments = new IntensitySegments();

            Assert.Throws<ArgumentException>(() => segments.Add(30, 10, 1));
            Assert.Throws<ArgumentException>(() => segments.Set(40, 20, 1));
        }
    }
}