using FakeItEasy;
using Fakes;
using FluentAssertions;
using Moq;
using NSubstitute;
using NUnit.Framework;

namespace MockingFrameworksComparison.AccessArgs
{
	internal class AccessArg : SetupMock
	{
		[Test]
		public void MockWitAccessToArg()
		{
			//Moq
			MoqRepository
				.Setup(x => x.GetById(It.IsAny<int>()))
				.Returns((int id) => new Entity(id));

			//NSubstitute
			NsubRepository
				.GetById(Arg.Any<int>())
				.Returns(args => new Entity((int)args[0]));

			//FakeItEasy
			A.CallTo(() => FieRepository.GetById(A<int>._))
				.ReturnsLazily(x => new Entity(x.Arguments.Get<int>(0)));

			#region Assertion

			MoqRepository.Object.GetById(1).Should().BeEquivalentTo(new Entity(1));

			NsubRepository.GetById(1).Should().BeEquivalentTo(new Entity(1));

			FieRepository.GetById(1).Should().BeEquivalentTo(new Entity(1));

			#endregion
		}
	}
}
