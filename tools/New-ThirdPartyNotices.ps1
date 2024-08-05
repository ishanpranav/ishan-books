# New-ThirdPartyNotices.ps1
# Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
# Licensed under the MIT License.

$path = "../THIRD-PARTY-NOTICES.md"

if (Test-Path $path) {
    Remove-Item $path
}

function Get-LicensePath {
    param ($key)

    $index = $key.IndexOf('_')

    if ($index -ne -1) {
        $key = $key.Substring(0, $index)
    }

    return "../docs/" + $key + ".txt"
}

function Out-ThirdPartyNotices {
    param ($value)

    Out-File -FilePath $path -InputObject $value -Append
}

function Convert-MarkdownString {
    param ($value)

    return $value.Replace('#', "\#")
}

$hashtable = @{}

function Out-Dependencies {
    param ($dependencies)

    foreach ($dependency in $dependencies) {
        if ($null -eq $dependency.index) {
            $dependency.index = $dependency.author
        }
    }

    foreach ($dependency in $dependencies | Sort-Object -Property "index") {
        Out-ThirdPartyNotices ""
        
        $out = "### " + (Convert-MarkdownString $dependency.title) + "&emsp;<sub><sup>"
        
        foreach ($format in $dependency.formats) {
            $out += '*' + (Convert-MarkdownString $format) + "*&ensp;"
        }
    
        Out-ThirdPartyNotices ($out + "</sup></sub>")
    
        if ($null -ne $dependency.author) {
            Out-ThirdPartyNotices ("- Author: " + $dependency.author)
        }
    
        if ($null -ne $dependency.designer) {
            Out-ThirdPartyNotices ("- Designer: " + $dependency.designer)
        }
    
        if ($null -ne $dependency.source) {
            $source = $dependency.source

            if ($source.StartsWith("https://github.com/")) {
                $source = $source.Substring(19)
            }

            if ($source.StartsWith("https://en.wikipedia.org/wiki/")) {
                $source = $source.Substring(30).Replace('_', "%20") + "%20-%20Wikipedia"
            }

            Out-ThirdPartyNotices ("- Source: [" + [System.Web.HttpUtility]::UrlDecode($source) + "](" + $dependency.source + ")")
        }

        if ($null -ne $dependency.licenseUrl) {
            Out-ThirdPartyNotices ""
            Out-ThirdPartyNotices ("See [here](" + $dependency.licenseUrl + ") for the project copyright information.")
        }
        elseif ($null -ne $dependency.license) {
            if ($null -eq $dependency.copyright) {
                $dependency.copyright = ""
            }

            if ($hashtable.ContainsKey($dependency.license)) {
                $license = $hashtable[$dependency.license]
            } else {
                $licensePath = Get-LicensePath $dependency.license

                if (Test-Path $licensePath) {
                    $license = @{
                        title = Get-Content $licensePath | Select-Object -First 1
                        text = Join-String -Separator "`n" -InputObject (Get-Content $licensePath | Select-Object -Skip 1)
                    }
                } else {
                    $licenseReference = $json.licenses[$dependency.license]
                    $license = @{
                        title = $licenseReference.title
                        text = (Invoke-WebRequest -Uri $licenseReference.url).Content
                    }
                }

                $license.text = [string]::Format($license.text, $dependency.copyright)
                $hashtable.Add($dependency.license, $license)
            }

            Out-ThirdPartyNotices ("- License: [" + $license.title + "](#" + $dependency.license + ')')
        }

        if ($null -ne $dependency.resource) {
            Out-ThirdPartyNotices ""
            Out-ThirdPartyNotices ("See [here](" + $dependency.resource + ") for the resource included from the repository.")
        }

        if ($null -ne $dependency.notices) {
            Out-ThirdPartyNotices ""
            Out-ThirdPartyNotices ("For more information about this software, please see its [third-party notices](" + $dependency.notices + ").")
        }
    }

    Out-ThirdPartyNotices ""
}

$json = Get-Content -Path "licenses.json" -Raw | ConvertFrom-Json -AsHashtable   

Out-ThirdPartyNotices "Third-Party Notices
===================
IshanBooks by Ishan Pranav

Copyright (c) 2023-2024 Ishan Pranav

This software uses third-party libraries or other resources that may be
distributed under licenses different than the software.

The attached notices are provided for informational purposes only. Please
create a new GitHub issue if a required notice is missing. 

Dependencies
------------
This section contains notices for binary dependencies redistributed alongside
the application."
Out-Dependencies $json.dependencies
Out-ThirdPartyNotices "References
----------
This section contains references to parts of the source code based on or
inspired by third-party open-source software."
Out-Dependencies $json.references
Out-ThirdPartyNotices "Resources
---------
This section contains attributions for helpful resources that assisted in the
development of this software. These elements may be included in the source
repository but are not redistributed with release versions of the application."
Out-Dependencies $json.tools
Out-ThirdPartyNotices "Licenses
--------"

foreach ($key in $hashtable.Keys | Sort-Object) {
    $license = $hashtable[$key]
    
    Out-ThirdPartyNotices ""
    Out-ThirdPartyNotices ("### <a id='" + $key + "'>" + $license.title + "</a>")
    Out-ThirdPartyNotices ""
    Out-ThirdPartyNotices "``````"
    Out-ThirdPartyNotices $license.text
    Out-ThirdPartyNotices "``````"
    Out-ThirdPartyNotices "________________________________________________________________________________"
}
