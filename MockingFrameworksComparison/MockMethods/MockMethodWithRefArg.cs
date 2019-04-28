using FakeItEasy;
using Fakes;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace MockingFrameworksComparison.MockMethods
{
	internal class MockMethodWithRefArg : SetupMock
	{
		[Test]
		public void MockWithRefArg()
		{
			//Moq
			var moqRefEntity = new Entity(1);
			MoqRepository
				.Setup(x => x.Fill(ref moqRefEntity))
				.Returns(true);

			//NSubstitute
			var nsubRefEntity = new Entity(1);
			NsubRepository
				.Fill(ref nsubRefEntity)
				.Returns(true);

			//FakeItEasy
			var fieRefEntity = new Entity(1);
			A.CallTo(() => FieRepository.Fill(ref fieRefEntity))
				.Returns(true);

			#region Assertion

			var moqEntity = moqRefEntity;
			MoqRepository.Object.Fill(ref moqEntity).Should().Be(true);
			moqEntity = null;
			MoqRepository.Object.Fill(ref moqEntity).Should().Be(false);

			var nsubEntity = nsubRefEntity;
			NsubRepository.Fill(ref nsubEntity).Should().Be(true);
			nsubEntity = null;
			NsubRepository.Fill(ref nsubEntity).Should().Be(false);

			var fieEntity = fieRefEntity;
			FieRepository.Fill(ref fieEntity).Should().Be(true);
			fieEntity = null;
			FieRepository.Fill(ref fieEntity).Should().Be(false);

			#endregion
		}
	}
}