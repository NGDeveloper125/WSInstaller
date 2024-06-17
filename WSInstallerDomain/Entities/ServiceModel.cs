
namespace WSInstallerDomain;

public record ServiceModel(string ServiceName,
                           string BinaryPathName,
                           string DisplayName = null,
                           string Description = null,
                           string[] DependsOn = null,
                           ServiceStartupType StartupType = ServiceStartupType.Automatic,
                           string ObjectName = null,
                           string Password = null,
                           ServiceType ServiceType = ServiceType.Win32OwnProcess,
                           ErrorControl ErrorControl = ErrorControl.Normal,
                           bool DelayedAutoStart = false,
                           string[] LoadOrderGroup = null,
                           string[] Tags = null,
                           bool DesktopInteract = false);





