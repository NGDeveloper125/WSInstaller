
namespace WSInstallerDomain;

public static class ServiceManager
{

    public static ProcessResult RegisterService(ServiceModel serviceModel)
    {
        ProcessResult result = new ProcessResult(false, "Fail to run register process");
        result = ServiceProcessManager.Execute(serviceModel, ExecuterType.sc);
        if(!result.Success && result.Message.ServiceModelIssue())
        {
            result = ServiceProcessManager.Execute(serviceModel, ExecuterType.PowerShell);
        }
        return result;
    }
}
