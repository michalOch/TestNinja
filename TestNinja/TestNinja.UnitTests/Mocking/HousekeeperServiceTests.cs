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
        private string _statementFileName;

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

            _statementFileName = "fileName";
            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(() => _statementFileName);

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
            VerifyStatementGenerated();
        }

        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_HouskeepersInvalidEmail_ShouldNotGenerateStatements(string email)
        {
            _housekeeper.Email = email;
            _service.SendStatementEmails(_statementDate);
            VerifyStatementNotGenerated();
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _service.SendStatementEmails(_statementDate);
            VerifyEmailSent();
        }

        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_InvalidStatementFilename_ShouldNotEmailTheStatement(string statementFileName)
        {
            _statementFileName = statementFileName;
            _service.SendStatementEmails(_statementDate);
            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayAMessageBox()
        {
            _emailSender.Setup(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>())).Throws<Exception>();

            _service.SendStatementEmails(_statementDate);

            VerifyMessageBoxDisplayed();
        }

        private void VerifyEmailNotSent()
        {
            _emailSender.Verify(es => es.EmailFile(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                    Times.Never);
        }
        private void VerifyEmailSent()
        {
            _emailSender.Verify(es =>
                es.EmailFile(
                    _housekeeper.Email,
                    _housekeeper.StatementEmailBody,
                    _statementFileName,
                    It.IsAny<string>()));
        }
        private void VerifyStatementGenerated()
        {
            _statementGenerator.Verify(sg => sg.SaveStatement(
                _housekeeper.Oid,
                _housekeeper.FullName,
                _statementDate));
        }
        private void VerifyStatementNotGenerated()
        {
            _statementGenerator.Verify(sg => sg.SaveStatement(
                    _housekeeper.Oid,
                    _housekeeper.FullName,
                    _statementDate),
                    Times.Never);
        }
        private void VerifyMessageBoxDisplayed()
        {
            _messageBox.Verify(mb => mb.Show(
                It.IsAny<string>(),
                It.IsAny<string>(),
                MessageBoxButtons.OK));
        }
    }
}
