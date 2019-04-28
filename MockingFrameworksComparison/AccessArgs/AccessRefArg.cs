using FakeItEasy;
using Fakes;
using FluentAssertions;
using Moq;
using NSubstitute;
using NUnit.Framework;

namespace MockingFrameworksComparison.AccessArgs
{
	internal class AccessRefArg : SetupMock
	{
		private delegate void FillCallback(ref Entity entity);

		[Test]
		public void MockMethodWithAccessToRefArg()
		{
			//Moq
			MoqRepository
				.Setup(x => x.Fill(ref It.Ref<Entity>.IsAny))
				.Callback(new FillCallback((ref Entity entity) => entity = new Entity(1)))
				.Returns(true);

			//NSubstitute
			Entity nsubRefEntity = null;
			NsubRepository
				.Fill(ref nsubRefEntity)
				.ReturnsForAnyArgs(x =>
				{
					x[0] = new Entity(1);
					return true;
				});

			//FakeItEasy
			Entity fieRefEntity = null;
			A.CallTo(() => FieRepository.Fill(ref fieRefEntity))
				.WithAnyArguments()
				.Returns(true)
				.AssignsOutAndRefParameters(new Entity(1));

			#region Assertion

			var moqEntity = new Entity(0);
			MoqRepository.Object.Fill(ref moqEntity).Should().Be(true);
			moqEntity.Should().BeEquivalentTo(new Entity(1));

			var nsubEntity = new Entity(0);
			NsubRepository.Fill(ref nsubEntity).Should().Be(true);
			nsubEntity.Should().BeEquivalentTo(new Entity(1));

			var fieEntity = new Entity(0);
			FieRepository.Fill(ref fieEntity).Should().Be(true);
			fieEntity.Should().BeEquivalentTo(new Entity(1));

			#endregion
		}
	}
}