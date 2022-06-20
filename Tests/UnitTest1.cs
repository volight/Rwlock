using NUnit.Framework;
using System.Threading.Tasks;
using Volight.LockWraps;
using System.Linq;
using System.Threading;

namespace Tests;
public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var rwl = new Rwlock();

        using (_ = rwl.Read())
        {

        }
        using (_ = rwl.Write())
        {

        }
    }

    [Test]
    public void Test2()
    {
        var rwl = new Rwlock<int>(0);

        using (var g = rwl.Read())
        {
            Assert.AreEqual(0, g.Value);
        }
        using (var g = rwl.Write())
        {
            g.Value = 1;
        }
    }
}