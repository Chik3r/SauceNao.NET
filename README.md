SauceNao.NET
===
A library and command-line tool to utilize the [SauceNAO](https://saucenao.com/) api.

## Projects
### SauceNao.NET
A .NET 7 library for SauceNAO. It is possible to search for images from a `byte[]` or from an image URL.
It is not currently possible to filter the results shown by database or by explicitness.
#### Example:

```csharp
SauceNao sauceNao = new("YOUR API KEY");

// You can load an image as a byte[] and search for it
byte[] imageBytes = ...;
SearchResult result = await sauceNao.Search(imageBytes);

// Or you can use a URL
SearchResult result = await sauceNao.Search("example.com");

// From a search result, you can get the similarity of an image
double similarity = result.Results[0].Header.Similarity;
// The Url of an image
string url = result.Results[0].Data.ExtUrls[0];
// and many other fields

```

### SauceNao.NET.Client
A CLI client that takes in an API token and a local image, finds similar images, and optionally downloads the first pixiv match.
#### Usage:
```bash
SauceNao.NET.Client.exe API_KEY path/to/image.png [--download] [--similarity SIMILARITY]

# Search for an image and download if the similarity is above 70%
SauceNao.NET.Client.exe API_KEY path/to/image.png --download --similarity 70
```

### SauceNao.NET.Test
Unit tests for SauceNao.NET