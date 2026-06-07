# Create-Certificate.ps1
# Copyright (c) 2023-2026 Ishan Pranav
# Licensed under the MIT license.

# https://github.com/ishanpranav/signing/blob/master/Create-Certificate.ps1

$message = "Please enter a name for the certificate in the 'User name' textbox.

Do not include spaces or other special characters in the certificate name.

Choose a password to encrypt the private key and enter it in the 'Password' textbox."
$credential = Get-Credential -Message $message -UserName "Ishan Pranav"

if ($null -eq $credential.Password) {
    exit
}

$certStoreLocation = "Cert:\CurrentUser\My"
$newParams = @{
    CertStoreLocation = $certStoreLocation
    HashAlgorithm     = "SHA256"
    FriendlyName      = $credential.UserName
    KeyExportPolicy   = "Exportable"
    KeyUsage          = "DigitalSignature"
    NotAfter          = (Get-Date).AddYears(2)
    Provider          = "Microsoft Strong Cryptographic Provider"
    Subject           = "CN=" + $credential.UserName
    Type              = "CodeSigning"
}

$certificate = New-SelfSignedCertificate @newParams
$exportCerParams = @{
    Cert     = $certificate 
    FilePath = [IO.Path]::ChangeExtension($credential.UserName, "cer") 
}

Export-Certificate @exportCerParams

$exportPfxParams = @{
    Cert     = Join-Path -Path $certStoreLocation -ChildPath $certificate.Thumbprint
    FilePath = [IO.Path]::ChangeExtension($credential.UserName, "pfx")
    Password = $credential.Password
}

Export-PfxCertificate @exportPfxParams
Write-Output "
Exported the public key (*.cer) and public-private key pair (*.pfx) and
installed the certificate in the $certStoreLocation certificate store.

To use this certificate without errors, manually move the certificate to the
trusted root certificate store (WARNING: this will make the self-signed
certificate a root authority).

Finally, run Add-Signature.ps1 to sign files."
