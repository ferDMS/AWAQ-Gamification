using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

// helps the certificate to force accept for testing purposes, and to not deal with certificate
public class ForceAcceptAll : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        return true;
    }
}
