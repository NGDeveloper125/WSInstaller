using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WSInstallerDomain;

namespace WSInstallerInterface;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private string serviceName;
    public string ServiceName
    {
        get { return serviceName; }
        set
        {
            serviceName = value;
            OnPropertyChanged(nameof(ServiceName));
        }
    }

    public string ServiceNameComment { get; set; }

    private string binPath;
    public string BinPath
    {
        get { return binPath; }
        set
        {
            binPath = value;
            OnPropertyChanged(nameof(BinPath));
        }
    }
    public string BinPathComment { get; set; }
    private string displayName;
    public string DisplayName
    {
        get { return displayName; }
        set
        {
            displayName = value;
            OnPropertyChanged(nameof(DisplayName));
        }
    }
    public string DisplayNameComment { get; set; }
    private string startAutomaticlly;
    public string StartAutomaticlly
    {
        get { return startAutomaticlly; }
        set
        {
            startAutomaticlly = value;
            OnPropertyChanged(nameof(StartAutomaticlly));
        }
    }
    public string StartAutomaticllyComment { get; set; }
    private string description;
    public string Description
    {
        get { return description; }
        set
        {
            description = value;
            OnPropertyChanged(nameof(Description));
        }
    }
    public string DescriptionComment { get; set; }
    private string StartupType;
    public string startupType
    {
        get { return startupType; }
        set
        {
            startupType = value;
            OnPropertyChanged(nameof(StartupType));
        }
    }
    public string StartupTypeComment { get; set; }
    private string objectName;
    public string ObjectName
    {
        get { return objectName; }
        set
        {
            objectName = value;
            OnPropertyChanged(nameof(ObjectName));
        }
    }
    public string ObjectNameComment { get; set; }
    private string password;
    public string Password
    {
        get { return password; }
        set
        {
            password = value;
            OnPropertyChanged(nameof(Password));
        }
    }
    public string PasswordComment { get; set; }
    private string serviceType;
    public string ServiceType
    {
        get { return serviceType; }
        set
        {
            serviceType = value;
            OnPropertyChanged(nameof(ServiceType));
        }
    }
    public string ServiceTypeComment { get; set; }
    private string errorControl;
    public string ErrorControl
    {
        get { return errorControl; }
        set
        {
            errorControl = value;
            OnPropertyChanged(nameof(ErrorControl));
        }
    }
    public string ErrorControlComment { get; set; }
    private string delayedAutoStart;
    public string DelayedAutoStart
    {
        get { return delayedAutoStart; }
        set
        {
            delayedAutoStart = value;
            OnPropertyChanged(nameof(DelayedAutoStart));
        }
    }
    public string DelayedAutoStartComment { get; set; }
    private string dependsOn;
    public string DependsOn
    {
        get { return dependsOn; }
        set
        {
            dependsOn = value;
            OnPropertyChanged(nameof(DependsOn));
        }
    }
    public string DependsOnComment { get; set; }
    private string loadOrderGroup;
    public string LoadOrderGroup
    {
        get { return loadOrderGroup; }
        set
        {
            loadOrderGroup = value;
            OnPropertyChanged(nameof(LoadOrderGroup));
        }
    }
    public string LoadOrderGroupComment { get; set; }
    private string tags;
    public string Tags
    {
        get { return tags; }
        set
        {
            tags = value;
            OnPropertyChanged(nameof(Tags));
        }
    }
    public string TagsComment { get; set; }

    private string desktopInteract;
    public string DesktopInteract
    {
        get { return desktopInteract; }
        set
        {
            desktopInteract = value;
            OnPropertyChanged(nameof(DesktopInteract));
        }
    }
    public string DesktopInteractComment { get; set; }

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
    }

    public void OnClick_RegisterService(object sender, RoutedEventArgs e)
    {
        ServiceModelResult result = ServiceModelBuilder.BuildServiceModel(ServiceName, BinPath, DisplayName, Description, StartupType, Password, DelayedAutoStart, ObjectName, ServiceType, ErrorControl, DependsOn, LoadOrderGroup, Tags, DesktopInteract);
        if (!result.Success)
        {
            switch (result.IssueOf)
            {
                case 1: ServiceNameComment = result.IssueMessage; break;
                case 2: BinPathComment = result.IssueMessage; break;
                case 3: DisplayNameComment = result.IssueMessage; break;
                case 4: DescriptionComment = result.IssueMessage; break;
                case 5: StartupTypeComment = result.IssueMessage; break;
                case 6: Password = result.IssueMessage; break;
                case 7: DelayedAutoStartComment = result.IssueMessage; break;
                case 8: ObjectNameComment = result.IssueMessage; break;
                case 9: ServiceTypeComment = result.IssueMessage; break;
                case 10: ErrorControlComment = result.IssueMessage; break;
                case 11: DependsOnComment = result.IssueMessage; break;
                case 12: LoadOrderGroupComment = result.IssueMessage; break;
                case 13: TagsComment = result.IssueMessage; break;
                case 14: DesktopInteractComment = result.IssueMessage; break;
                default: MessageBox.Show("Unknown error"); break;
            }
            //update ui
        }
        else
        {
            ServiceManager.RegisterService(result.ServiceModel);
        }
    }
}
