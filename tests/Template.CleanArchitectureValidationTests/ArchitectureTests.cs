using FluentAssertions;
using NetArchTest.Rules;

namespace Template.CleanArchitectureValidationTests;

public class ArchitectureTests
{
    private const string DomainNamespace = "Template.Domain";
    private const string ApplicationNamespace = "Template.Application";
    private const string InfrastructureNamespace = "Template.Infrastructure";
    private const string WebApiNamespace = "Template.WebApi";


    [Fact]
    public void Domain_Should_Not_Have_Dependency_On_Other_Projetcs()
    {
        // Arrange
        var otheProjetcs = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            WebApiNamespace
        };

        // Act
        var testResult = Types.InCurrentDomain()
                                    .That()
                                    .ResideInNamespace(DomainNamespace)
                                    .ShouldNot()
                                    .HaveDependencyOnAny(otheProjetcs)
                                    .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_Not_Have_Dependency_On_Other_Projects_Except_The_Domain_Project()
    {
        // Arrange
        var otheProjetcs = new[]
        {
            InfrastructureNamespace,
            WebApiNamespace
        };

        // Act
        var testResult = Types.InCurrentDomain()
                                    .That()
                                    .ResideInNamespace(ApplicationNamespace)
                                    .ShouldNot()
                                    .HaveDependencyOnAny(otheProjetcs)
                                    .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_Not_Have_Dependency_On_Webapi_Project()
    {
        // Arrange | Act
        var testResult = Types.InCurrentDomain()
                                    .That()
                                    .ResideInNamespace(InfrastructureNamespace)
                                    .ShouldNot()
                                    .HaveDependencyOn(WebApiNamespace)
                                    .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }
}