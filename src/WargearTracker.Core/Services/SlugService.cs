using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WargearTracker.Core.Services;
public static class SlugService
{
    public static string GenerateSlug(string name)
    {
        string slug = name.ToLower().Replace(" ", "-");
        return Regex.Replace(slug, "[^a-z0-9-]", "");
    }
}
