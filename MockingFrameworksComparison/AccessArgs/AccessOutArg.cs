using FakeItEasy;
using Fakes;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace MockingFrameworksComparison.AccessArgs
{
	internal class AccessOutArg : SetupMock
	{
		private delegate void TryGetByIdCallback(int id, out Entity entity);

		[Test]
		public void MockMethodWithAccessToOutArg()
		{
			//Moq
			Entity moqOutEntity;
			MoqRepository
				.Setup(x => x.TryGetById(1, out moqOutEntity))
				.Callback(new TryGetByIdCallback((int id, out Entity entity) => entity = new Entity(1)))
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
			Entity fieOutEntity;
			A.CallTo(() => FieRepository.TryGetById(1, out fieOutEntity))
				.Returns(true)
				.AssignsOutAndRefParameters(new Entity(1));

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