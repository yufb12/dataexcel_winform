using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Resources;

// 有关程序集的常规信息通过下列属性集
// 控制。更改这些属性值可修改
// 与程序集关联的信息。
[assembly: AssemblyTitle(Feng.Excel.App.Product.AssemblyTitle)]
[assembly: AssemblyDescription(Feng.Excel.App.Product.AssemblyDescription)]
[assembly: AssemblyConfiguration(Feng.Excel.App.Product.AssemblyConfiguration)]
[assembly: AssemblyCompany(Feng.Excel.App.Product.AssemblyCompany)]
[assembly: AssemblyProduct(Feng.Excel.App.Product.AssemblyProduct)]
[assembly: AssemblyCopyright(Feng.Excel.App.Product.AssemblyCopyright)]
[assembly: AssemblyTrademark(Feng.Excel.App.Product.AssemblyTrademark)]
[assembly: AssemblyCulture(Feng.Excel.App.Product.AssemblyCulture)]

[assembly: System.Security.SecurityRules(System.Security.SecurityRuleSet.Level1)]
// 将 ComVisible 设置为 false 使此程序集中的类型
// 对 COM 组件不可见。如果需要从 COM 访问此程序集中的类型，
// 则将该类型上的 ComVisible 属性设置为 true。
[assembly: ComVisible(true)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid(Feng.Excel.App.Product.AssemblyGuid_______)]
[assembly: AllowPartiallyTrustedCallers()]

[assembly: Feng.Excel.App.Assembly()]
// 程序集的版本信息由下面四个值组成:
//
//      主版本
//      次版本
//      内部版本号
//      修订号
//
[assembly: AssemblyVersion(Feng.DataUtlis.SmallVersion.AssemblySecondVersion)]
[assembly: AssemblyFileVersion(Feng.DataUtlis.SmallVersion.AssemblySecondVersion)]
[assembly: AssemblyInformationalVersion(Feng.DataUtlis.SmallVersion.AssemblySecondVersion)]
 

[assembly: Dependency("System.Drawing,", LoadHint.Always)]
[assembly: Dependency("System,", LoadHint.Always)]
[assembly: Dependency("System.Data,", LoadHint.Always)]
[assembly: Dependency("System.XML,", LoadHint.Always)]
[assembly: Dependency("System.Windows.Forms,", LoadHint.Always)]
[assembly: Dependency("System.Configuration.Install,", LoadHint.Always)]
[assembly: Dependency("System.Management,", LoadHint.Always)]
[assembly: Dependency("System.Design,", LoadHint.Always)]
[assembly: Dependency("DataUtils,", LoadHint.Always)]
 