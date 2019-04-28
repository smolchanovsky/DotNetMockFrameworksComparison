using FakeItEasy;
using Fakes;
using Moq;
using NSubstitute;
using NUnit.Framework;

namespace MockingFrameworksComparison
{
	internal class SetupMock
	{
		protected Mock<IRepository> MoqRepository;
		protected IRepository NsubRepository;
		protected IRepository FieRepository;
		protected Mock<IDbContext> MoqDbContext;
		protected IDbContext NsubDbContext;
		protected IDbContext FieDbContext;

		[SetUp]
		public void SetUpRepository()
		{
			//Moq
			MoqRepository = new Mock<IRepository>();

			//NSubstitute
			NsubRepository = Substitute.For<IRepository>();

			//FakeItEasy
			FieRepository = A.Fake<IRepository>();
		}

		[SetUp]
		public void SetUpDbContext()
		{
			//Moq
			MoqDbContext = new Mock<IDbContext>();

			//NSubstitute
			NsubDbContext = Substitute.For<IDbContext>();

			//FakeItEasy
			FieDbContext = A.Fake<IDbContext>();
		}
	}
}
	