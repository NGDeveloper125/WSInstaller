using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Runtime.InteropServices.JavaScript;
using System.Text;

namespace WSInstallerDomain.ProcessManager;

public static class PowerShellManager
{

public static void RegisterService(ServiceModel serviceModel)
{
    string cmd = GenerateServiceRegistrationCommand(serviceModel);
    using (PowerShell powerShell = PowerShell.Create())
    {
        powerShell.AddScript(cmd);

        Collection<PSObject> results = powerShell.Invoke();

        if (powerShell.Streams.Error.Count > 0)
        {
            string errorMessage = string.Join(Environment.NewLine, powerShell.Streams.Error.Select(e => e.ToString()));
            throw new Exception($"Error registering service: {errorMessage}");
        }
    }
}

    private static string GenerateServiceRegistrationCommand(ServiceModel serviceModel)
    {
        StringBuilder commandBuilder = new StringBuilder("New-Service -Name '");
        commandBuilder.Append(serviceModel.ServiceName);
        commandBuilder.Append("' -BinaryPathName '");
        commandBuilder.Append(serviceModel.BinaryPathName);
        commandBuilder.Append("'");

        if (!string.IsNullOrEmpty(serviceModel.DisplayName))
        {
            commandBuilder.Append(" -DisplayName '");
            commandBuilder.Append(serviceModel.DisplayName);
            commandBuilder.Append("'");
        }

        if (!string.IsNullOrEmpty(serviceModel.Description))
        {
            commandBuilder.Append(" -Description '");
            commandBuilder.Append(serviceModel.Description);
            commandBuilder.Append("'");
        }

        if (serviceModel.DependsOn != null && serviceModel.DependsOn.Any())
        {
            commandBuilder.Append(" -DependsOn @(");
            commandBuilder.Append(string.Join(",", serviceModel.DependsOn.Select(d => $"'{d}'")));
            commandBuilder.Append(")");
        }

        commandBuilder.Append(" -StartupType ");
        commandBuilder.Append(serviceModel.StartupType);

        if (!string.IsNullOrEmpty(serviceModel.ObjectName))
        {
            commandBuilder.Append(" -Credential (Get-Credential -UserName '");
            commandBuilder.Append(serviceModel.ObjectName);
            commandBuilder.Append("' -Message 'Enter password')");
        }
        else if (!string.IsNullOrEmpty(serviceModel.Password))
        {
            commandBuilder.Append(" -Credential (New-Object System.Management.Automation.PSCredential ('NT AUTHORITY\\LocalService', (ConvertTo-SecureString -String '");
            commandBuilder.Append(serviceModel.Password);
            commandBuilder.Append("' -AsPlainText -Force)))");
        }

        commandBuilder.Append(" -ServiceType '");
        commandBuilder.Append(serviceModel.ServiceType);
        commandBuilder.Append("'");

        commandBuilder.Append(" -ErrorControl ");
        commandBuilder.Append(serviceModel.ErrorControl);

        if (serviceModel.DelayedAutoStart)
        {
            commandBuilder.Append(" -DelayedAutoStart");
        }

        if (serviceModel.LoadOrderGroup != null && serviceModel.LoadOrderGroup.Any())
        {
            commandBuilder.Append(" -LoadOrderGroup @(");
            commandBuilder.Append(string.Join(",", serviceModel.LoadOrderGroup.Select(g => $"'{g}'")));
            commandBuilder.Append(")");
        }

        if (serviceModel.Tags != null && serviceModel.Tags.Any())
        {
            commandBuilder.Append(" -Tag @(");
            commandBuilder.Append(string.Join(",", serviceModel.Tags.Select(t => $"'{t}'")));
            commandBuilder.Append(")");
        }

        if (serviceModel.DesktopInteract)
        {
            commandBuilder.Append(" -DesktopInteract");
        }

        return commandBuilder.ToString();
    }
}