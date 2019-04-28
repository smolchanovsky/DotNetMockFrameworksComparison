using FakeItEasy;
using Fakes;
using FluentAssertions;
using Moq;
using NSubstitute;
using NUnit.Framework;

namespace MockingFrameworksComparison.MatchingArgs
{
	internal class MatchingArg : SetupMock
	{
		[Test]
		public void MockWitMatchingConcreteArg()
		{
			//Moq
			MoqRepository
				.Setup(x => x.GetById(1))
				.Returns(new Entity(1));

			//NSubstitute
			NsubRepository
				.GetById(1)
				.Returns(new Entity(1));

			//FakeItEasy
			A.CallTo(() => FieRepository.GetById(1))
				.Returns(new Entity(1));

			#region Assetion

			MoqRepository.Object.GetById(1).Should().BeEquivalentTo(new Entity(1));
			MoqRepository.Object.GetById(0).Should().Be(null);

			NsubRepository.GetById(1).Should().BeEquivalentTo(new Entity(1));
			NsubRepository.GetById(0).Should().Be(null);

			FieRepository.GetById(1).Should().BeEquivalentTo(new Entity(1));
			FieRepository.GetById(0).Should().BeEquivalentTo(A.Fake<Entity>());

			#endregion
		}

		[Test]
		public void MockWitMatchingAnyArg()
		{
			//Moq
			MoqRepository
				.Setup(x => x.GetById(It.IsAny<int>()))
				.Returns(new Entity(1));

			//NSubstitute
			NsubRepository
				.GetById(Arg.Any<int>())
				.Returns(new Entity(1));

			//FakeItEasy
			A.CallTo(() => FieRepository.GetById(A<int>._))
				.Returns(new Entity(1));;

			#region Assertion

			MoqRepository.Object.GetById(0).Should().BeEquivalentTo(new Entity(1));

			NsubRepository.GetById(0).Should().BeEquivalentTo(new Entity(1));

			FieRepository.GetById(0).Should().BeEquivalentTo(new Entity(1));

			#endregion
		}

		[Test]
		public void MockWithMatchingSpecificArg()
		{
			//Moq
			MoqRepository
				.Setup(x => x.GetById(It.Is<int>(a => a > 0)))
				.Returns(new Entity(1));

			//NSubstitute
			NsubRepository
				.GetById(Arg.Is<int>(x => x > 0))
				.Returns(new Entity(1));

			//FakeItEasy
			A.CallTo(() => FieRepository.GetById(A<int>.That.IsGreaterThan(0)))
				.Returns(new Entity(1));

			#region Assertion

			MoqRepository.Object.GetById(1).Should().BeEquivalentTo(new Entity(1));
			MoqRepository.Object.GetById(0).Should().Be(null);

			NsubRepository.GetById(1).Should().BeEquivalentTo(new Entity(1));
			NsubRepository.GetById(0).Should().Be(null);

			FieRepository.GetById(1).Should().BeEquivalentTo(new Entity(1));
			FieRepository.GetById(0).Should().BeEquivalentTo(A.Fake<Entity>());

			#endregion
		}
	}
}