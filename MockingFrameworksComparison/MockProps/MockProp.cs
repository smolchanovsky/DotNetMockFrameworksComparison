using FakeItEasy;
using Fakes;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace MockingFrameworksComparison.MockProps
{
	internal class MockProp : SetupMock
	{
		[Test]
		public void MockConcreteProp()
		{
			//Moq
			MoqDbContext.Setup(x => x.DbSet).Returns(new DbSet("Name"));

			//NSubstitute
			NsubDbContext.DbSet.Returns(new DbSet("Name"));

			//FakeItEasy
			A.CallTo(() => FieDbContext.DbSet).Returns(new DbSet("Name"));

			#region Assertion

			MoqDbContext.Object.DbSet.Should().BeEquivalentTo(new DbSet("Name"));

			NsubDbContext.DbSet.Should().BeEquivalentTo(new DbSet("Name"));

			FieDbContext.DbSet.Should().BeEquivalentTo(new DbSet("Name"));

			#endregion
		}

		[Test]
		public void MockPropRecursive()
		{
			//Moq
			MoqDbContext.Setup(x => x.DbSet.Name).Returns("Name");

			//NSubstitute
			NsubDbContext.DbSet.Name.Returns("Name");

			//FakeItEasy
			A.CallTo(() => FieDbContext.DbSet.Name).Returns("Name");

			#region Assertion

			MoqDbContext.Object.DbSet.Name.Should().BeEquivalentTo("Name");

			NsubDbContext.DbSet.Name.Should().BeEquivalentTo("Name");

			FieDbContext.DbSet.Name.Should().BeEquivalentTo("Name");

			#endregion
		}
	}
}