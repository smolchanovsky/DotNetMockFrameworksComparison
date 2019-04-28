using FakeItEasy;
using Fakes;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace MockingFrameworksComparison.MockMethods
{
	internal class MockMethodWithOutArg : SetupMock
	{
		[Test]
		public void MockWithOutArg()
		{
			//Moq
			var moqOutEntity = new Entity(1);
			MoqRepository
				.Setup(x => x.TryGetById(1, out moqOutEntity))
				.Returns(true);

			//NSubstitute
			Entity nsubOutEntity;
			NsubRepository
				.TryGetById(1, out nsubOutEntity)
				.Returns(x =>
				{
					x[1] = new Entity(1);
					return true;
				});

			//FakeItEasy
			var fieOutEntity = new Entity(1); ;
			A.CallTo(() => FieRepository.TryGetById(1, out fieOutEntity))
				.Returns(true);

			#region Assertion

			MoqRepository.Object.TryGetById(1, out var moqEntity).Should().Be(true);
			moqEntity.Should().BeEquivalentTo(new Entity(1));

			NsubRepository.TryGetById(1, out var nsubEntity).Should().Be(true);
			nsubEntity.Should().BeEquivalentTo(new Entity(1));

			FieRepository.TryGetById(1, out var fieEntity).Should().Be(true);
			fieEntity.Should().BeEquivalentTo(new Entity(1));

			#endregion
		}
	}
}