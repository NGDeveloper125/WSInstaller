
using System.Text;

namespace WSInstallerDomain;

public static class SCScriptGenerator
{
    public static string GenerateServiceRegistrationCommand(ServiceModel serviceModel)
    {
        StringBuilder commandBuilder = new StringBuilder("sc.exe create ");
        commandBuilder.Append(serviceModel.ServiceName);
        commandBuilder.Append(" binPath= ");
        commandBuilder.Append(serviceModel.BinaryPathName);

        if (!string.IsNullOrEmpty(serviceModel.DisplayName))
        {
            commandBuilder.Append(" DisplayName= \"");
            commandBuilder.Append(serviceModel.DisplayName);
            commandBuilder.Append("\"");
        }

        if (!string.IsNullOrEmpty(serviceModel.Description))
        {
            commandBuilder.Append(" Description= \"");
            commandBuilder.Append(serviceModel.Description);
            commandBuilder.Append("\"");
        }

        if (serviceModel.DependsOn != null && serviceModel.DependsOn.Any())
        {
            commandBuilder.Append(" depend= ");
            commandBuilder.Append(string.Join("/", serviceModel.DependsOn));
        }

        commandBuilder.Append(" start= ");
        commandBuilder.Append(GetStartupTypeForSc(serviceModel.StartupType));

        if (!string.IsNullOrEmpty(serviceModel.ObjectName))
        {
            commandBuilder.Append(" obj= \"");
            commandBuilder.Append(serviceModel.ObjectName);
            commandBuilder.Append("\" password= *");
        }
        else if (!string.IsNullOrEmpty(serviceModel.Password))
        {
            commandBuilder.Append(" password= \"");
            commandBuilder.Append(serviceModel.Password);
            commandBuilder.Append("\"");
        }

        commandBuilder.Append(" type= ");
        commandBuilder.Append(GetServiceTypeForSc(serviceModel.ServiceType));

        commandBuilder.Append(" error= ");
        commandBuilder.Append(GetErrorControlForSc(serviceModel.ErrorControl));

        if (serviceModel.DelayedAutoStart)
        {
            commandBuilder.Append(" delayedAutoStart= 1");
        }

        if (serviceModel.LoadOrderGroup != null && serviceModel.LoadOrderGroup.Any())
        {
            commandBuilder.Append(" group= ");
            commandBuilder.Append(string.Join("/", serviceModel.LoadOrderGroup));
        }

        if (serviceModel.Tags != null && serviceModel.Tags.Any())
        {
            commandBuilder.Append(" tag= ");
            commandBuilder.Append(string.Join("/", serviceModel.Tags));
        }

        if (serviceModel.DesktopInteract)
        {
            commandBuilder.Append(" interactive= 1");
        }

        return commandBuilder.ToString();
    }

    private static string GetStartupTypeForSc(ServiceStartupType startupType)
    {
        return startupType switch
        {
            ServiceStartupType.Automatic => "auto",
            ServiceStartupType.Manual => "demand",
            ServiceStartupType.Disabled => "disabled",
            ServiceStartupType.Boot => "boot",
            _ => throw new ArgumentOutOfRangeException(nameof(startupType), startupType, null)
        };
    }

    private static string GetServiceTypeForSc(ServiceType serviceType)
    {
        return serviceType switch
        {
            ServiceType.KernelDriver => "kernel",
            ServiceType.FileSystemDriver => "filesys",
            ServiceType.Adapter => "adapter",
            ServiceType.RecognizerDriver => "recognizer",
            ServiceType.Win32OwnProcess => "own",
            ServiceType.Win32ShareProcess => "share",
            ServiceType.InteractiveProcess => "interact",
            ServiceType.PseucdoService => "pseudo",
            _ => throw new ArgumentOutOfRangeException(nameof(serviceType), serviceType, null)
        };
    }

    private static string GetErrorControlForSc(ErrorControl errorControl)
    {
        return errorControl switch
        {
            ErrorControl.Ignore => "ignore",
            ErrorControl.Normal => "normal",
            ErrorControl.Severe => "severe",
            ErrorControl.Critical => "critical",
            _ => throw new ArgumentOutOfRangeException(nameof(errorControl), errorControl, null)
        };
    }

}