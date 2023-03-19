﻿using DigitalBank.Domain.Entities;
using DigitalBank.Domain.Interfaces.Commands;
using DigitalBank.GraphQL.Types;
using GraphQL.Types;

namespace DigitalBank.Tests.Types;

[TestClass]
public class CommandResultTypeTests
{
    [TestMethod]
    public void CommandResultType_Void_HasCorrectFields()
    {
        // Arrange
        var commandResultType = new CommandResultType();

        // Act
        var statusField = commandResultType.Fields.FirstOrDefault(f => f.Name == "Status");
        var messageField = commandResultType.Fields.FirstOrDefault(f => f.Name == "Message");
        var errorCodeField = commandResultType.Fields.FirstOrDefault(f => f.Name == "ErrorCode");

        // Assert
        Assert.IsNotNull(statusField);
        Assert.AreEqual("The status of the command", statusField.Description);

        Assert.IsNotNull(messageField);
        Assert.AreEqual("The message of the command", messageField.Description);

        Assert.IsNotNull(errorCodeField);
        Assert.AreEqual("The error code of the command", errorCodeField.Description);
    }

    [TestMethod]
    public void CommandResultType_Generic_HasCorrectFields()
    {
        // Arrange
        var commandResultType = new CommandResultType<Account, AccountType>();

        // Act
        var statusField = commandResultType.Fields.FirstOrDefault(f => f.Name == "Status");
        var messageField = commandResultType.Fields.FirstOrDefault(f => f.Name == "Message");
        var resultField = commandResultType.Fields.FirstOrDefault(f => f.Name == "Result");
        var errorCodeField = commandResultType.Fields.FirstOrDefault(f => f.Name == "ErrorCode");

        // Assert
        Assert.IsNotNull(statusField);
        Assert.AreEqual("The status of the command", statusField.Description);

        Assert.IsNotNull(messageField);
        Assert.AreEqual("The message of the command", messageField.Description);

        Assert.IsNotNull(resultField);
        Assert.AreEqual("The result of the command", resultField.Description);

        Assert.IsNotNull(errorCodeField);
        Assert.AreEqual("The error code of the command", errorCodeField.Description);
    }
}