using FakeItEasy;
using Fakes;
using FluentAssertions;
using Moq;
using NSubstitute;
using NUnit.Framework;

namespace MockingFrameworksComparison.MatchingArgs
{
	internal class MatchingRefArg : SetupMock
	{
		private delegate void FillCallback(int id, ref Entity entity);

		[Test]
		public void MockWitMatchingAnyRefArg()
		{
			//Moq
			MoqRepository
				.Setup(x => x.Fill(ref It.Ref<Entity>.IsAny))
				.Returns(true);

			//NSubstitute
			//Does not have direct support for arg matching ref parameters:
			//https://github.com/nsubstitute/NSubstitute/issues/401
			Entity nsubRefEntity = null;
			NsubRepository
				.Fill(ref nsubRefEntity)
				.ReturnsForAnyArgs(true);

			//FakeItEasy
			//Does not have direct support for arg matching ref parameters:
			//https://fakeiteasy.readthedocs.io/en/stable/argument-constraints/
			Entity fieRefEntity = null;
			A.CallTo(() => FieRepository.Fill(ref fieRefEntity))
				.WithAnyArguments()
				.Returns(true);

			#region Assertion

			Entity moqEntity = null;
			MoqRepository.Object.Fill(ref moqEntity).Should().Be(true);

			Entity nsubEntity = null;
			NsubRepository.Fill(ref nsubEntity).Should().Be(true);

			Entity fieEntity = null;
			FieRepository.Fill(ref fieEntity).Should().Be(true);

			#endregion
		}

		[Test]
		public void MockWitMatchingArgAndAnyRefArg()
		{
			//Moq
			MoqRepository
				.Setup(x => x.Fill(1, ref It.Ref<Entity>.IsAny))
				.Callback(new FillCallback((int id, ref Entity entity) => entity = new Entity(1)))
				.Returns(true);

			//NSubstitute
			//Does not have direct support for arg matching ref parameters:
			//https://github.com/nsubstitute/NSubstitute/issues/401

			//FakeItEasy
			//Does not have direct support for arg matching ref parameters:
			//https://fakeiteasy.readthedocs.io/en/stable/argument-constraints/
			Entity fieRefEntity = null;
			A.CallTo(() => FieRepository.Fill(1, ref fieRefEntity))
				.WhenArgumentsMatch(x => x.Get<int>("id") == 1)
				.Returns(true)
				.AssignsOutAndRefParameters(new Entity(1));

			#region Assertion

			var moqEntity = new Entity(0);
			MoqRepository.Object.Fill(1, ref moqEntity).Should().Be(true);
			moqEntity.Should().BeEquivalentTo(new Entity(1));
			MoqRepository.Object.Fill(0, ref moqEntity).Should().Be(false);
			moqEntity.Should().BeEquivalentTo(new Entity(1));

			var fieEntity = new Entity(0);
			FieRepository.Fill(1, ref fieEntity).Should().Be(true);
			fieEntity.Should().BeEquivalentTo(new Entity(1));
			FieRepository.Fill(0, ref fieEntity).Should().Be(false);
			fieEntity.Should().BeEquivalentTo(new Entity(1));

			#endregion
		}
	}
}