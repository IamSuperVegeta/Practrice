namespace PracticeTest;

using AtlasianPrep.RateLimiter;
using NUnit.Framework;

[TestFixture]
public class RateLimiterTests
{

    private RateLimtter? _rateLimiterTests;

    [SetUp]
    public void Setup()
    {
        _rateLimiterTests = new RateLimtter(TimeSpan.FromSeconds(10), 5);
    }

    [Test]
    public void IsUserAllowed_FirstRequest_ReturnsTrue()
    {
        var result = _rateLimiterTests.IsUserAllowed("123");
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsUserAllowed_Exceeds_ReturnsFalse()
    {
        for (int i = 0; i < 6; i++)
        {
            _rateLimiterTests.IsUserAllowed("123");
        }

        Assert.That(_rateLimiterTests.IsUserAllowed("123"), Is.False);
    }

    [Test]
    public void IsUserAllowed_AfterRefill_ReturnsTrue()
    {
        Assert.Pass();
    }

}