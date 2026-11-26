// JsonCompanySerializer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Liber.CompanyWriters;

public class JsonCompanyWriter : ICompanyWriter
{
    private readonly JsonSerializerOptions _options;

    public JsonCompanyWriter(JsonSerializerOptions options)
    {
        _options = options;
    }

    public Task WriteAsync(Stream output, Company company)
    {
        return JsonSerializer.SerializeAsync(output, company, _options);
    }
}
