using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;

namespace sip.front.nonce
{
  public class Csp : IHttpModule
  {

    private HttpApplication _application;
    private string _guid;
    private string _csp;
    private string _hashes;

    public Csp()
    {
    }

    public string ModuleName
    {
      get { return "sip.front.nonce.Csp"; }
    }

    public bool IsReusable
    {
      get { return false; }
    }

    public void Init(HttpApplication application)
    {
      _guid =
          Guid
              .NewGuid()
              .ToString("N");

      GenerarHashes();

      var csp = new string[]{
              string.Format("script-src 'self' 'nonce-{0}' 'strict-dynamic' interno.winper.cl;", _guid),
              string.Format("style-src 'self' 'nonce-{0}' 'strict-dynamic' interno.winper.cl fonts.googleapis.com {1};", _guid, _hashes),
              "object-src 'none';",
              "base-uri 'self';",
              "connect-src 'self';",
              "font-src 'self' fonts.googleapis.com fonts.gstatic.com data:;",
              "frame-src 'self' data:;",
              "img-src 'self' data:;",
              "media-src 'self';",
              "worker-src 'none';"
            };
      _csp = string.Join(" ", csp);

      _application = application;
      application.EndRequest += ApplicationOnEndRequest;
    }

    private string GenerateGuid()
    {
      return Guid.NewGuid().ToString("N");
    }

    private void GenerarHashes()
    {
      var hashes = new List<string>
            {
                "'sha256-biLFinpqYMtWHmXfkA1BPeCY0/fNt46SAZ+BBk5YUog='"
              , "'sha256-OYMB7KMN+glKFkxpFQE42PT+MTewAPJ9fVw6sa/qbek='"
              , "'sha256-qnVkQSG7pWu17hBhIw0kCpfEB3XGvt0mNRa6+uM6OUU='"
              , "'sha256-5u9eiy0/oYBlRWoBMGJd/qM1QzwI5EWRoyTyRpm2Meo='"
              , "'sha256-wu3gJluHsPexcM0L2bTmsflJi5LPQF4LPQ/Cs+bwQDE='"
              , "'sha256-eGEjDHvKPIwNyzNFfYy1mZ3TrVNfLfu2Q9FgclYobfk='"
              , "'sha256-sz5p/e6Zt4J7CcB1R7yU3roB1AVlo459l2Frxw22/jo='"
              , "'sha256-jRRyeWYB1QapYeuwhzkZgnARGSV/8pTppUFlv+qNCYM='"
              , "'sha256-zZX8EVAmzM5CWGBb+lQIpEYl5zaDZXsj4qI2OfcaYjI='"
              , "'sha256-VVgLSFum0lNrXS66nARmCc+/rV6FcnUUyrQN0V1CwIc='"
              , "'sha256-y0P4mDiY46aqq0yjLBnlAwwe704yW8nRqclnYhwG1nY='"
              , "'sha256-JPC3E4++HgdW1AFa+Ko2QxdeL3DSVnLrGJMjku2qDKQ='"
              , "'sha256-QhboIpr9GPGj+MOGPPNg2TP0M62SFUonbeIKqS5oTAQ='"
              , "'sha256-7VXlcg/uSZugHSa6UtIG2/44ju460LiO4M0CyQfraX8='"
              , "'sha256-i3s2bkABzFoSr6y+Om8DX4NyJBBPAfM7Pa6a7aLdhTY='"
              , "'sha256-NHarn8wEqJqUQoKwsaJttWeSqzOSSPTy65p3Z6aS0Qs='"
              , "'sha256-+17AcPK/e5AtiK52Z2vnx3uG3BMzyzRr4Qv5UQsEbDU='"
              , "'sha256-Pg7ZwGkhxKgYdFyLfY1fVYVQUA2Mp2v/zRbkMdZ+73w='"
              , "'sha256-CZNSnR7ebrxcGX2/NuQ9QfHMKdndBXp8JjbzUv4sZRM='"
              , "'sha256-VjQbKDTZ0HrHdNcSELSwrrPNUNYT2AM5eyVHMLPLpzY='"
              , "'sha256-JHifDLeo1FrY9lSi8/sIR91pHkTa67ozQWhP0zwLtlo='"
              , "'sha256-DL2IXj82ZaxyeudYyUlldcmA2Ado0C8vLkhPmTFfEd8='"
              , "'sha256-+HZ2bN6aSsrOvF73rkgsamlC7VqQGpJdGcLo+BPRJHU='"
              , "'sha256-5gdGQ5tJpJXRNhPg1knhMpFkry/6QByZSurLMIrG85s='"
              , "'sha256-eaibCr3PsW3XeGgV8Lk6pz78HTvy1D+WCCs46f2CyU8='"
              , "'sha256-eQ5h08Y91O6oHSOTIgqtLCqEvmkkLksyI65gus0dT7c='"
              , "'sha256-HAgnzuwwWZVTWCpTtdsuLzsJE2dB3AZeO0DpiUJoNc0='"
              , "'sha256-MF9fuQdQlemaUA/SZ/Y2838RHNcvTtiJkT5+wuK9rRg='"
              , "'sha256-mIIAEApnnrJrLyO9y8Nk5rJksDecDmbJn+bQdTVhHfI='"
              , "'sha256-NZwvcDYu2y3P1AE0Wic05Q/OpjX+BxUpcqPRs4bR+48='"
              , "'sha256-GMOG99chr3nu6AAQQF55vnTf3bJo3QskXrhrXruuqw4='"
              , "'sha256-DiWU82cFIBGH7Ne2frmS4x7VCHwEoh4eM70g23XHRCc='"
              , "'sha256-47DEQpj8HBSa+/TImW+5JCeuQeRkm5NMpJWZG3hSuFU='"
              , "'sha256-JhKzV0YjUGHarJKL4vqCbydjnGoEwOBB0BFyH44brUA='"
              , "'sha256-GwHZT535b07mYnmBNai2AVuOqmgTTF1A4EqzeDlIyl0='"
              , "'sha256-47DEQpj8HBSa+/TImW+5JCeuQeRkm5NMpJWZG3hSuFU='"
              , "'sha256-WNfaHXSw9mxMVgOvSbG/K9X39EMhSDF8QPNCwXRvWpg='"
              , "'sha256-xP0rftOzPbJ61kuyhz42qPE+qhnAQy3lQ9MzzB/etFs='"
            };

      hashes =
          hashes
              .Distinct()
              .ToList();

      var consolidado =
          string.Join(" ", hashes);

      _hashes = consolidado;
    }

    private void ApplicationOnEndRequest(object sender, EventArgs e)
    {
      var context =
          _application.Context;

      var path =
          context.Request.Url.AbsolutePath;

      if (!path.EndsWith("index.html"))
        return;

      var response = context.Response;

      response.Headers["Content-Security-Policy"] = _csp;

      var originalFilter = context.Response.Filter;
      var memoryStream = new MemoryStream();
      context.Response.Filter = memoryStream;

      context.Response.Flush();

      memoryStream.Position = 0;

      var contentEncoding = context.Response.Headers["Content-Encoding"];
      var htmlOriginal = DescomprimirSiEsNecesario(memoryStream, contentEncoding, context.Response.ContentEncoding);
      var htmlModificado = htmlOriginal.Replace("_CSP_NONCE_", _guid).Replace("_CSP_RULE_", _csp);
      var contenidoComprimido = ComprimirSiEsNecesario(htmlModificado, contentEncoding, context.Response.ContentEncoding);

      context.Response.Filter = originalFilter;
      context.Response.Clear();

      context.Response.OutputStream.Write(contenidoComprimido, 0, contenidoComprimido.Length);
    }

    private string DescomprimirSiEsNecesario(Stream input, string encodingContenido, Encoding encoding)
    {
      if (encodingContenido == "gzip")
      {
        using (var stream = new GZipStream(input, CompressionMode.Decompress))
        using (var reader = new StreamReader(stream, encoding))
        {
          return reader.ReadToEnd();
        }
      }

      if (encodingContenido == "deflate")
      {
        using (var stream = new DeflateStream(input, CompressionMode.Decompress))
        using (var reader = new StreamReader(stream, encoding))
        {
          return reader.ReadToEnd();
        }
      }

      using (var reader = new StreamReader(input, encoding))
      {
        return reader.ReadToEnd();
      }
    }

    private byte[] ComprimirSiEsNecesario(string content, string encodingContent, Encoding encoding)
    {
      var input = encoding.GetBytes(content);

      if (encodingContent == "gzip")
      {
        using (var output = new MemoryStream())
        using (var stream = new GZipStream(output, CompressionMode.Compress))
        {
          stream.Write(input, 0, input.Length);
          stream.Close();

          return output.ToArray();
        }
      }

      if (encodingContent == "deflate")
      {
        using (var output = new MemoryStream())
        using (var stream = new DeflateStream(output, CompressionMode.Compress))
        {
          stream.Write(input, 0, input.Length);
          stream.Close();

          return output.ToArray();
        }
      }

      return input;
    }

    public void Dispose()
    {
    }

  }
}
