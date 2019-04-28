using FakeItEasy;
using Fakes;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace MockingFrameworksComparison.MockMethods
{
	internal class MockMethod : SetupMock
	{

		[Test]
		public void MockMethodWithoutArg()
		{
			//Moq
			MoqRepository.Setup(x => x.GetAll()).Returns(new[] { new Entity(1) });

			//NSubstitute
			NsubRepository.GetAll().Returns(new[] { new Entity(1) });

			//FakeItEasy
			A.CallTo(() => FieRepository.GetAll()).Returns(new[] { new Entity(1) });

			#region Assetion

			MoqRepository.Object.GetAll().Should().BeEquivalentTo(new[] { new Entity(1) });

			NsubRepository.GetAll().Should().BeEquivalentTo(new[] { new Entity(1) });

			FieRepository.GetAll().Should().BeEquivalentTo(new[] { new Entity(1) });

			#endregion
		}

		[Test]
		public void MockMethodWithArg()
		{
			//Moq
			MoqRepository.Setup(x => x.GetById(1)).Returns(new Entity(1));

			//NSubstitute
			NsubRepository.GetById(1).Returns(new Entity(1));

			//FakeItEasy
			A.CallTo(() => FieRepository.GetById(1)).Returns(new Entity(1));

			#region Assetion

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
