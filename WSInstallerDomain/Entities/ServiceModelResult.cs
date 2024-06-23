
namespace WSInstallerDomain;

public record ServiceModelResult(bool Success, ServiceModel ServiceModel, int IssueOf, string IssueMessage);