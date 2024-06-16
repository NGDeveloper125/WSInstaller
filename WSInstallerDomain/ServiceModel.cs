
namespace WSInstallerDomain;

public record ServiceModel(string serviceName,
                           string binaryPathName,
                           string displayName = null,
                           string description = null,
                           string[] dependsOn = null,
                           ServiceStartupType startupType = ServiceStartupType.Automatic,
                           string objectName = null,
                           string password = null,
                           ServiceType serviceType = ServiceType.Win32OwnProcess,
                           ErrorControl errorControl = ErrorControl.Normal,
                           bool delayedAutoStart = false,
                           string[] loadOrderGroup = null,
                           string[] tags = null,
                           bool desktopInteract = false);





