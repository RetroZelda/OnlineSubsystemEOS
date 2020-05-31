// (C) Gaslight Games Ltd, 2019-2020.  All rights reserved.

using System.IO;
using UnrealBuildTool;

public class EOSSDK : ModuleRules
{
#if WITH_FORWARDED_MODULE_RULES_CTOR
    public EOSSDK( ReadOnlyTargetRules Target ) : base( Target )
#else
    public EOSSDK( TargetInfo Target )
#endif
    {
        // The currently supported version of the EOS SDK.
        // EOSVersion = "1.6";

        Type = ModuleType.External;

        if( Target.Configuration == UnrealTargetConfiguration.Development )
        {
            OptimizeCode = CodeOptimization.Never;
        }

        // we want to link to 3rd party libs through the project, and not through the plugin
        string combined = Path.Combine( PluginDirectory, "..", "3rdparty", "EOS", "SDK" );
        string BaseDirectory = Path.GetFullPath( combined );

        // Check if the EOS SDK exists.
        if( Directory.Exists( BaseDirectory ) )
        {
            PublicDefinitions.Add( "EOS_SDK_INSTALLED" );
        }

        // Include headers
        PublicIncludePaths.Add( Path.Combine( BaseDirectory, "Include" ) );

        if( Target.Platform == UnrealTargetPlatform.Win64 )
        {
            // Add the import library
            PublicAdditionalLibraries.Add( Path.Combine( BaseDirectory, "Lib", "EOSSDK-Win64-Shipping.lib" ) );

            // Dlls
            RuntimeDependencies.Add( Path.Combine( BaseDirectory, "Bin", "EOSSDK-Win64-Shipping.dll" ) );
            PublicDelayLoadDLLs.Add( "EOSSDK-Win64-Shipping.dll" );
        }
        
        if( Target.Platform == UnrealTargetPlatform.Win32 )
        {
            // Add the import library
            PublicAdditionalLibraries.Add( Path.Combine( BaseDirectory, "Lib", "EOSSDK-Win32-Shipping.lib" ) );

            // Dlls
            RuntimeDependencies.Add( Path.Combine( BaseDirectory, "Bin", "EOSSDK-Win32-Shipping.dll" ) );
            PublicDelayLoadDLLs.Add( "EOSSDK-Win32-Shipping.dll" );
        }
        else if( Target.Platform == UnrealTargetPlatform.Linux )
        {
            // Add the import library
            PublicAdditionalLibraries.Add( Path.Combine( BaseDirectory, "Bin", "libEOSSDK-Linux-Shipping.so" ) );
            RuntimeDependencies.Add( Path.Combine( BaseDirectory, "Bin", "libEOSSDK-Linux-Shipping.so" ) );
        }
        else if( Target.Platform == UnrealTargetPlatform.Mac )
        {
            // Add the import library
            PublicAdditionalLibraries.Add( Path.Combine( BaseDirectory, "Bin", "libEOSSDK-Mac-Shipping.dylib" ) );
            RuntimeDependencies.Add( Path.Combine( BaseDirectory, "Bin", "libEOSSDK-Mac-Shipping.dylib" ) );
        }
    }
}
