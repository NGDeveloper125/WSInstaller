
namespace WSInstallerDomain;

public static class Extensions
{
    public static bool ServiceModelIssue(this string message) 
                        => message.Contains("The service name is invalid")
                        || message.Contains("The service name is already in use")
                        || message.Contains("The executable path is invalid")
                        || message.Contains("The service already exists")
                        || message.Contains("The account name is invalid")
                        || message.Contains("file not found");
}