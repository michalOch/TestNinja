using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class HousekeeperServiceTests
    {
        // HousekeeperHelper
        // Method to test SendStatementEmails
        // Get the houskeepers from the database
        // For each house keeper its going to generate statement file and save it on the disk
        // Then email this statement file to the housekeeper

        // All the reqiured unit test for SendStatementEmails method

        // private methods SaveStatement and EmailFile are touching external resources 
        // need to extract these methods and put in seperate classes
        // then use DI to inject those dependencie into HousekeeperHelper class

        // need to create some of interaction test to check if house keeper helper object talks with other objects like statementGenerator, emailSender properly
        
        private Mock<IUnitOfWork> _unitOfWork;
        private Housekeeper _housekeeper;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;
        private HousekeeperService _service;
        private DateTime _statementDate = new DateTime(2017, 1, 1);
        private readonly string _statementFileName = "filename";

        [SetUp]
        public void SetUp()
        {        
            _housekeeper = new Housekeeper
            {
                Email = "a",
                FullName = "b",
                StatementEmailBody = "c",
                Oid = 1
            };

            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _housekeeper
            }.AsQueryable());

            _statementGenerator = new Mock<IStatementGenerator>();
            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();

            _service = new HousekeeperService(
                _unitOfWork.Object,
                _statementGenerator.Object,
                _emailSender.Object,
                _messageBox.Object);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_ShouldGenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => 
                sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate));
        }

        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_HouskeepersInvalidEmail_ShouldNotGenerateStatements(string email)
        {
            _housekeeper.Email = email;

            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg =>
                sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate), Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _statementGenerator.Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(_statementFileName);

            _service.SendStatementEmails(_statementDate);

            _emailSender.Verify(es =>
                es.EmailFile(
                    _housekeeper.Email, 
                    _housekeeper.StatementEmailBody, 
                    _statementFileName, 
                    It.IsAny<string>()));
        }

        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_InvalidStatementFilename_ShouldNotEmailTheStatement(string statementFileName)
        {
            _statementGenerator.Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(() => statementFileName);

            _service.SendStatementEmails(_statementDate);

            _emailSender.Verify(es =>
                es.EmailFile(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                    Times.Never);
        }

    }
}
