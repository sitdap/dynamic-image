# Thanks to AutoMapper for the inspiration for this build file.

$global:config = 'Release'
$framework = '4.0x86'
$product_name = "DynamicImage"
$fully_qualified_product_name = "SoundInTheory.DynamicImage"

properties {
    $base_dir = resolve-path .
    $source_dir = "$base_dir\src"
    $build_dir = "$base_dir\build"
    $package_dir_main = "$base_dir\package\DynamicImage"
    $package_dir_mvc = "$base_dir\package\DynamicImage.Mvc"
    $nunit_dir = "$base_dir\src\packages\NUnit.2.5.10.11092\tools"
    $nuget_dir = "$base_dir\src\packages\NuGet.CommandLine.1.6.0\tools"
}

task Default -depends Clean, Compile, Test, PackageMain, PackageMvc

task Clean {
    delete_directory "$package_dir_main"
    delete_directory "$package_dir_mvc"
}

task Compile -depends Clean {
    exec { msbuild /t:Clean /t:Build /p:Configuration=$config /v:q /nologo $source_dir\$fully_qualified_product_name.sln }
}

task Test -depends Compile {
    $tests_dir = "$source_dir/$fully_qualified_product_name.Tests/bin/$config"
    exec { & $nunit_dir\nunit-console-x86.exe $tests_dir/$fully_qualified_product_name.Tests.dll /nologo /nodots /xml=$tests_dir/$fully_qualified_product_name.Tests.TestResults.xml }
}

task PackageMain -depends Test {
    create_directory "$package_dir_main"
     
    # Copy NuSpec template files to package dir
    cp "$build_dir\$product_name\$product_name.nuspec" "$package_dir_main"
    copy_files "$build_dir\$product_name\content" "$package_dir_main\content" "*.*"
    cp "$build_dir\LICENSE.txt" "$package_dir_main"

    # Copy binary files to package dir
    copy_files "$source_dir\$fully_qualified_product_name\bin\$config" "$package_dir_main\lib\NET40" "$fully_qualified_product_name.dll","$fully_qualified_product_name.pdb"

    # Copy source files to package dir
    copy_files "$source_dir\$fully_qualified_product_name" "$package_dir_main\src\$fully_qualified_product_name" "*.cs"

    # Get the version number of main DLL
    $full_version = [Reflection.Assembly]::LoadFile("$source_dir\$fully_qualified_product_name\bin\$config\$fully_qualified_product_name.dll").GetName().Version
    $version = $full_version.Major.ToString() + "." + $full_version.Minor.ToString() + "." + $full_version.Build.ToString()

    # Build the NuGet package
    exec { & $nuget_dir\NuGet.exe pack -Symbols -Version "$version" -OutputDirectory "$package_dir_main" "$package_dir_main\$product_name.nuspec" }

    # Push NuGet package to nuget.org
    #exec { & $nuget_dir\NuGet.exe push "$package_dir_main\$product_name.$version.nupkg" }
}

task PackageMvc -depends Test {
    create_directory "$package_dir_mvc"
     
    # Copy NuSpec template files to package dir
    cp "$build_dir\$product_name.Mvc\$product_name.Mvc.nuspec" "$package_dir_mvc"
    cp "$build_dir\LICENSE.txt" "$package_dir_mvc"

    # Copy binary files to package dir
    copy_files "$source_dir\$fully_qualified_product_name.Mvc\bin\$config" "$package_dir_mvc\lib\NET40" "$fully_qualified_product_name.Mvc.dll","$fully_qualified_product_name.Mvc.pdb"

    # Copy source files to package dir
    copy_files "$source_dir\$fully_qualified_product_name.Mvc" "$package_dir_mvc\src\$fully_qualified_product_name.Mvc" "*.cs"

    # Get the version number of main DLL
    $full_version = [Reflection.Assembly]::LoadFile("$source_dir\$fully_qualified_product_name.Mvc\bin\$config\$fully_qualified_product_name.Mvc.dll").GetName().Version
    $version = $full_version.Major.ToString() + "." + $full_version.Minor.ToString() + "." + $full_version.Build.ToString()
    
    # Update DynamicImage dependency in NuSpec file with version number
    $doc = New-Object System.Xml.XmlDocument
    $doc.Load("$package_dir_mvc\$product_name.Mvc.nuspec")
    $dependency = $doc.SelectSingleNode("//dependency[@id = 'DynamicImage']")
    $dependency.SetAttribute("version", $version)
    $doc.Save("$package_dir_mvc\$product_name.Mvc.nuspec")

    # Build the NuGet package
    exec { & $nuget_dir\NuGet.exe pack -Symbols -Version "$version" -OutputDirectory "$package_dir_mvc" "$package_dir_mvc\$product_name.Mvc.nuspec" }

    # Push NuGet package to nuget.org
    #exec { & $nuget_dir\NuGet.exe push "$package_dir_mvc\$product_name.Mvc.$version.nupkg" }
}

# Helper functions

function global:delete_directory($directory_name) {
    rd $directory_name -recurse -force -ErrorAction SilentlyContinue | out-null
}

function global:create_directory($directory_name)
{
    mkdir $directory_name -ErrorAction SilentlyContinue | out-null
}

function global:copy_files($source, $destination, $include = @(), $exclude = @()) {
    create_directory $destination
    
    $items = Get-ChildItem $source -Recurse -Include $include -Exclude $exclude
    foreach ($item in $items) {
        $dir = $item.DirectoryName.Replace($source,$destination)
        $target = $item.FullName.Replace($source,$destination)

        if (!(test-path($dir))) {
            create_directory $dir
        }
        
        if (!(test-path($target))) {
            cp -path $item.FullName -destination $target
        }
    }
}