
namespace WSInstallerDomain;

public static class ServiceModelBuilder
{
    public static ServiceModelResult BuildServiceModel(string? serviceName, 
                                                       string? binPath, 
                                                       string? displayName, 
                                                       string? description, 
                                                       string? startupType, 
                                                       string? password, 
                                                       string? delayedAutoStart, 
                                                       string? objectName, 
                                                       string? serviceType, 
                                                       string? errorControl, 
                                                       string? dependsOn,
                                                       string? loadOrderGroup,
                                                       string? tags,
                                                       string? desktopInteract)
    {
        if(string.IsNullOrEmpty(serviceName) || string.IsNullOrWhiteSpace(serviceName)) return new ServiceModelResult(false, null!, 1, "ServiceName is required");
        if(string.IsNullOrEmpty(binPath) || string.IsNullOrWhiteSpace(binPath)) return new ServiceModelResult(false, null!, 2, "BinPath is required");
        if(string.IsNullOrEmpty(displayName) || string.IsNullOrWhiteSpace(displayName)) return new ServiceModelResult(false, null!, 3, "DisplayName is required");
        if(string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description)) return new ServiceModelResult(false, null!, 4, "StartAutomaticlly is required");

        return new ServiceModelResult(true, new ServiceModel(serviceName, binPath, displayName, description, 
                                                             Array.Empty<string>(), ServiceStartupType.Automatic, 
                                                             objectName, password, ServiceType.PseucdoService, ErrorControl.Normal, 
                                                             false, Array.Empty<string>(), Array.Empty<string>(), false), 0, "Success");
    }
}