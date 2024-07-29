# Certificates

* <https://digitaldrummerj.me/angular-local-ssl/>

```bash
openssl req -new -x509 -newkey rsa:2048 -sha256 -nodes -keyout localhost.key -days 3560 -out localhost.crt -config certificate.config
openssl req      -x509 -newkey rsa:2048         -nodes -keyout localhost.key -days 3650 -out localhost.crt -addext "subjectAltName = DNS:xxxx.provider.com"  
```

```bash
New-SelfSignedCertificate -DnsName www.mydomain.example -CertStoreLocation cert:\LocalMachine\My

New-SelfSignedCertificate -DnsName interno.winper.cl -CertStoreLocation cert:\LocalMachine\My

New-SelfSignedCertificate -DnsName *.mydomain.example -CertStoreLocation cert:\LocalMachine\My

```
