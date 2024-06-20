
using System.Diagnostics;
using WSInstallerDomain.ProcessManager;

namespace WSInstallerDomain;

public static class ServiceProcessManager
{
    public static ProcessResult Execute(ServiceModel serviceModel, ExecuterType executerType)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo()
        {
            FileName = executerType switch
            {
                ExecuterType.PowerShell => "powershell.exe",
                ExecuterType.sc => "sc.exe"
            },
            Arguments = GenerateScript(serviceModel, executerType),
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            UseShellExecute = false
        };

        using Process process = new Process();
        process.StartInfo = startInfo;
        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();
        process.WaitForExit();

        return HandleProcessResult(output, error);
    }

    private static string GenerateScript(ServiceModel serviceModel, ExecuterType executerType) => executerType switch
    {
        ExecuterType.PowerShell => PowerShellScriptGenerator.GenerateServiceRegistrationCommand(serviceModel),
        ExecuterType.sc => SCScriptGenerator.GenerateServiceRegistrationCommand(serviceModel)
    };
    

    private static ProcessResult HandleProcessResult(string output, string error) =>
                     string.IsNullOrEmpty(error) ? new ProcessResult(true, output) : new ProcessResult(false, error);
    
}