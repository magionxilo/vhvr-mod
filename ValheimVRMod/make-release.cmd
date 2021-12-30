if not exist  "%COMMON_DIR%Valheim\BepInEx\plugins" mkdir  "%COMMON_DIR%Valheim\BepInEx\plugins"
copy "%SOLUTION_DIR%ValheimVRMod\bin\Debug\ValheimVRMod.dll" "%COMMON_DIR%Valheim\BepInEx\plugins"
if not exist "%COMMON_DIR%Valheim\Valheim_Data\Managed" mkdir "%COMMON_DIR%Valheim\Valheim_Data\Managed"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Managed\SteamVR.dll" "%COMMON_DIR%Valheim\Valheim_Data\Managed"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Managed\SteamVR_Actions.dll" "%COMMON_DIR%Valheim\Valheim_Data\Managed"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Managed\Unity.XR.Management.dll" "%COMMON_DIR%Valheim\Valheim_Data\Managed"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Managed\Unity.XR.OpenVR.dll" "%COMMON_DIR%Valheim\Valheim_Data\Managed"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Managed\netstandard.dll" "%COMMON_DIR%Valheim\Valheim_Data\Managed"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Managed\UnityEngine.XR.LegacyInputHelpers.dll" "%COMMON_DIR%Valheim\Valheim_Data\Managed"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Managed\UnityEngine.SpatialTracking.dll" "%COMMON_DIR%Valheim\Valheim_Data\Managed"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Managed\amplify_occlusion.dll" "%COMMON_DIR%Valheim\Valheim_Data\Managed"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Managed\final_ik.dll" "%COMMON_DIR%Valheim\Valheim_Data\Managed"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Managed\root_motion_demo_assets.dll" "%COMMON_DIR%Valheim\Valheim_Data\Managed"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Managed\root_motion_shared.dll" "%COMMON_DIR%Valheim\Valheim_Data\Managed"


if not exist "%COMMON_DIR%Valheim\Valheim_Data\Plugins\x86_64" mkdir "%COMMON_DIR%Valheim\Valheim_Data\Plugins\x86_64"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Plugins\x86_64\XRSDKOpenVR.dll" "%COMMON_DIR%Valheim\Valheim_Data\Plugins\x86_64"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Plugins\x86_64\openvr_api.dll" "%COMMON_DIR%Valheim\Valheim_Data\Plugins\x86_64"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Plugins\x86_64\ucrtbased.dll" "%COMMON_DIR%Valheim\Valheim_Data\Plugins\x86_64"

if not exist "%COMMON_DIR%Valheim\Valheim_Data\UnitySubsystems" mkdir "%COMMON_DIR%Valheim\Valheim_Data\UnitySubsystems"
if not exist "%COMMON_DIR%Valheim\Valheim_Data\UnitySubsystems\XRSDKOpenVR" mkdir "%COMMON_DIR%Valheim\Valheim_Data\UnitySubsystems\XRSDKOpenVR"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\UnitySubsystems\XRSDKOpenVR\UnitySubsystemsManifest.json" "%COMMON_DIR%Valheim\Valheim_Data\UnitySubsystems\XRSDKOpenVR"

if not exist  "%COMMON_DIR%Valheim\Valheim_Data\StreamingAssets\SteamVR" mkdir  "%COMMON_DIR%Valheim\Valheim_Data\StreamingAssets\SteamVR"
Xcopy /Y /E /I "%SOLUTION_DIR%Unity\build\ValheimVR_Data\StreamingAssets\SteamVR"  "%COMMON_DIR%Valheim\Valheim_Data\StreamingAssets\SteamVR"

if not exist "%COMMON_DIR%Valheim\Valheim_Data\StreamingAssets" mkdir "%COMMON_DIR%Valheim\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\AssetBundles"  "%COMMON_DIR%Valheim\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\AssetBundles.manifest"  "%COMMON_DIR%Valheim\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\steamvr_shaders"  "%COMMON_DIR%Valheim\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\steamvr_shaders.manifest"  "%COMMON_DIR%Valheim\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\xrmanager"  "%COMMON_DIR%Valheim\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\xrmanager.manifest"  "%COMMON_DIR%Valheim\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\steamvr_player_prefabs" "%COMMON_DIR%Valheim\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\steamvr_player_prefabs.manifest"  "%COMMON_DIR%Valheim\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\vhvr_custom" "%COMMON_DIR%Valheim\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\vhvr_custom.manifest"  "%COMMON_DIR%Valheim\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\amplify_resources" "%COMMON_DIR%Valheim\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\amplify_resources.manifest"  "%COMMON_DIR%Valheim\Valheim_Data\StreamingAssets"

if not exist "%SOLUTION_DIR%ValheimVRMod\release" mkdir "%SOLUTION_DIR%ValheimVRMod\release"
if not exist "%SOLUTION_DIR%ValheimVRMod\release\BepInEx" mkdir "%SOLUTION_DIR%ValheimVRMod\release\BepInEx"
if not exist "%SOLUTION_DIR%ValheimVRMod\release\BepInEx\plugins" mkdir  "%SOLUTION_DIR%ValheimVRMod\release\BepInEx\plugins"
copy "%SOLUTION_DIR%ValheimVRMod\bin\Debug\ValheimVRMod.dll" "%SOLUTION_DIR%ValheimVRMod\release\BepInEx\plugins"

if not exist "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data" mkdir "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data"
if not exist "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\Managed" mkdir "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\Managed"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Managed\SteamVR.dll" "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\Managed"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Managed\SteamVR_Actions.dll" "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\Managed"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Managed\Unity.XR.Management.dll" "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\Managed"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Managed\Unity.XR.OpenVR.dll" "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\Managed"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Managed\netstandard.dll" "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\Managed"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Managed\UnityEngine.XR.LegacyInputHelpers.dll" "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\Managed"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Managed\UnityEngine.SpatialTracking.dll" "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\Managed"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Managed\amplify_occlusion.dll" "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\Managed"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Managed\final_ik.dll" "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\Managed"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Managed\root_motion_demo_assets.dll" "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\Managed"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Managed\root_motion_shared.dll" "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\Managed"

if not exist "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\Plugins" mkdir if not exist "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\Plugins"
if not exist "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\Plugins\x86_64" mkdir "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\Plugins\x86_64"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Plugins\x86_64\XRSDKOpenVR.dll" "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\Plugins\x86_64"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Plugins\x86_64\openvr_api.dll" "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\Plugins\x86_64"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\Plugins\x86_64\ucrtbased.dll" "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\Plugins\x86_64"

if not exist "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\UnitySubsystems" mkdir "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\UnitySubsystems"
if not exist "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\UnitySubsystems\XRSDKOpenVR" mkdir "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\UnitySubsystems\XRSDKOpenVR"
copy "%SOLUTION_DIR%Unity\build\ValheimVR_Data\UnitySubsystems\XRSDKOpenVR\UnitySubsystemsManifest.json" "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\UnitySubsystems\XRSDKOpenVR"

if not exist "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\StreamingAssets" mkdir "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\StreamingAssets"
if not exist "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\StreamingAssets\SteamVR" mkdir  "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\StreamingAssets\SteamVR"
Xcopy /Y /E /I "%SOLUTION_DIR%Unity\build\ValheimVR_Data\StreamingAssets\SteamVR"  "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\StreamingAssets\SteamVR"

copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\AssetBundles"  "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\AssetBundles.manifest"  "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\steamvr_shaders"  "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\steamvr_shaders.manifest"  "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\xrmanager"  "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\xrmanager.manifest"  "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\steamvr_player_prefabs" "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\steamvr_player_prefabs.manifest"  "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\vhvr_custom" "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\vhvr_custom.manifest"  "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\amplify_resources" "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\StreamingAssets"
copy  "%SOLUTION_DIR%Unity\ValheimVR\Assets\AssetBundles\amplify_resources.manifest"  "%SOLUTION_DIR%ValheimVRMod\release\Valheim_Data\StreamingAssets"