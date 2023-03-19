using DigitalBank.Domain.Entities.Base;

namespace DigitalBank.Tests.Entities;

[TestClass]
public class BaseEntityTests
{
  [TestMethod]
  [TestCategory("Domain-Entity")]
  public void Id_Should_Be_Unique()
  {
    var entity1 = new BaseEntity();
    var entity2 = new BaseEntity();
    
    Assert.AreNotEqual(entity1.Id, entity2.Id);
  }

  [TestMethod]
  [TestCategory("Domain-Entity")]
  public void Deleted_Should_Default_To_False()
  {
    var entity = new BaseEntity();

    Assert.IsFalse(entity.Deleted);
  }

  [TestMethod]
  [TestCategory("Domain-Entity")]
  public void CreatedAt_Should_Be_Set()
  {
    var entity = new BaseEntity();
    
    Assert.AreNotEqual(default(DateTime), entity.CreatedAt);
  }

  [TestMethod]
  [TestCategory("Domain-Entity")]
  public void UpdatedAt_Should_Be_Null_By_Default()
  {
    var entity = new BaseEntity();
    
    Assert.IsNull(entity.UpdatedAt);
  }

  [TestMethod]
  [TestCategory("Domain-Entity")]
  public void DeletedAt_Should_Be_Null_By_Default()
  {
    var entity = new BaseEntity();
    
    Assert.IsNull(entity.DeletedAt);
  }
}