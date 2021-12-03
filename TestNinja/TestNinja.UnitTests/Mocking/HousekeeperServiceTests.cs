using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
