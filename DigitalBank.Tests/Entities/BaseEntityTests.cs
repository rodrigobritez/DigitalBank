using DigitalBank.Domain.Entities.Base;

namespace DigitalBank.Tests.Entities;

[TestClass]
public class BaseEntityTests
{
  [TestMethod]
  public void Id_Should_Be_Unique()
  {
    // Arrange
    BaseEntity entity1 = new BaseEntity();
    BaseEntity entity2 = new BaseEntity();

    // Assert
    Assert.AreNotEqual(entity1.Id, entity2.Id);
  }

  [TestMethod]
  public void Deleted_Should_Default_To_False()
  {
    // Arrange
    BaseEntity entity = new BaseEntity();

    // Assert
    Assert.IsFalse(entity.Deleted);
  }

  [TestMethod]
  public void CreatedAt_Should_Be_Set()
  {
    // Arrange
    BaseEntity entity = new BaseEntity();

    // Assert
    Assert.AreNotEqual(default(DateTime), entity.CreatedAt);
  }

  [TestMethod]
  public void UpdatedAt_Should_Be_Null_By_Default()
  {
    // Arrange
    BaseEntity entity = new BaseEntity();

    // Assert
    Assert.IsNull(entity.UpdatedAt);
  }

  [TestMethod]
  public void DeletedAt_Should_Be_Null_By_Default()
  {
    // Arrange
    BaseEntity entity = new BaseEntity();

    // Assert
    Assert.IsNull(entity.DeletedAt);
  }
}