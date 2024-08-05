# New-PublishPage.ps1
# Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
# Licensed under the MIT License.

[Environment]::CurrentDirectory = $(Get-Location)

$xslCompiledTransform = New-Object System.Xml.Xsl.XslCompiledTransform
$stringReader = [System.IO.StringReader]::new((Get-Content "../docs/publish.html" -Raw).Replace("&nbsp", " "))
$xmlWriter = [System.Xml.XmlWriter]::Create("../docs/index.html")

try {
    $xmlReader = [System.Xml.XmlReader]::Create($stringReader)

    $xslCompiledTransform.Load("publish-page.xslt")
    
    try {
        $xslCompiledTransform.Transform($xmlReader, $xmlWriter)   
    }
    finally {
        $xmlReader.Dispose()
    }
}
finally {
    $stringReader.Close()
    $xmlWriter.Close() 
}
